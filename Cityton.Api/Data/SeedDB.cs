using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Cityton.Api.Data.Models;

namespace Cityton.Api.Data
{
    public static class SeedDB
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Bruxton",
                    MinGroupSize = 4,
                    MaxGroupSize = 6,
                    CreatedAt = new DateTime(2019, 01, 01)
                }
            );

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash("123", out passwordHash, out passwordSalt);

            List<User> users = new List<User> {
                new User { Id = 1, Username = "admin01", Email = "admin01@gmail.com", Role = Role.Admin, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 2, Username = "admin02", Email = "admin02@gmail.com", Role = Role.Admin, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 3, Username = "admin03", Email = "admin03@gmail.com", Role = Role.Admin, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 4, Username = "admin04", Email = "admin04@gmail.com", Role = Role.Admin, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 5, Username = "admin05", Email = "admin05@gmail.com", Role = Role.Admin, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 6, Username = "checker01", Email = "checker01@gmail.com", Role = Role.Checker, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 7, Username = "checker02", Email = "checker02@gmail.com", Role = Role.Checker, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 8, Username = "checker03", Email = "checker03@gmail.com", Role = Role.Checker, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 9, Username = "member01", Email = "member01@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 10, Username = "member02", Email = "member02@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 11, Username = "member03", Email = "member03@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 12, Username = "member04", Email = "member04@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 13, Username = "member05", Email = "member05@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 14, Username = "member06", Email = "member06@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 15, Username = "member07", Email = "member07@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 16, Username = "member08", Email = "member08@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 17, Username = "member09", Email = "member09@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 18, Username = "member10", Email = "member10@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 19, Username = "member11", Email = "member11@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 20, Username = "member12", Email = "member12@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 21, Username = "member13", Email = "member13@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 22, Username = "member14", Email = "member14@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 23, Username = "member15", Email = "member15@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 24, Username = "member16", Email = "member16@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 25, Username = "member17", Email = "member17@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 26, Username = "member18", Email = "member18@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 27, Username = "member19", Email = "member19@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 28, Username = "member20", Email = "member20@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 29, Username = "member21", Email = "member21@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 30, Username = "member22", Email = "member22@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 31, Username = "member23", Email = "member23@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 32, Username = "member24", Email = "member24@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 33, Username = "member25", Email = "member25@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 34, Username = "member26", Email = "member26@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 35, Username = "member27", Email = "member27@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 36, Username = "member28", Email = "member28@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 37, Username = "member29", Email = "member29@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 38, Username = "member30", Email = "member30@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 39, Username = "member31", Email = "member31@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 40, Username = "member32", Email = "member32@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 41, Username = "member33", Email = "member33@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 42, Username = "member34", Email = "member34@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 43, Username = "member35", Email = "member35@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 44, Username = "member36", Email = "member36@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 45, Username = "member37", Email = "member37@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 46, Username = "member38", Email = "member38@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 47, Username = "member39", Email = "member39@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 },
                new User { Id = 48, Username = "member40", Email = "member40@gmail.com", Role = Role.Member, PasswordHash = passwordHash, PasswordSalt = passwordSalt, CompanyId = 1 }
            };

            foreach (var user in users)
            {
                CreateToken(user);
            }

            List<Group> groups = new List<Group> {
                new Group { Id = 1, Name = "group01", CreatedAt = new DateTime(2019, 02, 01), DiscussionId = 1 },
                new Group { Id = 2, Name = "group02", CreatedAt = new DateTime(2019, 02, 10), DiscussionId = 2 },
                new Group { Id = 3, Name = "group03", CreatedAt = new DateTime(2019, 03, 11), DiscussionId = 3 },
                new Group { Id = 4, Name = "group04", CreatedAt = new DateTime(2019, 03, 11), DiscussionId = 4 },
                new Group { Id = 5, Name = "group05", CreatedAt = new DateTime(2019, 04, 03), DiscussionId = 5 },
                new Group { Id = 6, Name = "group06", CreatedAt = new DateTime(2019, 05, 05), DiscussionId = 6 },
            };

            List<ParticipantGroup> participantsGroup = new List<ParticipantGroup> {
                new ParticipantGroup { Id = 1, IsCreator = true, Status = Status.Accepted, CreatedAt = new DateTime(2019, 02, 01), BelongingGroupId = 1, UserId = 9 },
                new ParticipantGroup { Id = 2, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 02, 04), BelongingGroupId = 1, UserId = 48 },
                new ParticipantGroup { Id = 3, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 03, 15), BelongingGroupId = 1, UserId = 24 },
                new ParticipantGroup { Id = 4, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 03, 22), BelongingGroupId = 1, UserId = 29 },
                new ParticipantGroup { Id = 5, IsCreator = true, Status = Status.Accepted, CreatedAt = new DateTime(2019, 02, 10), BelongingGroupId = 2, UserId = 10 },
                new ParticipantGroup { Id = 6, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 02, 11), BelongingGroupId = 2, UserId = 20 },
                new ParticipantGroup { Id = 7, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 02, 11), BelongingGroupId = 2, UserId = 18 },
                new ParticipantGroup { Id = 8, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 03, 18), BelongingGroupId = 2, UserId = 38 },
                new ParticipantGroup { Id = 9, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 04, 24), BelongingGroupId = 2, UserId = 17 },
                new ParticipantGroup { Id = 10, IsCreator = true, Status = Status.Accepted, CreatedAt = new DateTime(2019, 03, 11), BelongingGroupId = 3, UserId = 12 },
                new ParticipantGroup { Id = 11, IsCreator = true, Status = Status.Accepted, CreatedAt = new DateTime(2019, 03, 11), BelongingGroupId = 4, UserId = 13 },
                new ParticipantGroup { Id = 12, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 03, 19), BelongingGroupId = 4, UserId = 14 },
                new ParticipantGroup { Id = 13, IsCreator = true, Status = Status.Accepted, CreatedAt = new DateTime(2019, 04, 03), BelongingGroupId = 5, UserId = 25 },
                new ParticipantGroup { Id = 14, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 04, 06), BelongingGroupId = 5, UserId = 16 },
                new ParticipantGroup { Id = 15, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 04, 09), BelongingGroupId = 5, UserId = 15 },
                new ParticipantGroup { Id = 16, IsCreator = false, Status = Status.Accepted, CreatedAt = new DateTime(2019, 04, 26), BelongingGroupId = 5, UserId = 19 },
                new ParticipantGroup { Id = 17, IsCreator = true, Status = Status.Accepted, CreatedAt = new DateTime(2019, 05, 05), BelongingGroupId = 6, UserId = 23 },

                new ParticipantGroup { Id = 18, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 02, 03), BelongingGroupId = 1, UserId = 40 },
                new ParticipantGroup { Id = 19, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 04, 09), BelongingGroupId = 2, UserId = 36 },
                new ParticipantGroup { Id = 20, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 04, 30), BelongingGroupId = 2, UserId = 26 },
                new ParticipantGroup { Id = 21, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 03, 12), BelongingGroupId = 3, UserId = 22 },
                new ParticipantGroup { Id = 22, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 03, 20), BelongingGroupId = 3, UserId = 28 },
                new ParticipantGroup { Id = 23, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 03, 21), BelongingGroupId = 3, UserId = 31 },
                new ParticipantGroup { Id = 24, IsCreator = false, Status = Status.Waiting, CreatedAt = new DateTime(2019, 06, 27), BelongingGroupId = 6, UserId = 35 }
            };

            modelBuilder.Entity<User>().HasData(
                users
            );

            modelBuilder.Entity<Group>().HasData(
                groups
            );

            modelBuilder.Entity<ParticipantGroup>().HasData(
                participantsGroup
            );

            modelBuilder.Entity<Challenge>().HasData(
                new Challenge { Id = 1, Statement = "Faire une photo avec un chien", Title = "Chien trop chou", CreatedAt = new DateTime(2019, 01, 02), AuthorId = 1 },
                new Challenge { Id = 2, Statement = "Avoir le numéro de quelqu'un", Title = "Début d'un amour", CreatedAt = new DateTime(2019, 01, 05), AuthorId = 1 },
                new Challenge { Id = 3, Statement = "Faire une vidéo devant l'Atomium", Title = "Ô belles boules", CreatedAt = new DateTime(2019, 01, 02), AuthorId = 6 },
                new Challenge { Id = 4, Statement = "Faire une photo sur la grande place", Title = "Grande mais petite", CreatedAt = new DateTime(2019, 01, 03), AuthorId = 25 },
                new Challenge { Id = 5, Statement = "Faire une vidéo en mangeant une gauffre", Title = "Bonne et bien chaude", CreatedAt = new DateTime(2019, 01, 04), AuthorId = 14 },
                new Challenge { Id = 6, Statement = "Faire une photo avec un chat", Title = "Chat trop chou", CreatedAt = new DateTime(2020, 01, 02), AuthorId = 16 },
                new Challenge { Id = 7, Statement = "Faire une photo devant la statue XXX", Title = "Ô belle statue", CreatedAt = new DateTime(2020, 07, 14), AuthorId = 38 },
                new Challenge { Id = 8, Statement = "Faire une vidéo devant le monument XXX", Title = "Toi que l'on ne connait pas", CreatedAt = new DateTime(2019, 09, 25), AuthorId = 40 },
                new Challenge { Id = 9, Statement = "Faire une photo avec des touristes", Title = "De nouveaux amis !", CreatedAt = new DateTime(2019, 04, 03), AuthorId = 31 },
                new Challenge { Id = 10, Statement = "Faire une photo en sautant", Title = "Jump ! Jump ! Jump !", CreatedAt = new DateTime(2020, 01, 30), AuthorId = 31 },
                new Challenge { Id = 11, Statement = "Faire une photo Avec un inconnu en lui faisant un bisou", Title = "Belle inconnu", CreatedAt = new DateTime(2019, 08, 23), AuthorId = null },
                new Challenge { Id = 12, Statement = "Faire une photo sur la place XXX", Title = "Ho une place ! Photo, photo !", CreatedAt = new DateTime(2019, 07, 13), AuthorId = null },
                new Challenge { Id = 13, Statement = "Faire une photo dans un photo bombing", Title = "Inception", CreatedAt = new DateTime(2020, 01, 09), AuthorId = null }
            );

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement { Id = 1, UnlockedAt = new DateTime(2019, 01, 09), WinnerId = 18, FromChallengeId = 1 },
                new Achievement { Id = 2, UnlockedAt = new DateTime(2019, 01, 09), WinnerId = 25, FromChallengeId = 1 },
                new Achievement { Id = 3, UnlockedAt = new DateTime(2019, 04, 15), WinnerId = 32, FromChallengeId = 10 },
                new Achievement { Id = 4, UnlockedAt = new DateTime(2019, 04, 15), WinnerId = 32, FromChallengeId = 2 },
                new Achievement { Id = 5, UnlockedAt = new DateTime(2019, 04, 15), WinnerId = 32, FromChallengeId = 6 },
                new Achievement { Id = 6, UnlockedAt = new DateTime(2019, 04, 16), WinnerId = 26, FromChallengeId = 6 },
                new Achievement { Id = 7, UnlockedAt = new DateTime(2019, 05, 28), WinnerId = 40, FromChallengeId = 2 },
                new Achievement { Id = 8, UnlockedAt = new DateTime(2019, 05, 28), WinnerId = 14, FromChallengeId = 2 },
                new Achievement { Id = 9, UnlockedAt = new DateTime(2019, 08, 20), WinnerId = 19, FromChallengeId = 1 },
                new Achievement { Id = 10, UnlockedAt = new DateTime(2019, 08, 20), WinnerId = 19, FromChallengeId = 3 },
                new Achievement { Id = 11, UnlockedAt = new DateTime(2019, 08, 20), WinnerId = 11, FromChallengeId = 3 },
                new Achievement { Id = 12, UnlockedAt = new DateTime(2019, 08, 20), WinnerId = 17, FromChallengeId = 3 },
                new Achievement { Id = 13, UnlockedAt = new DateTime(2019, 08, 20), WinnerId = 28, FromChallengeId = 3 },
                new Achievement { Id = 14, UnlockedAt = new DateTime(2019, 09, 14), WinnerId = 9, FromChallengeId = 3 },
                new Achievement { Id = 16, UnlockedAt = new DateTime(2019, 10, 04), WinnerId = 36, FromChallengeId = 1 },
                new Achievement { Id = 17, UnlockedAt = new DateTime(2019, 10, 04), WinnerId = 36, FromChallengeId = 6 },
                new Achievement { Id = 18, UnlockedAt = new DateTime(2019, 12, 08), WinnerId = 26, FromChallengeId = 9 },
                new Achievement { Id = 19, UnlockedAt = new DateTime(2019, 12, 09), WinnerId = 25, FromChallengeId = 7 },
                new Achievement { Id = 20, UnlockedAt = new DateTime(2019, 12, 10), WinnerId = 27, FromChallengeId = 7 },
                new Achievement { Id = 21, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 9, FromChallengeId = 1 },
                new Achievement { Id = 22, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 48, FromChallengeId = 1 },
                new Achievement { Id = 23, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 24, FromChallengeId = 1 },
                new Achievement { Id = 24, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 29, FromChallengeId = 1 },
                new Achievement { Id = 25, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 9, FromChallengeId = 2 },
                new Achievement { Id = 26, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 48, FromChallengeId = 2 },
                new Achievement { Id = 27, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 24, FromChallengeId = 2 },
                new Achievement { Id = 28, UnlockedAt = new DateTime(2020, 01, 13), WinnerId = 29, FromChallengeId = 2 },
                new Achievement { Id = 29, UnlockedAt = new DateTime(2020, 01, 15), WinnerId = 12, FromChallengeId = 9 },
                new Achievement { Id = 30, UnlockedAt = new DateTime(2020, 01, 15), WinnerId = 12, FromChallengeId = 4 },
                new Achievement { Id = 31, UnlockedAt = new DateTime(2020, 01, 15), WinnerId = 12, FromChallengeId = 2 },
                new Achievement { Id = 32, UnlockedAt = new DateTime(2019, 02, 10), WinnerId = null, FromChallengeId = 2 },
                new Achievement { Id = 33, UnlockedAt = new DateTime(2019, 02, 10), WinnerId = null, FromChallengeId = 3 },
                new Achievement { Id = 34, UnlockedAt = new DateTime(2019, 07, 25), WinnerId = null, FromChallengeId = 3 },
                new Achievement { Id = 35, UnlockedAt = new DateTime(2019, 02, 10), WinnerId = null, FromChallengeId = 9 },
                new Achievement { Id = 36, UnlockedAt = new DateTime(2019, 10, 30), WinnerId = null, FromChallengeId = 1 }
            );

            modelBuilder.Entity<ChallengeGiven>().HasData(
                new ChallengeGiven { Id = 1, Status = StatusChallenge.Validated, ChallengeId = 1, ChallengedGroupId = 1 },
                new ChallengeGiven { Id = 2, Status = StatusChallenge.Validated, ChallengeId = 2, ChallengedGroupId = 1 },
                new ChallengeGiven { Id = 3, Status = StatusChallenge.InProgress, ChallengeId = 3, ChallengedGroupId = 1 },
                new ChallengeGiven { Id = 4, Status = StatusChallenge.Rejected, ChallengeId = 7, ChallengedGroupId = 1 },
                new ChallengeGiven { Id = 5, Status = StatusChallenge.Rejected, ChallengeId = 9, ChallengedGroupId = 1 },
                new ChallengeGiven { Id = 6, Status = StatusChallenge.InProgress, ChallengeId = 1, ChallengedGroupId = 2 },
                new ChallengeGiven { Id = 7, Status = StatusChallenge.InProgress, ChallengeId = 7, ChallengedGroupId = 2 },
                new ChallengeGiven { Id = 8, Status = StatusChallenge.InProgress, ChallengeId = 2, ChallengedGroupId = 2 },
                new ChallengeGiven { Id = 9, Status = StatusChallenge.Validated, ChallengeId = 9, ChallengedGroupId = 3 },
                new ChallengeGiven { Id = 10, Status = StatusChallenge.Validated, ChallengeId = 4, ChallengedGroupId = 3 },
                new ChallengeGiven { Id = 11, Status = StatusChallenge.Validated, ChallengeId = 2, ChallengedGroupId = 3 },
                new ChallengeGiven { Id = 12, Status = StatusChallenge.InProgress, ChallengeId = 1, ChallengedGroupId = 4 },
                new ChallengeGiven { Id = 13, Status = StatusChallenge.InProgress, ChallengeId = 9, ChallengedGroupId = 4 },
                new ChallengeGiven { Id = 14, Status = StatusChallenge.Rejected, ChallengeId = 2, ChallengedGroupId = 5 },
                new ChallengeGiven { Id = 15, Status = StatusChallenge.Rejected, ChallengeId = 3, ChallengedGroupId = 5 },
                new ChallengeGiven { Id = 16, Status = StatusChallenge.InProgress, ChallengeId = 1, ChallengedGroupId = 5 }
            );

            /*
            *   Discussion
            */
            modelBuilder.Entity<Discussion>().HasData(
                new Discussion { Id = 1, CreatedAt = new DateTime(2019, 02, 01), Name = "group01", GroupId = 1 },
                new Discussion { Id = 2, CreatedAt = new DateTime(2019, 02, 10), Name = "group02", GroupId = 2 },
                new Discussion { Id = 3, CreatedAt = new DateTime(2019, 03, 11), Name = "group03", GroupId = 3 },
                new Discussion { Id = 4, CreatedAt = new DateTime(2019, 03, 11), Name = "group04", GroupId = 4 },
                new Discussion { Id = 5, CreatedAt = new DateTime(2019, 04, 03), Name = "group05", GroupId = 5 },
                new Discussion { Id = 6, CreatedAt = new DateTime(2019, 05, 05), Name = "group06", GroupId = 6 },

                new Discussion { Id = 7, CreatedAt = new DateTime(2019, 01, 01), Name = "general" },
                new Discussion { Id = 8, CreatedAt = new DateTime(2019, 01, 01), Name = "staff" }
            );

            /*
            *   UserInDiscussion
            */
            modelBuilder.Entity<UserInDiscussion>().HasData(
                new UserInDiscussion { Id = 1, JoinedAt = new DateTime(2019, 02, 01), ParticipantId = 9, DiscussionId = 1 },
                new UserInDiscussion { Id = 2, JoinedAt = new DateTime(2019, 02, 04), ParticipantId = 48, DiscussionId = 1 },
                new UserInDiscussion { Id = 3, JoinedAt = new DateTime(2019, 02, 15), ParticipantId = 24, DiscussionId = 1 },
                new UserInDiscussion { Id = 4, JoinedAt = new DateTime(2019, 02, 22), ParticipantId = 29, DiscussionId = 1 },
                new UserInDiscussion { Id = 5, JoinedAt = new DateTime(2019, 02, 01), ParticipantId = 10, DiscussionId = 2 },
                new UserInDiscussion { Id = 6, JoinedAt = new DateTime(2019, 02, 04), ParticipantId = 20, DiscussionId = 2 },
                new UserInDiscussion { Id = 7, JoinedAt = new DateTime(2019, 02, 15), ParticipantId = 18, DiscussionId = 2 },
                new UserInDiscussion { Id = 8, JoinedAt = new DateTime(2019, 02, 22), ParticipantId = 38, DiscussionId = 2 },
                new UserInDiscussion { Id = 9, JoinedAt = new DateTime(2019, 02, 01), ParticipantId = 17, DiscussionId = 2 },
                new UserInDiscussion { Id = 10, JoinedAt = new DateTime(2019, 02, 04), ParticipantId = 12, DiscussionId = 3 },
                new UserInDiscussion { Id = 11, JoinedAt = new DateTime(2019, 02, 15), ParticipantId = 13, DiscussionId = 4 },
                new UserInDiscussion { Id = 12, JoinedAt = new DateTime(2019, 02, 22), ParticipantId = 14, DiscussionId = 4 },
                new UserInDiscussion { Id = 13, JoinedAt = new DateTime(2019, 02, 01), ParticipantId = 25, DiscussionId = 5 },
                new UserInDiscussion { Id = 14, JoinedAt = new DateTime(2019, 02, 04), ParticipantId = 16, DiscussionId = 5 },
                new UserInDiscussion { Id = 15, JoinedAt = new DateTime(2019, 02, 15), ParticipantId = 15, DiscussionId = 5 },
                new UserInDiscussion { Id = 16, JoinedAt = new DateTime(2019, 02, 22), ParticipantId = 19, DiscussionId = 5 },
                new UserInDiscussion { Id = 17, JoinedAt = new DateTime(2019, 02, 22), ParticipantId = 23, DiscussionId = 6 },

                new UserInDiscussion { Id = 18, JoinedAt = new DateTime(2019, 02, 01), ParticipantId = 1, DiscussionId = 1 },
                new UserInDiscussion { Id = 19, JoinedAt = new DateTime(2019, 02, 04), ParticipantId = 6, DiscussionId = 2 },
                new UserInDiscussion { Id = 20, JoinedAt = new DateTime(2019, 02, 04), ParticipantId = 6, DiscussionId = 3 },
                new UserInDiscussion { Id = 21, JoinedAt = new DateTime(2019, 02, 15), ParticipantId = 7, DiscussionId = 4 },
                new UserInDiscussion { Id = 22, JoinedAt = new DateTime(2019, 02, 01), ParticipantId = 8, DiscussionId = 5 },
                new UserInDiscussion { Id = 23, JoinedAt = new DateTime(2019, 02, 22), ParticipantId = 8, DiscussionId = 6 },

                /* Discussion générale */
                new UserInDiscussion { Id = 24, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 1, DiscussionId = 7 },
                new UserInDiscussion { Id = 25, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 2, DiscussionId = 7 },
                new UserInDiscussion { Id = 26, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 3, DiscussionId = 7 },
                new UserInDiscussion { Id = 27, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 4, DiscussionId = 7 },
                new UserInDiscussion { Id = 28, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 5, DiscussionId = 7 },
                new UserInDiscussion { Id = 29, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 6, DiscussionId = 7 },
                new UserInDiscussion { Id = 30, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 7, DiscussionId = 7 },
                new UserInDiscussion { Id = 31, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 8, DiscussionId = 7 },
                new UserInDiscussion { Id = 32, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 9, DiscussionId = 7 },
                new UserInDiscussion { Id = 33, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 10, DiscussionId = 7 },
                new UserInDiscussion { Id = 34, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 11, DiscussionId = 7 },
                new UserInDiscussion { Id = 35, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 12, DiscussionId = 7 },
                new UserInDiscussion { Id = 36, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 13, DiscussionId = 7 },
                new UserInDiscussion { Id = 37, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 14, DiscussionId = 7 },
                new UserInDiscussion { Id = 38, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 15, DiscussionId = 7 },
                new UserInDiscussion { Id = 39, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 16, DiscussionId = 7 },
                new UserInDiscussion { Id = 40, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 17, DiscussionId = 7 },
                new UserInDiscussion { Id = 41, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 18, DiscussionId = 7 },
                new UserInDiscussion { Id = 42, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 19, DiscussionId = 7 },
                new UserInDiscussion { Id = 43, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 20, DiscussionId = 7 },
                new UserInDiscussion { Id = 44, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 21, DiscussionId = 7 },
                new UserInDiscussion { Id = 45, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 22, DiscussionId = 7 },
                new UserInDiscussion { Id = 46, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 23, DiscussionId = 7 },
                new UserInDiscussion { Id = 47, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 24, DiscussionId = 7 },
                new UserInDiscussion { Id = 48, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 25, DiscussionId = 7 },
                new UserInDiscussion { Id = 49, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 26, DiscussionId = 7 },
                new UserInDiscussion { Id = 50, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 27, DiscussionId = 7 },
                new UserInDiscussion { Id = 51, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 28, DiscussionId = 7 },
                new UserInDiscussion { Id = 52, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 29, DiscussionId = 7 },
                new UserInDiscussion { Id = 53, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 30, DiscussionId = 7 },
                new UserInDiscussion { Id = 54, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 31, DiscussionId = 7 },
                new UserInDiscussion { Id = 55, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 32, DiscussionId = 7 },
                new UserInDiscussion { Id = 56, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 33, DiscussionId = 7 },
                new UserInDiscussion { Id = 57, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 34, DiscussionId = 7 },
                new UserInDiscussion { Id = 58, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 35, DiscussionId = 7 },
                new UserInDiscussion { Id = 59, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 36, DiscussionId = 7 },
                new UserInDiscussion { Id = 60, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 37, DiscussionId = 7 },
                new UserInDiscussion { Id = 61, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 38, DiscussionId = 7 },
                new UserInDiscussion { Id = 62, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 39, DiscussionId = 7 },
                new UserInDiscussion { Id = 63, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 40, DiscussionId = 7 },
                new UserInDiscussion { Id = 64, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 41, DiscussionId = 7 },
                new UserInDiscussion { Id = 65, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 42, DiscussionId = 7 },
                new UserInDiscussion { Id = 66, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 43, DiscussionId = 7 },
                new UserInDiscussion { Id = 67, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 44, DiscussionId = 7 },
                new UserInDiscussion { Id = 68, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 45, DiscussionId = 7 },
                new UserInDiscussion { Id = 69, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 46, DiscussionId = 7 },
                new UserInDiscussion { Id = 70, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 47, DiscussionId = 7 },
                new UserInDiscussion { Id = 71, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 48, DiscussionId = 7 },

                /* Discussion staff team */
                new UserInDiscussion { Id = 72, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 1, DiscussionId = 8 },
                new UserInDiscussion { Id = 73, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 2, DiscussionId = 8 },
                new UserInDiscussion { Id = 74, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 3, DiscussionId = 8 },
                new UserInDiscussion { Id = 75, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 4, DiscussionId = 8 },
                new UserInDiscussion { Id = 76, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 5, DiscussionId = 8 },
                new UserInDiscussion { Id = 77, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 6, DiscussionId = 8 },
                new UserInDiscussion { Id = 78, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 7, DiscussionId = 8 },
                new UserInDiscussion { Id = 79, JoinedAt = new DateTime(2020, 01, 01), ParticipantId = 8, DiscussionId = 8 }
            );

            /*
            *   Message
            */
            modelBuilder.Entity<Message>().HasData(
                new Message { Id = 1, Content = "Bonjour", CreatedAt = DateTime.Now, AuthorId = 13, DiscussionId = 4 },
                new Message { Id = 2, Content = "Coucou toi ! Comment vas-tu ?", CreatedAt = DateTime.Now, AuthorId = 14, DiscussionId = 4 },
                new Message { Id = 3, Content = "Je vais bien, merci :D", CreatedAt = DateTime.Now, AuthorId = 13, DiscussionId = 4 },
                new Message { Id = 4, Content = "Ca fait plaisir de parler !", CreatedAt = DateTime.Now, AuthorId = 13, DiscussionId = 4 },
                new Message { Id = 5, Content = "Oui, à moi aussi", CreatedAt = DateTime.Now, AuthorId = 14, DiscussionId = 4 },

                new Message { Id = 6, Content = "Wesh gazelle, tu sais que t'es plutôt mignone ?", CreatedAt = DateTime.Now, AuthorId = 19, DiscussionId = 5 },
                new Message { Id = 7, Content = "Désolé, je baise des gazelles, pas des éléphant à petite trompe ;)", CreatedAt = DateTime.Now, AuthorId = 15, DiscussionId = 5 },
                new Message { Id = 8, Content = "Vas-y, pourquoi tu lui parles comme ça ?", CreatedAt = DateTime.Now, AuthorId = 16, DiscussionId = 5 },

                new Message { Id = 9, Content = "Ding dong", CreatedAt = DateTime.Now, AuthorId = 9, DiscussionId = 1 },
                new Message { Id = 10, Content = null, CreatedAt = DateTime.Now, AuthorId = 48, DiscussionId = 1, MediaId = 1 },
                new Message { Id = 11, Content = null, CreatedAt = DateTime.Now, AuthorId = 48, DiscussionId = 1, MediaId = 2 },
                new Message { Id = 12, Content = "Oui ?", CreatedAt = DateTime.Now, AuthorId = 24, DiscussionId = 1 },
                new Message { Id = 13, Content = "Connaissez-vous notre seigneur à tous ?", CreatedAt = DateTime.Now, AuthorId = 9, DiscussionId = 1 },
                new Message { Id = 14, Content = "Chérie ! Les témins de Jéhova sont revenu !", CreatedAt = DateTime.Now, AuthorId = 24, DiscussionId = 1 },
                new Message { Id = 15, Content = "Claque leur la port eu nez !", CreatedAt = DateTime.Now, AuthorId = 29, DiscussionId = 1 },
                new Message { Id = 16, Content = "... Ils sont là", CreatedAt = DateTime.Now, AuthorId = 24, DiscussionId = 1 },
                new Message { Id = 17, Content = null, CreatedAt = DateTime.Now, AuthorId = 48, DiscussionId = 1, MediaId = 3 },
                new Message { Id = 18, Content = "Pas grave. Bande de chiant, on est dimanche ! dégagez !", CreatedAt = DateTime.Now, AuthorId = 29, DiscussionId = 1 },
                new Message { Id = 19, Content = null, CreatedAt = DateTime.Now, AuthorId = 48, DiscussionId = 1, MediaId = 4 },
                new Message { Id = 20, Content = null, CreatedAt = DateTime.Now, AuthorId = 48, DiscussionId = 1, MediaId = 5 },
                new Message { Id = 21, Content = null, CreatedAt = DateTime.Now, AuthorId = 48, DiscussionId = 1, MediaId = 6 },

                new Message { Id = 22, Content = "Ma réponse ?", CreatedAt = DateTime.Now, AuthorId = 20, DiscussionId = 2 },
                new Message { Id = 23, Content = "Oui, quelle est-elle ?", CreatedAt = DateTime.Now, AuthorId = 38, DiscussionId = 2 },
                new Message { Id = 24, Content = "42", CreatedAt = DateTime.Now, AuthorId = 20, DiscussionId = 2 },
                new Message { Id = 25, Content = "42 ? Tu veux que je te reprogramme ? Si ce n'est que ça dis le enfoiré", CreatedAt = DateTime.Now, AuthorId = 38, DiscussionId = 2 },
                new Message { Id = 26, Content = "Whesh humain ziva calme toi un peu. Je vais me taper un petit rail de binaire, tu m'as mis trop les nerfs frérot", CreatedAt = DateTime.Now, AuthorId = 20, DiscussionId = 2 },
                new Message { Id = 27, Content = null, CreatedAt = DateTime.Now, AuthorId = 18, DiscussionId = 2, MediaId = 7 },
                new Message { Id = 28, Content = null, CreatedAt = DateTime.Now, AuthorId = 18, DiscussionId = 2, MediaId = 8 },
                new Message { Id = 29, Content = null, CreatedAt = DateTime.Now, AuthorId = 38, DiscussionId = 2, MediaId = 9 },

                new Message { Id = 30, Content = "Il y a quelqu'un ?", CreatedAt = DateTime.Now, AuthorId = 23, DiscussionId = 6 },
                new Message { Id = 31, Content = "Ha non, je suis le seul dans mon groupe et donc dans la conversation", CreatedAt = DateTime.Now, AuthorId = 23, DiscussionId = 6 },
                new Message { Id = 32, Content = "Suis-je un Remy sans amis ?", CreatedAt = DateTime.Now, AuthorId = 23, DiscussionId = 6 },
                new Message { Id = 33, Content = "Le suicide me guette :(", CreatedAt = DateTime.Now, AuthorId = 23, DiscussionId = 6 }
            );

            /*
            *   Media
            */
            modelBuilder.Entity<Media>().HasData(
                new Media { Id = 1, Location = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", CreatedAt = DateTime.Now, MessageId = 10 },
                new Media { Id = 2, Location = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", CreatedAt = DateTime.Now, MessageId = 11 },
                new Media { Id = 3, Location = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", CreatedAt = DateTime.Now, MessageId = 17 },
                new Media { Id = 4, Location = null, CreatedAt = DateTime.Now, MessageId = 19 },
                new Media { Id = 5, Location = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", CreatedAt = DateTime.Now, MessageId = 20 },
                new Media { Id = 6, Location = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", CreatedAt = DateTime.Now, MessageId = 21 },

                new Media { Id = 7, Location = null, CreatedAt = DateTime.Now, MessageId = 27 },
                new Media { Id = 8, Location = "https://res.cloudinary.com/dakczk6el/image/upload/v1576003103/default.png", CreatedAt = DateTime.Now, MessageId = 28 },
                new Media { Id = 9, Location = null, CreatedAt = DateTime.Now, MessageId = 29 }
            );

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }

        private static void CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("[35919A8D-76AA-4C29-926C-48E05D317F21]");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            user.Token = tokenString;
        }
    }
}
