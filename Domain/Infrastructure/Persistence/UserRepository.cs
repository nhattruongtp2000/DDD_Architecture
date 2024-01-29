using Application.Common.Interfaces.Persistence;
using Domain.Entites;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();

        public User? GetUserByEmail(string email)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.FirstOrDefault(p => p.Email == email);
                    if (user == null)
                    {
                        return null;
                    }
                    else
                    {
                        return user;
                    }
                    //else //remove existing order
                    //{
                    //    using (var trans = db.Database.BeginTransaction())
                    //    {
                    //        try
                    //        {
                    //            db.oRD_HEADERs.RemoveRange(ords);
                    //            db.SaveChanges();
                    //            trans.Commit();
                    //            return true;
                    //        }
                    //        catch (DbEntityValidationException e)
                    //        {
                    //            foreach (var eve in e.EntityValidationErrors)
                    //            {
                    //                foreach (var ve in eve.ValidationErrors)
                    //                {
                    //                }
                    //            }

                    //            trans.Rollback();
                    //            return false;
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            trans.Rollback();
                    //            return false;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RegisterUser(User user)
        {
            try
            {
                using(var db=new ApplicationDbContext())
                {
                    var userExist=db.Users.FirstOrDefault(x=>x.Email==user.Email);
                    if (userExist != null)
                    {
                        return false;
                    }

                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
