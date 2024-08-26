using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Repository
{
    public class UserRepository
    {
        AppDbConext conext;
        public UserRepository(AppDbConext appDb)
        {
            conext = appDb;
        }
       
    }
}
