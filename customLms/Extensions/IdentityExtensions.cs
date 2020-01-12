using customLms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace customLms.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetuserFirstName(this IIdentity identity)

        {
            var db = ApplicationDbContext.Create();
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(identity.Name));

            return user != null ? user.name : string.Empty;

        }
        //genjamz ase

        public static async Task GetUser(this List<UserViewModel>user)
        {
            var db = ApplicationDbContext.Create();
            user.AddRange(await (from u in db.Users
                                 select new UserViewModel
                                 {


                                     id = u.Id,
                                     email = u.Email,
                                     Name = u.name




                                 }).OrderBy(o => o.email).ToListAsync());


        }





    }
}