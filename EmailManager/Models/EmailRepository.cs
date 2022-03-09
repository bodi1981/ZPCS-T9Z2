using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models
{
    public static class EmailRepository
    {
        public static Email GetEmail(int emailId, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Emails.Single(x => x.Id == emailId && x.UserId == userId);
            }
        }

        public static List<Email> GetEmails(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Emails.Where(x => x.UserId == userId).ToList();
            }
        }

        public static void AddEmail(Email email)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Emails.Add(email);
                dbContext.SaveChanges();
            }
        }

        public static void RemoveEmail(int emailId, string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var emailToDelete = dbContext.Emails.Single(x => x.Id == emailId && x.UserId == userId);
                dbContext.Emails.Remove(emailToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}