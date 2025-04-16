using insuranceApp1.Models;

namespace insuranceApp1.Repositories
{
    public class UsersRepository
    {
        InsuranceDbContext utx;
        public UsersRepository(InsuranceDbContext utx)
        {
            this.utx = utx;
        }

        public bool IsValidUser(string userName, string psw, string role)
        {
            bool b = false;
            Users user = utx.Usersall.Where(u => u.UserName == userName && u.Password == psw && u.Role == role).FirstOrDefault();
            if (user != null)
            {
                b = true;
            }
            return b;
        }

        public bool Add(Users user)
        {
            utx.Usersall.Add(user);
            int r = utx.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserExists(string username)
        {
            return utx.Usersall.Any(u => u.UserName == username);
        }

        // Method to add a new user
        public void AddUser(string userName, string psw, string role)
        {
            var user = new Users { UserName = userName, Password = psw, Role = role  };
            utx.Usersall.Add(user);
            utx.SaveChanges();
        }
    }
}