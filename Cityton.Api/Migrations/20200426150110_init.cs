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
                    Title = table.Column<string>(nullable: false),
                    Statement = table.Column<string>(nullable: false),
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
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Statement", "Title" },
                values: new object[,]
                {
                    { 11, null, new DateTime(2019, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo Avec un inconnu en lui faisant un bisou", "Belle inconnu" },
                    { 12, null, new DateTime(2019, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo sur la place XXX", "Ho une place ! Photo, photo !" },
                    { 13, null, new DateTime(2020, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo dans un photo bombing", "Inception" }
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
                    { 29, 1, "member21@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.C6OwtrxubVx9tXsHHDStWD6fQGppu4uqAoS70gzVFZ4", "member21" },
                    { 30, 1, "member22@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.x4gZAIxbWTdFCijoX-dlYQWLn8kpl9D_1KxWfk0qCxo", "member22" },
                    { 31, 1, "member23@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.UMZ2u0OY7dSWzz5-A35S4h8bjvVnK2FbiFKUDgSqw_I", "member23" },
                    { 32, 1, "member24@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.f_n5N01CQSqF_jQoKldDJ_hHKTBj4DT7rtTIUaLWM5k", "member24" },
                    { 33, 1, "member25@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.FRkomeXt3wepGT51c_JL45c1tV17behY9rjMQLO48SQ", "member25" },
                    { 34, 1, "member26@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.c9IiuLsf8W6fkB1epBFXbWwlhJv866__SDRRwh_vx_k", "member26" },
                    { 35, 1, "member27@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.DhbFOaa6Qo4oWLbTIe5V2kUxENpEpm4RhXk21edS0oE", "member27" },
                    { 36, 1, "member28@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.D7yr_8Ylt_7HJsvmMYdCjJeMJ6o52cgFGYNNUqH-uy4", "member28" },
                    { 37, 1, "member29@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.-G04unQP7DHKUOyRwrWEdp9wQRLl4ON-jmg2uhNfDJo", "member29" },
                    { 38, 1, "member30@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.QxmQkEsW6AVw8_Q3zkua1OklM_dNOeggd3cAQpQnCH8", "member30" },
                    { 28, 1, "member20@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.bb9u11x5fUT0w5Sugw30wioAIhp63qxwT6VmPEN5mMc", "member20" },
                    { 41, 1, "member33@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.FjhJfIDwrkrzkUB0l_jm6H_9E6Kh7IuyYHq6hBD_gZc", "member33" },
                    { 42, 1, "member34@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.SwWeQ8HRp6XXsGzN1vjriTYxG7X6o0j3MPaosnHZYm0", "member34" },
                    { 43, 1, "member35@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.x3DctptPa3u5ws5WQGDxH1fbBaveZSALvhhHvLAIVvQ", "member35" },
                    { 44, 1, "member36@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.7FkxO018pGosTErro6L-WG_gAOBc-mO5PXMTWaEs0QU", "member36" },
                    { 45, 1, "member37@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.PQ9x6g4ShZSPoERwVctHHDk0lhTJMdO9Ed4AJQ_b550", "member37" },
                    { 46, 1, "member38@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.rAjuYdP5TfquDLUGKuBFqumxMQBFAbg2EFklHoZraAk", "member38" },
                    { 47, 1, "member39@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.LtJtpD_1AfzyApOoAeOoYiMK6Gbn126NITMywKFtEBQ", "member39" },
                    { 48, 1, "member40@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.hhdohPKuMxAOYrYi34b5O6953a_u0XS-k7TBy32MR5c", "member40" },
                    { 39, 1, "member31@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.Y8Avi6SfOgLbIHL4QFhBEnRs2yIIWTCoHNZFKsULdDY", "member31" },
                    { 40, 1, "member32@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.nHKamc1HOYXc5HhSueaj_OvHO39iSuvwfkLR_r6NwH4", "member32" },
                    { 27, 1, "member19@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.2ENyHV8maqzodFKx9EE2rKjuh_c2Bnx3GOK6Yor4nB4", "member19" },
                    { 25, 1, "member17@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.TH5Z7muyMJ-W2Ve4tlOb54-cPab_3uRz_qy03aJQH_E", "member17" },
                    { 2, 1, "admin02@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODc5MTMyNjksImV4cCI6MTU4ODUxODA2OSwiaWF0IjoxNTg3OTEzMjY5fQ.O-9KQ7yFJygv-XHLgrG8UZMm780Nu6jMG1kB5gh3hZA", "admin02" },
                    { 3, 1, "admin03@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODc5MTMyNjksImV4cCI6MTU4ODUxODA2OSwiaWF0IjoxNTg3OTEzMjY5fQ.g4yAN2w6gVK5l8oP3wFslhNm418DexzLBfDI5GfAKUw", "admin03" },
                    { 4, 1, "admin04@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODc5MTMyNjksImV4cCI6MTU4ODUxODA2OSwiaWF0IjoxNTg3OTEzMjY5fQ.kyLAoCx6zy_yzMD4gqK3jD_lKIxhPCndm-kRkRVENcg", "admin04" },
                    { 5, 1, "admin05@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjUiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODc5MTMyNjksImV4cCI6MTU4ODUxODA2OSwiaWF0IjoxNTg3OTEzMjY5fQ.wZDvyXhYQhCs0OyJUO0vmUtTKthtVjo_otIeSf6rUwQ", "admin05" },
                    { 6, 1, "checker01@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjYiLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.bFlIs-IUBF4SJcO95GTGPtsOLE5d_0ULP9rP_7Jc0S4", "checker01" },
                    { 7, 1, "checker02@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjciLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.VXtRREPlRaH3oRf9I8H8Aow0JaXSl16-iHe2aXgE_5w", "checker02" },
                    { 8, 1, "checker03@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjgiLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.lUA9FdwIZYRuvs_kXOUy_0UJjmh8-es76INbhbBmXdg", "checker03" },
                    { 9, 1, "member01@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjkiLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTg3OTEzMjY5LCJleHAiOjE1ODg1MTgwNjksImlhdCI6MTU4NzkxMzI2OX0.rUPtNR5bWFk1OWQ0JWRufvlsKZHG7zlCU6sFP-BcM8Q", "member01" },
                    { 10, 1, "member02@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.gBMb792MCR678S5BaDTDbk8IM390_XzKfyw4rHMoNF4", "member02" },
                    { 11, 1, "member03@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjExIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.EmaJabh3OyvC8BUrwRc4mlLioG_cWxyuAT8Z6wRQIH4", "member03" },
                    { 12, 1, "member04@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.rPvx_E3ZhjNfPGzLLRSc0fIznUa4yeK15MmK6nUEK-s", "member04" },
                    { 13, 1, "member05@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.2fXqecUtC1UwkVB7AzK7S0bDDDJvFxCzaIyotfSU3sE", "member05" },
                    { 14, 1, "member06@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.3MEKbJyE3ZxakTC0gdiMfxVIs-icK_HY4CNeT33D-P8", "member06" },
                    { 15, 1, "member07@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.xUJGFzP_prHGRVRPSVgihE6fN_PUmn9TPpI2IohJTP4", "member07" },
                    { 16, 1, "member08@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.1j2d6gGWN9HRg_VGYZVPXUWe5O_l2Cc-Ved22e4qVdg", "member08" },
                    { 17, 1, "member09@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.Vvzs5cymF_rfwMZvcBweVEHsT562lbv7qJgmoLz_yLo", "member09" },
                    { 18, 1, "member10@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.6yxAX8GapbGAj01iE2G2f6Ii830fhmonvhUa1pRYp-0", "member10" },
                    { 19, 1, "member11@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.2UrG1dHYjaKB3hd3bd74prsDqWj-11ZrPg2AgzJfgG4", "member11" },
                    { 20, 1, "member12@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.by8TeEGGi2FzFLpgFSg5WpvJevlc0NFiWAYEqu_amWs", "member12" },
                    { 21, 1, "member13@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.pBWKttGrBzcGeyFBFZXTE27k-EttShTQPGDq68KnsMo", "member13" },
                    { 22, 1, "member14@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.23fSvog_agvMuAM3hJvSZskC_8kdFbcLn1h-hH1apWQ", "member14" },
                    { 23, 1, "member15@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.qBiAvSy4qQDpqSp243QZnvsle4bpjeYsZCZi8Fzj3D8", "member15" },
                    { 24, 1, "member16@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.XLgw1R8MxlgY5Kd98x6QF7rjSYZ0azKBPxFdfJTg2Ec", "member16" },
                    { 26, 1, "member18@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU4NzkxMzI2OSwiZXhwIjoxNTg4NTE4MDY5LCJpYXQiOjE1ODc5MTMyNjl9.BykGemKNO8rgOIN4qycJq4Y05rcasC8ryV-JKjb4jtg", "member18" },
                    { 1, 1, "admin01@gmail.com", new byte[] { 87, 235, 67, 208, 156, 10, 33, 9, 125, 154, 17, 74, 24, 255, 197, 44, 128, 238, 192, 26, 105, 135, 49, 111, 34, 57, 211, 41, 42, 88, 139, 89, 132, 253, 211, 15, 233, 45, 219, 128, 166, 95, 84, 47, 254, 66, 5, 248, 58, 255, 194, 255, 113, 93, 251, 110, 60, 71, 242, 86, 9, 38, 28, 99 }, new byte[] { 10, 243, 129, 141, 26, 36, 202, 189, 176, 111, 14, 75, 189, 219, 92, 12, 155, 73, 56, 246, 207, 34, 112, 47, 2, 66, 22, 205, 91, 220, 32, 240, 0, 76, 121, 26, 188, 145, 182, 214, 155, 186, 249, 205, 55, 52, 208, 109, 135, 177, 111, 74, 179, 31, 61, 16, 78, 202, 155, 196, 160, 15, 7, 7, 208, 23, 108, 58, 212, 192, 131, 246, 116, 65, 195, 3, 104, 154, 197, 149, 209, 79, 101, 69, 0, 97, 138, 67, 121, 229, 200, 110, 166, 146, 170, 255, 211, 247, 65, 59, 7, 174, 17, 184, 159, 57, 185, 156, 52, 38, 99, 73, 255, 91, 79, 59, 186, 93, 194, 35, 229, 73, 134, 165, 198, 99, 159, 8 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1ODc5MTMyNjksImV4cCI6MTU4ODUxODA2OSwiaWF0IjoxNTg3OTEzMjY5fQ.CdGzvw22TUPEHOwOUSS8gIj7j11lLAF_8UJeAZG248w", "admin01" }
                });

            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Statement", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo avec un chien", "Chien trop chou" },
                    { 4, 25, new DateTime(2019, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo sur la grande place", "Grande mais petite" },
                    { 9, 31, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo avec des touristes", "De nouveaux amis !" },
                    { 10, 31, new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo en sautant", "Jump ! Jump ! Jump !" },
                    { 5, 14, new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une vidéo en mangeant une gauffre", "Bonne et bien chaude" },
                    { 7, 38, new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo devant la statue XXX", "Ô belle statue" },
                    { 3, 6, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une vidéo devant l'Atomium", "Ô belles boules" },
                    { 8, 40, new DateTime(2019, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une vidéo devant le monument XXX", "Toi que l'on ne connait pas" },
                    { 6, 16, new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo avec un chat", "Chat trop chou" },
                    { 2, 1, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avoir le numéro de quelqu'un", "Début d'un amour" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "DiscussionId", "MediaId" },
                values: new object[,]
                {
                    { 14, 24, "Chérie ! Les témins de Jéhova sont revenu !", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(50), 1, null },
                    { 28, 18, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(100), 2, 8 },
                    { 27, 18, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(100), 2, 7 },
                    { 26, 20, "Whesh humain ziva calme toi un peu. Je vais me taper un petit rail de binaire, tu m'as mis trop les nerfs frérot", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(100), 2, null },
                    { 25, 38, "42 ? Tu veux que je te reprogramme ? Si ce n'est que ça dis le enfoiré", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(90), 2, null },
                    { 24, 20, "42", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(90), 2, null },
                    { 23, 38, "Oui, quelle est-elle ?", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(90), 2, null },
                    { 22, 20, "Ma réponse ?", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(80), 2, null },
                    { 15, 29, "Claque leur la port eu nez !", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(60), 1, null },
                    { 9, 9, "Ding dong", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9480), 1, null },
                    { 21, 48, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(80), 1, 6 },
                    { 20, 48, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(80), 1, 5 },
                    { 19, 48, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(70), 1, 4 },
                    { 11, 48, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(10), 1, 2 },
                    { 12, 24, "Oui ?", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(50), 1, null },
                    { 13, 9, "Connaissez-vous notre seigneur à tous ?", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(50), 1, null },
                    { 17, 48, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(70), 1, 3 },
                    { 16, 24, "... Ils sont là", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(60), 1, null },
                    { 10, 48, null, new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9480), 1, 1 },
                    { 18, 29, "Pas grave. Bande de chiant, on est dimanche ! dégagez !", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(70), 1, null },
                    { 29, 38, null, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(110), 2, 9 },
                    { 2, 14, "Coucou toi ! Comment vas-tu ?", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9390), 4, null },
                    { 33, 23, "Le suicide me guette :(", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(120), 6, null },
                    { 32, 23, "Suis-je un Remy sans amis ?", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(120), 6, null },
                    { 31, 23, "Ha non, je suis le seul dans mon groupe et donc dans la conversation", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(110), 6, null },
                    { 30, 23, "Il y a quelqu'un ?", new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(110), 6, null },
                    { 1, 13, "Bonjour", new DateTime(2020, 4, 26, 17, 1, 9, 650, DateTimeKind.Local).AddTicks(4360), 4, null },
                    { 8, 16, "Vas-y, pourquoi tu lui parles comme ça ?", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9470), 5, null },
                    { 3, 13, "Je vais bien, merci :D", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9450), 4, null },
                    { 7, 15, "Désolé, je baise des gazelles, pas des éléphant à petite trompe ;)", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9470), 5, null },
                    { 6, 19, "Wesh gazelle, tu sais que t'es plutôt mignone ?", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9470), 5, null },
                    { 4, 13, "Ca fait plaisir de parler !", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9460), 4, null },
                    { 5, 14, "Oui, à moi aussi", new DateTime(2020, 4, 26, 17, 1, 9, 658, DateTimeKind.Local).AddTicks(9460), 4, null }
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
                    { 1, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(1620), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 10 },
                    { 2, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2600), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 11 },
                    { 3, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2630), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 17 },
                    { 4, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2630), null, 19 },
                    { 5, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2630), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 20 },
                    { 6, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2640), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 21 },
                    { 7, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2640), null, 27 },
                    { 8, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2640), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 28 },
                    { 9, new DateTime(2020, 4, 26, 17, 1, 9, 659, DateTimeKind.Local).AddTicks(2650), null, 29 }
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
