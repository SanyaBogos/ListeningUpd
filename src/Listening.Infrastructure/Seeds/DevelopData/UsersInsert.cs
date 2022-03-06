using Listening.Core.Entities.Custom;
using Listening.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.DevelopData
{
    public static class UsersInsert
    {
        public static Dictionary<ApplicationUser, string> GetUsersWithRoles()
        {
            var dict = new Dictionary<ApplicationUser, string>
            {
                { CreateNewUser("super@super.com", "Super first", "Super last"), GlobalConstats.SUPER }
            };

            for (int i = 1; i <= 3; i++)
                dict.Add(CreateNewUser($"admin{i}@admin.com", $"Admin_{i} first", $"Admin_{i} last"), GlobalConstats.ADMIN);

            for (int i = 1; i <= 3; i++)
                dict.Add(CreateNewUser($"user{i}@admin.com", $"User_{i} first", $"User_{i} last"), GlobalConstats.USER);

            return dict;
        }

        private static ApplicationUser CreateNewUser(string email, string firstName, string lastName)
        {
            return new ApplicationUser
            {
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                IsEnabled = true,
            };
        }
    }
}
