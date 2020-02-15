using IndiviualLabA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndiviualLabA.Repositories
{
    public class CustomUserRepo
    {
        ApplicationDbContext _context;

        public CustomUserRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public CustomUser GetUser(string userName)
        {
            CustomUser customUser = this._context.CustomUsers
                                        .Where(u => u.UserName == userName)
                                        .FirstOrDefault();
            if (customUser == null)
            {
                customUser = new CustomUser();
                customUser.UserName = userName;
            }
            return customUser;
        }

        public string UpdateProfile(CustomUser updatedCustomUser)
        {
            try
            {
                var identityUser = this._context.Users.Where(u => u.UserName.ToLower()
                                                == updatedCustomUser.UserName.ToLower())
                                                                    .FirstOrDefault();
                // Ensure user is in the identity table.
                if (identityUser != null)
                {

                    // Get current custom user.
                    var customUser = this._context.CustomUsers
                                            .Where(
                                                u => u.UserName == identityUser.UserName)
                                            .FirstOrDefault();

                    // Create CustomUser entry if it does not exist yet.
                    if (customUser == null)
                    {
                        customUser = new CustomUser()
                        {
                            UserName = updatedCustomUser.UserName,
                            FirstName = updatedCustomUser.FirstName,
                            LastName = updatedCustomUser.LastName
                        };
                        this._context.CustomUsers.Add(customUser);
                        this._context.SaveChanges();
                    }
                    // CustomUser already exists so update it.
                    else
                    {
                        customUser.FirstName = updatedCustomUser.FirstName;
                        customUser.LastName = updatedCustomUser.LastName;
                        this._context.CustomUsers.Update(customUser);
                        this._context.SaveChanges();
                    }
                }
                else
                {
                    return "User not found.";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "OK";
        }
    }

}
