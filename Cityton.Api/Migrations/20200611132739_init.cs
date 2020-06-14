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
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DiscussionId = table.Column<int>(nullable: false),
                    SupervisorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                columns: new[] { "Id", "CreatedAt", "DiscussionId", "Name", "SupervisorId" },
                values: new object[,]
                {
                    { 3, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "group03", null },
                    { 6, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "group06", null }
                });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "CreatedAt", "GroupId", "Name" },
                values: new object[,]
                {
                    { 6, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "group06" },
                    { 3, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "group03" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Email", "PasswordHash", "PasswordSalt", "Picture", "Role", "Token", "Username" },
                values: new object[,]
                {
                    { 47, 1, "member39@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.sVmCe80bQpvcAHcFjM5KIHbpQ96sfc79LbhxSNRVgIc", "member39" },
                    { 27, 1, "member19@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.pkB-BHP9wvIcvGh6buO9rxwb32vPSWNEe4GbIixdoe4", "member19" },
                    { 28, 1, "member20@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.bkoWZF2H9eyn3cy-gGxyZ2UL_w4aSSBOFPOT6o5dKz0", "member20" },
                    { 29, 1, "member21@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.WK_kmgy1OTQaMM-Qktv_uIosK5Sl4WDWY6R2Dspb2FA", "member21" },
                    { 30, 1, "member22@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.xKwjjZpEf2HBqxihfzwHYGLUi6-67sEr4nQrm6jYA-M", "member22" },
                    { 31, 1, "member23@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.YPRNGYuiHBI3W3IHe5NroqaIl777vRJixr2K1PUJOwc", "member23" },
                    { 32, 1, "member24@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.Xhqv5NgweOiUMySbyl0hDqT4yVctFPKzy36PF4Xf3Wc", "member24" },
                    { 33, 1, "member25@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.mOasM3Uqf39Zq2e-hdBxFLbFs50kkLv03Ggc1aPNHLo", "member25" },
                    { 34, 1, "member26@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.-OKak0VIlz-NAD9mcxkr_F9WvR5ar12i0rZK02iwhOQ", "member26" },
                    { 35, 1, "member27@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.1UTB5x3fuzYCHlDzfsW7T_d2fz7jw3GuFkiiU-p6qZs", "member27" },
                    { 48, 1, "member40@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.FdNetFVU0IcKtQzDxpNpnYZEUcGXrH8eApfZfDlwdy8", "member40" },
                    { 36, 1, "member28@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.406H-Z4oVAMN90pN7Eso_Ssm3UhGDvgDrTcrhcjnIt4", "member28" },
                    { 38, 1, "member30@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.2c9yoYxZLavbkHA_YoqTuIO6Bn9uSo8D75uju83wgHM", "member30" },
                    { 39, 1, "member31@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.SZ-az9jHpJRxbZbY2qtIk6WhJVloyKI0Jt1GjZEMua0", "member31" },
                    { 40, 1, "member32@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.qTjaPMBgkkTCkNOkpeP0BBjhC6zP0IrA8MB2-6kzhZ4", "member32" },
                    { 41, 1, "member33@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.L1OYmKus57kY7s3zHtPXn2t_nM56iGHZgSIBhVzZYxg", "member33" },
                    { 42, 1, "member34@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.348lcmswrRIT0K1pAz3IBhoxUBQIqYkVijYzk4dNwYc", "member34" },
                    { 43, 1, "member35@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.JrmTm7tbGUG5l6B04ZVnNdsY6GT43Z8isbgnyxzSQfQ", "member35" },
                    { 44, 1, "member36@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.MaEes_CoNGN-KsrTGmURQ3auhePqY3VtahiYOZA6Kc0", "member36" },
                    { 45, 1, "member37@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.JUPMpq5Su6YLLaRoMrOHJvWrurCY1cfvPRrhIdjnlmA", "member37" },
                    { 46, 1, "member38@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQ2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.SQcZYwPEIek7vR9ihAM5cUOY896fpaD4wvVqukisH4U", "member38" },
                    { 26, 1, "member18@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.Jx6zbucZpLbs7tOzliKrqDN8LrA4qdRPUB8io-ZqbDE", "member18" },
                    { 37, 1, "member29@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjM3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.UMpwFZyyW-kTlzRXRlgdUlSLXfTul5LBj_HVMLUrBKc", "member29" },
                    { 25, 1, "member17@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.shNRZNGtess3N7yUEkyt8zq6kPOfy5LXs840VwmG8vs", "member17" },
                    { 23, 1, "member15@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.uZ-v7PJRN2MM-bBBwJTtTNt0nk3NQhH5ynxNxMqBPbc", "member15" },
                    { 2, 1, "admin02@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTE4ODIwNTgsImV4cCI6MTU5MjQ4Njg1OCwiaWF0IjoxNTkxODgyMDU4fQ.Vd9w9p0_lpNHxVjFrBsw15w1LVbay7-IEd3vz7UUE_k", "admin02" },
                    { 3, 1, "admin03@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTE4ODIwNTgsImV4cCI6MTU5MjQ4Njg1OCwiaWF0IjoxNTkxODgyMDU4fQ.CJ3MiURK5LJsExctTKEhl-nmhfXY24_7C8fF1yPzg68", "admin03" },
                    { 4, 1, "admin04@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTE4ODIwNTgsImV4cCI6MTU5MjQ4Njg1OCwiaWF0IjoxNTkxODgyMDU4fQ.Ofkyv9HqbIl90rRqTeLed-Lsv8VcIFjp6SOlXnICIAI", "admin04" },
                    { 5, 1, "admin05@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjUiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTE4ODIwNTgsImV4cCI6MTU5MjQ4Njg1OCwiaWF0IjoxNTkxODgyMDU4fQ.1mnS08_GC-vmeQW6VhBDgIlWp06dnly5L674ziRwgrk", "admin05" },
                    { 6, 1, "checker01@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjYiLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.yloRIwVBYpoCZ7Asj3f9NnDUrdUhgEKEhz_o_IhP7lw", "checker01" },
                    { 7, 1, "checker02@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjciLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.GJkOgvGU_phR1phbsbU9_ISxjPPgPxE93wU0rVHZQNU", "checker02" },
                    { 8, 1, "checker03@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjgiLCJyb2xlIjoiQ2hlY2tlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.XeK4Xnpa12EbgatKOlPRtpcBKIY1rPvSFUUAoQtaGQE", "checker03" },
                    { 9, 1, "member01@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjkiLCJyb2xlIjoiTWVtYmVyIiwibmJmIjoxNTkxODgyMDU4LCJleHAiOjE1OTI0ODY4NTgsImlhdCI6MTU5MTg4MjA1OH0.H95B3z96eQwwEuxCAacLEDjP-G3-R0WJ1tnZAuJ6Uxw", "member01" },
                    { 10, 1, "member02@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.iPyshoV49fbjYPsumHJfR9mgqnQoNa2K0AtIO4ZwFfc", "member02" },
                    { 11, 1, "member03@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjExIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.bm6Lmnu0715sMbDgMbnacVWVSP4NTRzPeb3cqATXg6Q", "member03" },
                    { 12, 1, "member04@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.iRi9BauMyPcPw2SdS2WdFeMeKBBX_hOLqINVryL2g24", "member04" },
                    { 13, 1, "member05@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEzIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.SO6f0hMP9nfsR08sjWOGjEgFl__00TOnFQK4KYPp-Co", "member05" },
                    { 14, 1, "member06@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.gt1s_FT9xuTJ3U0kPwaljvZ5dW6x919PSELICbIB6fk", "member06" },
                    { 15, 1, "member07@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE1Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.oxgO3Usmc4L8qrGoEq_oTTnAvzQ4ppVnBz-102wZ7Dg", "member07" },
                    { 16, 1, "member08@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE2Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.uQCRNB8OS4igr_aoqc7tdqN5PsVuCh4qM3kDDVsAtfE", "member08" },
                    { 17, 1, "member09@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE3Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.K-hzkjdTI5ImRj1g2RK5VpwVJkO0m7e5n-ndy83Mgis", "member09" },
                    { 18, 1, "member10@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE4Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.uSBtxTha2dRGEnWhz6KPLCsaVW8_aojhVBngbbFLm6Q", "member10" },
                    { 19, 1, "member11@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE5Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.e8FfGU1es5vc6rfu3Bx8c9aUXPq6nalaQ1uhIHxTyIU", "member11" },
                    { 20, 1, "member12@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIwIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.rMYbIBkDFfCpTsKBY9FgOoGlpLfXUrovMDUQtEdRfdc", "member12" },
                    { 21, 1, "member13@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIxIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.pbxtsdILM64RG9HN1tuZN18GeWb_C_LsMQ-5yDKM5vM", "member13" },
                    { 22, 1, "member14@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIyIiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.q9aUryUJZubHNbaikOJA9YggwppCmaT3YYUfr_ZyYt0", "member14" },
                    { 24, 1, "member16@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 0, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjI0Iiwicm9sZSI6Ik1lbWJlciIsIm5iZiI6MTU5MTg4MjA1OCwiZXhwIjoxNTkyNDg2ODU4LCJpYXQiOjE1OTE4ODIwNTh9.HIB-uKHlVQE9ZJEidHabbURjKq1MZEi_baHVKFtiN_w", "member16" },
                    { 1, 1, "admin01@gmail.com", new byte[] { 77, 119, 165, 78, 115, 199, 170, 223, 164, 67, 39, 88, 192, 12, 230, 27, 74, 179, 111, 183, 0, 94, 58, 168, 123, 139, 217, 163, 133, 171, 98, 111, 186, 7, 218, 33, 91, 198, 64, 14, 23, 243, 94, 221, 74, 240, 10, 208, 23, 75, 53, 117, 235, 116, 120, 184, 159, 206, 37, 169, 22, 166, 197, 143 }, new byte[] { 133, 70, 134, 178, 186, 153, 143, 58, 253, 122, 48, 114, 175, 194, 113, 115, 214, 86, 207, 129, 118, 10, 27, 23, 133, 206, 171, 174, 99, 81, 203, 85, 31, 62, 190, 200, 94, 120, 53, 122, 156, 131, 18, 223, 7, 202, 151, 201, 40, 101, 17, 10, 206, 43, 170, 163, 239, 157, 18, 157, 240, 189, 190, 141, 160, 18, 41, 112, 108, 135, 8, 45, 15, 222, 243, 110, 4, 66, 181, 130, 226, 152, 7, 128, 59, 169, 255, 88, 255, 198, 201, 188, 3, 106, 149, 49, 72, 118, 214, 242, 108, 40, 189, 122, 3, 123, 252, 217, 79, 68, 4, 196, 40, 64, 41, 211, 113, 43, 107, 234, 185, 244, 21, 23, 103, 19, 139, 199 }, "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 2, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTE4ODIwNTgsImV4cCI6MTU5MjQ4Njg1OCwiaWF0IjoxNTkxODgyMDU4fQ.RzVLEdmKj0yYfMYA9H6mCjjG6SM8ORdsPNAispHuxsY", "admin01" }
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
                    { 6, 16, new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo avec un chat", "Chat trop chou" },
                    { 5, 14, new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une vidéo en mangeant une gauffre", "Bonne et bien chaude" },
                    { 8, 40, new DateTime(2019, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une vidéo devant le monument XXX", "Toi que l'on ne connait pas" },
                    { 3, 6, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une vidéo devant l'Atomium", "Ô belles boules" },
                    { 7, 38, new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faire une photo devant la statue XXX", "Ô belle statue" },
                    { 2, 1, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avoir le numéro de quelqu'un", "Début d'un amour" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "DiscussionId", "Name", "SupervisorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "group01", 1 },
                    { 4, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "group04", 5 },
                    { 5, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "group05", 6 },
                    { 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "group02", 1 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "DiscussionId", "MediaId" },
                values: new object[,]
                {
                    { 32, 23, "Suis-je un Remy sans amis ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2670), 6, null },
                    { 33, 23, "Le suicide me guette :(", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2670), 6, null },
                    { 31, 23, "Ha non, je suis le seul dans mon groupe et donc dans la conversation", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2660), 6, null },
                    { 30, 23, "Il y a quelqu'un ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2660), 6, null }
                });

            migrationBuilder.InsertData(
                table: "ParticipantGroups",
                columns: new[] { "Id", "BelongingGroupId", "CreatedAt", "IsCreator", "Status", "UserId" },
                values: new object[,]
                {
                    { 24, 6, new DateTime(2019, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 35 },
                    { 17, 6, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 23 },
                    { 21, 3, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 22 },
                    { 10, 3, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 12 },
                    { 23, 3, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 31 },
                    { 22, 3, new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 28 }
                });

            migrationBuilder.InsertData(
                table: "UsersInDiscussion",
                columns: new[] { "Id", "DiscussionId", "JoinedAt", "ParticipantId" },
                values: new object[,]
                {
                    { 55, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 70, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 47 },
                    { 53, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 52, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 51, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 54, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 31 },
                    { 56, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 33 },
                    { 58, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 35 },
                    { 71, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 59, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { 60, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37 },
                    { 61, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { 62, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 39 },
                    { 63, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 40 },
                    { 64, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 41 },
                    { 65, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 42 },
                    { 10, 3, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 66, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 43 },
                    { 67, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 44 },
                    { 68, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45 },
                    { 69, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46 },
                    { 57, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34 },
                    { 50, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 45, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 22 },
                    { 48, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 24, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 72, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 25, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 73, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 26, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 74, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 27, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 75, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 28, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 76, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 29, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 77, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 30, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 78, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 31, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 79, 8, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 32, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 47, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 46, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 },
                    { 44, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 43, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 42, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 41, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 49, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 40, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 38, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 37, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 36, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 35, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 34, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 33, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 39, 7, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 17, 6, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "FromChallengeId", "UnlockedAt", "WinnerId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 3, 10, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 10, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 35, 9, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 29, 9, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 18, 9, new DateTime(2019, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 30, 4, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 19, 7, new DateTime(2019, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 17, 6, new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { 5, 6, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 34, 3, new DateTime(2019, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 33, 3, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 14, 3, new DateTime(2019, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 13, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 12, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 6, 6, new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 20, 7, new DateTime(2019, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 11, 3, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 31, 2, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 2, 1, new DateTime(2019, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 9, 1, new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 16, 1, new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { 21, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 22, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 23, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 32, 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 36, 1, new DateTime(2019, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 2, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 },
                    { 24, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 7, 2, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 40 },
                    { 8, 2, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 25, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 26, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 27, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 28, 2, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 }
                });

            migrationBuilder.InsertData(
                table: "ChallengesGiven",
                columns: new[] { "Id", "ChallengeId", "ChallengedGroupId", "Status" },
                values: new object[,]
                {
                    { 16, 1, 5, 0 },
                    { 15, 3, 5, 3 },
                    { 14, 2, 5, 3 },
                    { 3, 3, 1, 0 },
                    { 5, 9, 1, 3 },
                    { 9, 9, 3, 2 },
                    { 10, 4, 3, 2 },
                    { 13, 9, 4, 0 },
                    { 7, 7, 2, 0 },
                    { 8, 2, 2, 0 },
                    { 6, 1, 2, 0 },
                    { 12, 1, 4, 0 },
                    { 4, 7, 1, 3 },
                    { 11, 2, 3, 2 },
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "CreatedAt", "GroupId", "Name" },
                values: new object[,]
                {
                    { 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "group02" },
                    { 5, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "group05" },
                    { 4, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "group04" },
                    { 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "group01" }
                });

            migrationBuilder.InsertData(
                table: "ParticipantGroups",
                columns: new[] { "Id", "BelongingGroupId", "CreatedAt", "IsCreator", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 9 },
                    { 2, 1, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 48 },
                    { 3, 1, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 24 },
                    { 4, 1, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 29 },
                    { 18, 1, new DateTime(2019, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 40 },
                    { 11, 4, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 13 },
                    { 5, 2, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 10 },
                    { 6, 2, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 20 },
                    { 16, 5, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 19 },
                    { 15, 5, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 15 },
                    { 14, 5, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 16 },
                    { 13, 5, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 25 },
                    { 7, 2, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 18 },
                    { 8, 2, new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 38 },
                    { 9, 2, new DateTime(2019, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 17 },
                    { 19, 2, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 36 },
                    { 12, 4, new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 14 },
                    { 20, 2, new DateTime(2019, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 26 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "DiscussionId", "MediaId" },
                values: new object[,]
                {
                    { 9, 9, "Ding dong", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1970), 1, null },
                    { 8, 16, "Vas-y, pourquoi tu lui parles comme ça ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1960), 5, null },
                    { 7, 15, "Désolé, je baise des gazelles, pas des éléphant à petite trompe ;)", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1960), 5, null },
                    { 6, 19, "Wesh gazelle, tu sais que t'es plutôt mignone ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1960), 5, null },
                    { 5, 14, "Oui, à moi aussi", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1950), 4, null },
                    { 4, 13, "Ca fait plaisir de parler !", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1950), 4, null },
                    { 3, 13, "Je vais bien, merci :D", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1940), 4, null },
                    { 2, 14, "Coucou toi ! Comment vas-tu ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1880), 4, null },
                    { 1, 13, "Bonjour", new DateTime(2020, 6, 11, 15, 27, 38, 752, DateTimeKind.Local).AddTicks(3880), 4, null },
                    { 29, 38, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2660), 2, 9 },
                    { 28, 18, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2650), 2, 8 },
                    { 26, 20, "Whesh humain ziva calme toi un peu. Je vais me taper un petit rail de binaire, tu m'as mis trop les nerfs frérot", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2650), 2, null },
                    { 25, 38, "42 ? Tu veux que je te reprogramme ? Si ce n'est que ça dis le enfoiré", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2650), 2, null },
                    { 24, 20, "42", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2640), 2, null },
                    { 23, 38, "Oui, quelle est-elle ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2640), 2, null },
                    { 22, 20, "Ma réponse ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2550), 2, null },
                    { 27, 18, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2650), 2, 7 },
                    { 17, 48, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2540), 1, 3 },
                    { 14, 24, "Chérie ! Les témins de Jéhova sont revenu !", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2530), 1, null },
                    { 13, 9, "Connaissez-vous notre seigneur à tous ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2520), 1, null },
                    { 12, 24, "Oui ?", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2520), 1, null },
                    { 11, 48, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2490), 1, 2 },
                    { 16, 24, "... Ils sont là", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2530), 1, null },
                    { 19, 48, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2540), 1, 4 },
                    { 18, 29, "Pas grave. Bande de chiant, on est dimanche ! dégagez !", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2540), 1, null },
                    { 20, 48, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2550), 1, 5 },
                    { 21, 48, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2550), 1, 6 },
                    { 10, 48, null, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(1970), 1, 1 },
                    { 15, 29, "Claque leur la port eu nez !", new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(2530), 1, null }
                });

            migrationBuilder.InsertData(
                table: "UsersInDiscussion",
                columns: new[] { "Id", "DiscussionId", "JoinedAt", "ParticipantId" },
                values: new object[,]
                {
                    { 13, 5, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 21, 4, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 12, 4, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 11, 4, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 14, 5, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 15, 5, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 18, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 19, 2, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, 2, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 8, 2, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { 7, 2, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 6, 2, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 5, 2, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 16, 5, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 1, 1, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 2, 1, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 48 },
                    { 3, 1, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 4, 1, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 22, 5, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "Id", "CreatedAt", "Location", "MessageId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(4230), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 10 },
                    { 2, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5410), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 11 },
                    { 3, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5460), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 17 },
                    { 4, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5470), null, 19 },
                    { 5, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5470), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 20 },
                    { 6, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5470), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 21 },
                    { 7, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5480), null, 27 },
                    { 8, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5480), "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/ProfilePictures/default.png", 28 },
                    { 9, new DateTime(2020, 6, 11, 15, 27, 38, 761, DateTimeKind.Local).AddTicks(5480), null, 29 }
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
                name: "IX_Groups_SupervisorId",
                table: "Groups",
                column: "SupervisorId");

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
                name: "Discussions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
