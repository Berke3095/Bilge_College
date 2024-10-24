using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UserManager<User> userManager = new UserManager<User>();
        }
    }
}