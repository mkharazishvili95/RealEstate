using RealEstate.Application.Helpers;
using RealEstate.Application.Models.Auth;
using RealEstate.Core.User;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }


        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool Register(UserRegisterModel model)
        {
            if (EmailExists(model.Email))
                return false;

            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                PIN = GenerateUniquePIN(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                RegisterDate = DateTime.UtcNow,
                BlockDate = null,
                BlockReason = null,
                IsBlocked = false,
                Balance = 0,
                Type = Common.Enums.User.UserType.Individual
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public string GenerateUniquePIN()
        {
            Random random = new Random();
            string pin;
            do
            {
                pin = random.Next(1000, 999999999).ToString();
            } while (_context.Users.Any(u => u.PIN == pin));

            return pin;
        }
    }
}
