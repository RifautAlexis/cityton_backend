using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Cityton.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Cityton.Api.Contracts.DTOs;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Cityton.Api.Data;
using Cityton.Api.Hubs.Helper;
using Microsoft.AspNetCore.Mvc;
using Cityton.Api.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cityton.Api.Hubs
{
    public class ChatHub : Hub
    {
        private IHttpContextAccessor _contextAccessor;

        // private static IMemoryCache usersConnectedToChat;
        private static Dictionary<string, int> usersConnectedToChat = new Dictionary<string, int>();
        private readonly IConfiguration _appSettings;
        private readonly ApplicationDBContext _appDBContext;
        private readonly IServiceProvider _serviceProvider;

        public ChatHub(
            IHttpContextAccessor contextAccessor,
            IConfiguration config,
            ApplicationDBContext appDBContext,
            IServiceProvider serviceProvider
        )
        {
            this._contextAccessor = contextAccessor;
            // this._usersConnectedToChat = memoryCache;
            _appSettings = config;
            _appDBContext = appDBContext;
            _serviceProvider = serviceProvider;
        }

        public override Task OnConnectedAsync()
        {
            string jwt = this._contextAccessor.HttpContext.Request.Query["access_token"];

            if (this.IsAuthorized(jwt))
            {
                this.decodeToken(jwt, out int userId, out Role role);

                if (usersConnectedToChat.ContainsKey(Context.ConnectionId))
                {

                    Context.Abort();

                    return Task.FromException(new Exception("A connection already exist, both are removed"));


                }

                usersConnectedToChat.Add(Context.ConnectionId, userId);

                return base.OnConnectedAsync();

            }

            if (usersConnectedToChat.ContainsKey(Context.ConnectionId)) usersConnectedToChat.Remove(Context.ConnectionId); // token expired and trying to reconnect
            Context.Abort();
            return Task.FromException(new Exception("You are not authorized"));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {

            usersConnectedToChat.Remove(Context.ConnectionId);

            Context.Abort();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewMessage(CreateMessageDTO request)
        {
            System.Console.WriteLine("!!!!! HUB !!!!!");
            System.Console.WriteLine(request.MediaUrl);
            System.Console.WriteLine("!!!!! END HUB !!!!!");
            if (!usersConnectedToChat.TryGetValue(Context.ConnectionId, out int connectedUserId))
            {
                Context.Abort();
            }

            if (!string.IsNullOrWhiteSpace(request.Message) || !string.IsNullOrWhiteSpace(request.MediaUrl))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<IHandlerHub<CreateMessageDTO, ObjectResult>>();
                    var objectResult = await handler.Handle(request, connectedUserId);
                    MessageDTO messageAdded = (MessageDTO)objectResult.Value;

                    await Clients.Group(messageAdded.DiscussionId.ToString()).SendAsync("messageReceived", messageAdded);
                }
            }

        }

        /* ****************************** */

        public async Task AddToGroup()
        {
            string currentConnectionId = Context.ConnectionId;

            IEnumerable<Discussion> discussions = await _appDBContext.Discussions
                .Where(d => d.UsersInDiscussion.Any(uid => uid.ParticipantId == usersConnectedToChat.GetValueOrDefault(currentConnectionId)))
                .ToListAsync();

            await discussions
                .ToList()
                .ForEachAsync(d => Groups.AddToGroupAsync(Context.ConnectionId, d.Id.ToString()));

            return;
        }

        public async Task RemoveFromGroup(int discussionId)
        {
            string currentConnectionId = Context.ConnectionId;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, discussionId.ToString());

            await Task.FromResult(true);
        }

        public async Task RemoveFromAllGroups()
        {
            string currentConnectionId = Context.ConnectionId;

            IEnumerable<Discussion> discussions = await _appDBContext.Discussions
                .Where(d => d.UsersInDiscussion.Any(uid => uid.ParticipantId == usersConnectedToChat.GetValueOrDefault(currentConnectionId)))
                .ToListAsync();

            await discussions
                .ToList()
                .ForEachAsync(d => Groups.RemoveFromGroupAsync(Context.ConnectionId, d.Id.ToString()));
        }

        public async Task RemoveMessage(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService<IHandler<int, ObjectResult>>();
                var objectResult = await handler.Handle(id);
                MessageDTO messageRemoved = (MessageDTO)objectResult.Value;

                await Clients.Group(messageRemoved.DiscussionId.ToString()).SendAsync("messageRemoved", messageRemoved);
            }
        }

        /* ****************************** */

        private bool IsAuthorized(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = this._appSettings.GetSection("Settings:Secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken validatedToken;
            try
            {
                var claims = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void decodeToken(string jwt, out int userId, out Role role)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenDecoded = handler.ReadJwtToken(jwt);

            userId = Int32.Parse(tokenDecoded.Claims.First(claim => claim.Type == "unique_name").Value);
            role = (Role)Enum.Parse(typeof(Role), tokenDecoded.Claims.First(claim => claim.Type == "role").Value);
        }
    }
}