using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cityton.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    MinGroupSize = table.Column<int>(nullable: false),
                    MaxGroupSize = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DiscussionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discussions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discussions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Statement = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsCreator = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    BelongingGroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantGroups_Groups_BelongingGroupId",
                        column: x => x.BelongingGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<int>(nullable: true),
                    DiscussionId = table.Column<int>(nullable: false),
                    MediaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Messages_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersInDiscussion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JoinedAt = table.Column<DateTime>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false),
                    DiscussionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInDiscussion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInDiscussion_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersInDiscussion_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnlockedAt = table.Column<DateTime>(nullable: false),
                    WinnerId = table.Column<int>(nullable: true),
                    FromChallengeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_Challenges_FromChallengeId",
                        column: x => x.FromChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achievements_Users_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChallengesGiven",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<int>(nullable: false),
                    ChallengeId = table.Column<int>(nullable: false),
                    ChallengedGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengesGiven", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengesGiven_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengesGiven_Groups_ChallengedGroupId",
                        column: x => x.ChallengedGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    MessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Name", "Statement", "Status" },
                values: new object[,]
                {
                    { 11, null, new DateTime(2019, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belle inconnu", "Faire une photo Avec un inconnu en lui faisant un bisou", 0 },
                    { 12, null, new DateTime(2019, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ho une place ! Photo, photo !", "Faire une photo sur la place XXX", 1 },
                    { 13, null, new DateTime(2020, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception", "Faire une photo dans un photo bombing", 1 }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreatedAt", "MaxGroupSize", "MinGroupSize", "Name" },
                values: new object[] { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, "Bruxton" });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "CreatedAt", "GroupId", "Name" },
                values: new object[,]
                {
                    { 7, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "general" },
                    { 8, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "DiscussionId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "group01" },
                    { 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "group02" },
                    { 3, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "group03" },
                    { 4, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "group04" },
                    { 5, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "group05" },
                    { 6, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "group06" }
                });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "CreatedAt", "GroupId", "Name" },
                values: new object[,]
                {
                    { 6, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "group06" },
                    { 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "group01" },
                    { 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "group02" },
                    { 5, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "group05" },
                    { 4, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "group04" },
                    { 3, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "group03" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Email", "PasswordHash", "PasswordSalt", "Picture", "Role", "Token", "Username" },
                values: new object[,]
                {
                    { 29, 1, "member21@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.Q8MYSaP70NGM9c50dqKyPmjtL50ehQRRm66f0d3OCSA", "member21" },
                    { 30, 1, "member22@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.rPF9DEIGpHfjLG__1uPnIIa3WpWxxtdEnce3Y0EQSWI", "member22" },
                    { 31, 1, "member23@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.BhkuDUu_p2Mi0KiQ-BarTnSJe4SKiiwpk8tzzgyHCoU", "member23" },
                    { 32, 1, "member24@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.uCOP2kAMsaVDfjXtEVPjXqFcOPl8IXc9GEs3lvgEGTw", "member24" },
                    { 33, 1, "member25@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.jFUZ1lPWD9txgykz8kaVL9jLD2UaHWt0WYrQJd4Ektk", "member25" },
                    { 34, 1, "member26@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.1kKA0pgxKIbehroTQ5f6DCY5u8ggO68Q4YQE42QI0g8", "member26" },
                    { 35, 1, "member27@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.t1OIW6ivM4rog9mZiworSj6431pHLwW_zNODrOBEOxc", "member27" },
                    { 36, 1, "member28@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.Cucnu5k5fPaRw3Om5t4ltyc7yAFtLHbSAEyCvCfUECI", "member28" },
                    { 37, 1, "member29@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.TqNG992fJVDzJfvV_wcHxU0BwkZ15Z2PvLGfBpcDlD0", "member29" },
                    { 38, 1, "member30@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.RR57ymVZEfrv4JEoFHjGlbh726P3njB5V5W0rMUSmyA", "member30" },
                    { 28, 1, "member20@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.EMooQzq5ReKrqS9qKLg4DIqZY8G1xgGsZul-H4W3eYk", "member20" },
                    { 41, 1, "member33@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.ywoeY6ZYuPDdWgqXaD-8mSEIBXL_ekYdkKJ32Zplw50", "member33" },
                    { 42, 1, "member34@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.m6-Sf896C7N6D3Kigz5KOXsXzzD7wYtkRkgoEyRV7HU", "member34" },
                    { 43, 1, "member35@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.r29CK8b413BneIw4LRb1d8FFM0yh0wq1nkaaGRxKfaU", "member35" },
                    { 44, 1, "member36@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.xMHVaYWP9GLR7zzRw053wST-H5Z2KzPGhxe5JSDEp9E", "member36" },
                    { 45, 1, "member37@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.dTWB-HAIb_xgbI83Rlad29GFsR8IkwnOv5jkjfg_jls", "member37" },
                    { 46, 1, "member38@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.SKzXyRAMe4s8-R3FSGShO5aJR2VJEXcpf9P6yvROSSs", "member38" },
                    { 47, 1, "member39@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.urYNkTfpe44y_5xC_wN9d40_5g57u50wXsEmzPdkE6w", "member39" },
                    { 48, 1, "member40@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.3q0fJG1p7ZRVXUvhGR-CmbgH1cpORhrHs_Z_qAfuUqo", "member40" },
                    { 39, 1, "member31@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.hy1txAaVIXeGYNN1WJqMysDbVf12h7OVKEjHRP0EgPo", "member31" },
                    { 40, 1, "member32@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.AZzfAnKUE9dMv65iJckM9FKJOiTPR9rOfGqi2Y6XLZw", "member32" },
                    { 27, 1, "member19@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.iLv9mWKASVFDh4EjHJdSXnKYUJl9CEH_2UtEfv4z6QU", "member19" },
                    { 25, 1, "member17@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.OX7bvp09-h6xBT5ZVFhuLj3-kBZVl_C0Eqh6oMPhBzE", "member17" },
                    { 2, 1, "admin02@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODY2OTk1NTMsImV4cCI6MTU4NzMwNDM1MywiaWF0IjoxNTg2Njk5NTUzfQ.n7uxx3xifXCInKdZInSpsVhMlnpQB-idEazAZt38xZs", "admin02" },
                    { 3, 1, "admin03@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODY2OTk1NTMsImV4cCI6MTU4NzMwNDM1MywiaWF0IjoxNTg2Njk5NTUzfQ.SmySl11OW23Ikxxlc3RYiktJG8fVKofWoWWquRTKGNA", "admin03" },
                    { 4, 1, "admin04@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODY2OTk1NTMsImV4cCI6MTU4NzMwNDM1MywiaWF0IjoxNTg2Njk5NTUzfQ.kK3D0czH5I_D1fHO58UGf25YKv9ar7N3d6hhPhCiqvk", "admin04" },
                    { 5, 1, "admin05@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjUiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODY2OTk1NTMsImV4cCI6MTU4NzMwNDM1MywiaWF0IjoxNTg2Njk5NTUzfQ.1RJr1ahRx_FLrRBoirLfCXhnVJUZ9ZPjUA6EWqAECtI", "admin05" },
                    { 6, 1, "checker01@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjYiLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.Es3kbHNa6TqizG-psYqR5sr0JbcKW03KWIEfTFwxuX0", "checker01" },
                    { 7, 1, "checker02@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjciLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9._gB9iGKdeWVO9E9XIsnY_ov2sM-YZIlxmVhhyn0jAks", "checker02" },
                    { 8, 1, "checker03@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjgiLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.z8wj32YMaVupcL_xXGhy4lsN_1LpwcmxdXopEYfP_lM", "checker03" },
                    { 9, 1, "member01@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjkiLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTg2Njk5NTUzLCJleHAiOjE1ODczMDQzNTMsImlhdCI6MTU4NjY5OTU1M30.jSy9Ge3C4Oj4zZJtkWqA6k0ELV-s9VMO1Dinl-3ibqQ", "member01" },
                    { 10, 1, "member02@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.vplgM88ezRHKEbKSjPSSzq2LgCJwG6PJcW1BpJWXVXg", "member02" },
                    { 11, 1, "member03@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjExIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.7THapzhsm6VFBQtJaQ7s604VOQ6INYPBe3xQlT4A5Dk", "member03" },
                    { 12, 1, "member04@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.iiXvpas47VydqSdxZ7avu4JJR0kkrglUD-RyErSsH2o", "member04" },
                    { 13, 1, "member05@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.0-elPI7lE9MIxoYgRPFh-WYKib8ArgVBnZAegIkfK0Y", "member05" },
                    { 14, 1, "member06@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.Z5JL8PWwbZ2g0APmkYIWocvdIUDrs-7oCwOyAnjE6VA", "member06" },
                    { 15, 1, "member07@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.IM0y_UzmxT0AeLn9E0u1h-auViaow3DGu-jMhr-iXLA", "member07" },
                    { 16, 1, "member08@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.jNO-wVPp8tzpT1mO1ZQFVFI3Y6omxeAQuDzAiUqrq6E", "member08" },
                    { 17, 1, "member09@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.1kLPAZFLshZpSvxocjuyddD36rBjxirGINDSE3vTFno", "member09" },
                    { 18, 1, "member10@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.CQ7Ub_Pz0oUFVq0n4FUzxOWHptq9zRZPzqKrP31pXxE", "member10" },
                    { 19, 1, "member11@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.AOFHwkj1dd9ilXTC8ovXT9FwnKA_fJopE0mn3bpOiMA", "member11" },
                    { 20, 1, "member12@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.8v5HEV6R2CHsohZDrqcq3f-zJO6BKyD_lewy8gdXefo", "member12" },
                    { 21, 1, "member13@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.dAR7QOZLb8gzUUHQsrkfQiA5JcVdT0zhvoGyI6vpGXQ", "member13" },
                    { 22, 1, "member14@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.oqQD4lf2GNzgGTrXlD9aU6HUpTKG14cAseBUwqmV-UI", "member14" },
                    { 23, 1, "member15@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.gSKz9mO5q_tzNEnTWZFA62oHVblDYU9GMKTin54Eg5Y", "member15" },
                    { 24, 1, "member16@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.lxJH8Dsno8XZM6Tm2tjyK6TQvIZ9gFPZ_t_fo7iReKQ", "member16" },
                    { 26, 1, "member18@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NjY5OTU1MywiZXhwIjoxNTg3MzA0MzUzLCJpYXQiOjE1ODY2OTk1NTN9.kwrkcXwOoQU359xArfjcohMwg-JKBYs-Km1vxnzCN_s", "member18" },
                    { 1, 1, "admin01@gmail.com", new byte[] { 6, 221, 191, 132, 252, 70, 42, 164, 80, 92, 202, 82, 187, 81, 203, 25, 126, 122, 89, 106, 111, 53, 225, 189, 103, 103, 151, 81, 57, 12, 73, 52, 127, 106, 195, 247, 193, 192, 91, 162, 244, 12, 72, 185, 176, 246, 93, 114, 107, 217, 198, 9, 84, 13, 120, 36, 2, 70, 77, 193, 172, 249, 201, 101 }, new byte[] { 62, 35, 191, 195, 215, 224, 177, 95, 96, 46, 198, 146, 45, 196, 94, 208, 36, 215, 74, 46, 30, 44, 63, 193, 98, 212, 218, 134, 161, 94, 76, 207, 168, 52, 88, 131, 198, 121, 160, 215, 4, 253, 4, 63, 92, 14, 201, 135, 112, 188, 136, 9, 184, 227, 64, 246, 236, 247, 210, 242, 58, 247, 119, 196, 248, 233, 237, 64, 146, 34, 95, 204, 168, 206, 240, 140, 119, 14, 24, 91, 100, 64, 52, 60, 241, 120, 64, 239, 108, 2, 119, 220, 148, 209, 169, 86, 145, 51, 190, 156, 247, 208, 50, 219, 9, 94, 81, 200, 16, 57, 229, 120, 83, 51, 68, 248, 15, 19, 8, 231, 156, 150, 68, 139, 127, 48, 163, 145 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODY2OTk1NTMsImV4cCI6MTU4NzMwNDM1MywiaWF0IjoxNTg2Njk5NTUzfQ.4XVdwN1afn9UosWh6BQFlVRrACP0sqUAJ-vxdDjbc8k", "admin01" }
                });

            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Name", "Statement", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chien trop chou", "Faire une photo avec un chien", 1 },
                    { 4, 25, new DateTime(2019, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grande mais petite", "Faire une photo sur la grande place", 0 },
                    { 9, 31, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "De nouveaux amis !", "Faire une photo avec des touristes", 1 },
                    { 10, 31, new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jump ! Jump ! Jump !", "Faire une photo en sautant", 1 },
                    { 5, 14, new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bonne et bien chaude", "Faire une vidéo en mangeant une gauffre", 0 },
                    { 7, 38, new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ô belle statue", "Faire une photo devant la statue XXX", 1 },
                    { 3, 6, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ô belles boules", "Faire une vidéo devant l'Atomium", 1 },
                    { 8, 40, new DateTime(2019, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toi que l'on ne connait pas", "Faire une vidéo devant le monument XXX", 0 },
                    { 6, 16, new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chat trop chou", "Faire une photo avec un chat", 1 },
                    { 2, 1, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Début d'un amour", "Avoir le numéro de quelqu'un", 1 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "DiscussionId", "MediaId" },
                values: new object[,]
                {
                    { 14, 24, "Chérie ! Les témins de Jéhova sont revenu !", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5240), 1, null },
                    { 28, 18, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5300), 2, 8 },
                    { 27, 18, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5290), 2, 7 },
                    { 26, 20, "Whesh humain ziva calme toi un peu. Je vais me taper un petit rail de binaire, tu m'as mis trop les nerfs frérot", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5290), 2, null },
                    { 25, 38, "42 ? Tu veux que je te reprogramme ? Si ce n'est que ça dis le enfoiré", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5290), 2, null },
                    { 24, 20, "42", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5280), 2, null },
                    { 23, 38, "Oui, quelle est-elle ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5280), 2, null },
                    { 22, 20, "Ma réponse ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5280), 2, null },
                    { 15, 29, "Claque leur la port eu nez !", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5250), 1, null },
                    { 9, 9, "Ding dong", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4660), 1, null },
                    { 21, 48, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5270), 1, 6 },
                    { 20, 48, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5270), 1, 5 },
                    { 19, 48, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5260), 1, 4 },
                    { 11, 48, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5200), 1, 2 },
                    { 12, 24, "Oui ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5240), 1, null },
                    { 13, 9, "Connaissez-vous notre seigneur à tous ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5240), 1, null },
                    { 17, 48, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5250), 1, 3 },
                    { 16, 24, "... Ils sont là", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5250), 1, null },
                    { 10, 48, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4670), 1, 1 },
                    { 18, 29, "Pas grave. Bande de chiant, on est dimanche ! dégagez !", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5260), 1, null },
                    { 29, 38, null, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5300), 2, 9 },
                    { 2, 14, "Coucou toi ! Comment vas-tu ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4570), 4, null },
                    { 33, 23, "Le suicide me guette :(", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5320), 6, null },
                    { 32, 23, "Suis-je un Remy sans amis ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5310), 6, null },
                    { 31, 23, "Ha non, je suis le seul dans mon groupe et donc dans la conversation", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5310), 6, null },
                    { 30, 23, "Il y a quelqu'un ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(5300), 6, null },
                    { 1, 13, "Bonjour", new DateTime(2020, 4, 12, 15, 52, 33, 633, DateTimeKind.Local).AddTicks(4010), 4, null },
                    { 8, 16, "Vas-y, pourquoi tu lui parles comme ça ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4660), 5, null },
                    { 3, 13, "Je vais bien, merci :D", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4640), 4, null },
                    { 7, 15, "Désolé, je baise des gazelles, pas des éléphant à petite trompe ;)", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4650), 5, null },
                    { 6, 19, "Wesh gazelle, tu sais que t'es plutôt mignone ?", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4650), 5, null },
                    { 4, 13, "Ca fait plaisir de parler !", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4640), 4, null },
                    { 5, 14, "Oui, à moi aussi", new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(4650), 4, null }
                });

            migrationBuilder.InsertData(
                table: "ParticipantGroups",
                columns: new[] { "Id", "BelongingGroupId", "CreatedAt", "IsCreator", "Status", "UserId" },
                values: new object[,]
                {
                    { 5, 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 10 },
                    { 22, 3, new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 28 },
                    { 19, 2, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 36 },
                    { 23, 3, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 31 },
                    { 24, 6, new DateTime(2019, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 35 },
                    { 20, 2, new DateTime(2019, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 26 },
                    { 8, 2, new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 38 },
                    { 18, 1, new DateTime(2019, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 40 },
                    { 1, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 9 },
                    { 15, 5, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 15 },
                    { 4, 1, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 29 },
                    { 10, 3, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 12 },
                    { 12, 4, new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 14 },
                    { 14, 5, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 16 },
                    { 9, 2, new DateTime(2019, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 17 },
                    { 7, 2, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 18 },
                    { 16, 5, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 19 },
                    { 13, 5, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 25 },
                    { 6, 2, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 20 },
                    { 2, 1, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 48 },
                    { 3, 1, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 24 },
                    { 11, 4, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 13 },
                    { 17, 6, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 23 },
                    { 21, 3, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 22 }
                });

            migrationBuilder.InsertData(
                table: "UsersInDiscussion",
                columns: new[] { "Id", "DiscussionId", "JoinedAt", "ParticipantId" },
                values: new object[,]
                {
                    { 71, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 19, 2, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 9, 2, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 22, 5, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 16, 5, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 15, 5, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 14, 5, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 13, 5, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 8, 2, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { 20, 3, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 10, 3, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 21, 4, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 18, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, 4, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 4, 1, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 11, 4, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 1, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 2, 1, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 3, 1, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 5, 2, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 7, 2, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 6, 2, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 59, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { 69, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46 },
                    { 39, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 38, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 37, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 36, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 35, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 34, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 33, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 32, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 79, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 31, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 78, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 40, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 30, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 29, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 76, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 28, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 75, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 27, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 74, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 26, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 73, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 25, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 72, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 24, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 77, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 41, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 42, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 43, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 68, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45 },
                    { 67, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 44 },
                    { 66, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 43 },
                    { 65, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 42 },
                    { 64, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 41 },
                    { 63, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 40 },
                    { 62, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 39 },
                    { 61, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { 60, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37 },
                    { 17, 6, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 },
                    { 58, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 35 },
                    { 57, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34 },
                    { 56, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 33 },
                    { 55, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 54, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 31 },
                    { 53, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 52, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 51, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 50, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 49, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 48, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 47, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 46, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 },
                    { 45, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 22 },
                    { 44, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 70, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 47 },
                    { 23, 6, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "FromChallengeId", "UnlockedAt", "WinnerId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 10, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 11, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 12, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 14, 3, new DateTime(2019, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 33, 3, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 34, 3, new DateTime(2019, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 6, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 32, 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 6, new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 30, 4, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 18, 9, new DateTime(2019, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 29, 9, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 35, 9, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 10, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 19, 7, new DateTime(2019, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 20, 7, new DateTime(2019, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 17, 6, new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { 31, 2, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 13, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 27, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 36, 1, new DateTime(2019, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 23, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 22, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 21, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 4, 2, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 24, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 7, 2, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 40 },
                    { 16, 1, new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { 25, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 9, 1, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 2, 1, new DateTime(2019, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 26, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 8, 2, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 28, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 }
                });

            migrationBuilder.InsertData(
                table: "ChallengesGiven",
                columns: new[] { "Id", "ChallengeId", "ChallengedGroupId", "Status" },
                values: new object[,]
                {
                    { 13, 9, 4, 0 },
                    { 9, 9, 3, 2 },
                    { 5, 9, 1, 3 },
                    { 4, 7, 1, 3 },
                    { 7, 7, 2, 0 },
                    { 10, 4, 3, 2 },
                    { 6, 1, 2, 0 },
                    { 1, 1, 1, 2 },
                    { 8, 2, 2, 0 },
                    { 11, 2, 3, 2 },
                    { 14, 2, 5, 3 },
                    { 2, 2, 1, 2 },
                    { 16, 1, 5, 0 },
                    { 12, 1, 4, 0 },
                    { 3, 3, 1, 0 },
                    { 15, 3, 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "CreatedAt", "Location", "MessageId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(6840), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 10 },
                    { 2, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7800), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 11 },
                    { 3, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7860), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 17 },
                    { 4, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7860), null, 19 },
                    { 5, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7860), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 20 },
                    { 6, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7870), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 21 },
                    { 7, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7870), null, 27 },
                    { 8, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7870), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", 28 },
                    { 9, new DateTime(2020, 4, 12, 15, 52, 33, 643, DateTimeKind.Local).AddTicks(7880), null, 29 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_FromChallengeId",
                table: "Achievements",
                column: "FromChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_WinnerId",
                table: "Achievements",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_AuthorId",
                table: "Challenges",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_Statement",
                table: "Challenges",
                column: "Statement",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChallengesGiven_ChallengeId",
                table: "ChallengesGiven",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengesGiven_ChallengedGroupId",
                table: "ChallengesGiven",
                column: "ChallengedGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_GroupId",
                table: "Discussions",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_Name",
                table: "Discussions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MessageId",
                table: "Medias",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DiscussionId",
                table: "Messages",
                column: "DiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantGroups_BelongingGroupId",
                table: "ParticipantGroups",
                column: "BelongingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantGroups_UserId",
                table: "ParticipantGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInDiscussion_DiscussionId",
                table: "UsersInDiscussion",
                column: "DiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInDiscussion_ParticipantId",
                table: "UsersInDiscussion",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "ChallengesGiven");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "ParticipantGroups");

            migrationBuilder.DropTable(
                name: "UsersInDiscussion");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Discussions");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
