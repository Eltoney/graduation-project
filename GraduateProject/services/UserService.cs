using System.Linq;
using GraduateProject.contexts;
using GraduateProject.httpModels;
using GraduateProject.httpModels.response;
using GraduateProject.models;
using GraduateProject.utils;
using Microsoft.VisualBasic.CompilerServices;

namespace GraduateProject.services;

public interface IUserService
{
    AuthenticateResponse? Authenticate(AuthenticateRequest model, out string message);
    IEnumerable<User> GetAll();
    User GetById(int id);
    AuthenticateResponse? Register(AuthenticateRequest model, out string s);
    void Update(int id, UpdateRequest model);
    void Delete(int id);


    class UserService : IUserService
    {
        private DetectionProjectContext _context;

        public UserService(DetectionProjectContext context)
        {
            _context = context;
   
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest model, out string message)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username);

            if (user != null && CryptUtils.StringToSHA256(user.Password) == model.Password)
            {
                message = "Success";
                return new AuthenticateResponse

                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Token = JwtUtils.GenerateToken(user)
                };
            }

            message = "Incorrect UserName or Password";
            return null;
        }


        public AuthenticateResponse? Register(AuthenticateRequest model, out string? message)
        {
            if (CommonUtils.CheckStrings(model.FirstName, model.Password, model.EmailAddress))
            {
                message = "Missing Variables";
            }

            var tmpUser = _context.Users.SingleOrDefault(u => model.Username == u.UserName);
            if (tmpUser != null)
            {
                message = "UserName Is Already Taken";
                return null;
            }

            var newUser = new User()
            {
                UserName = model.Username,
                Password = CryptUtils.StringToSHA256(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            _context.Users.Add(newUser);

            var isSucceed = _context.SaveChanges();

            if (isSucceed != 0)
            {
                String token = CommonUtils.CreateTokenSession(newUser);
                _context.Tokens.Add(new Token()
                {
                    Token1 = token,
                    UserId = newUser.userID
                });
                _context.SaveChanges();

                message = "Success";
                return new AuthenticateResponse()
                {
                    Token = token,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Username = newUser.UserName
                };
            }
            message = "Failed To Register Please Try Again Later";
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }


        public void Update(int id, UpdateRequest model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}