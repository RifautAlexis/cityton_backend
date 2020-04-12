using System;
using Cityton.Api.Data.Models;
using Cityton.Api.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cityton.Api.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ChallengeGiven> ChallengesGiven { get; set; }
        public DbSet<ParticipantGroup> ParticipantGroups { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<UserInDiscussion> UsersInDiscussion { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Media> Medias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CompanyMap(modelBuilder.Entity<Company>());
            new UserMap(modelBuilder.Entity<User>());
            new ChallengeMap(modelBuilder.Entity<Challenge>());
            new AchievementMap(modelBuilder.Entity<Achievement>());
            new GroupMap(modelBuilder.Entity<Group>());
            new ChallengeGivenMap(modelBuilder.Entity<ChallengeGiven>());
            new ParticipantGroupMap(modelBuilder.Entity<ParticipantGroup>());
            new DiscussionMap(modelBuilder.Entity<Discussion>());
            new UserInDiscussionMap(modelBuilder.Entity<UserInDiscussion>());
            new MessageMap(modelBuilder.Entity<Message>());
            new MediaMap(modelBuilder.Entity<Media>());

            modelBuilder.Seed();
        }

    }
}
