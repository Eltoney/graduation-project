using System.Linq;
using GraduateProject.contexts;
using GraduateProject.httpModels;
using GraduateProject.httpModels.api.requests;
using GraduateProject.httpModels.api.response;
using GraduateProject.models;
using GraduateProject.utils;

namespace GraduateProject.services;

public interface IUserService
{
    /// <summary>
    /// Make the login operation by check UserName and Password and if matches create a session token and store it in database
    /// </summary>
    /// <param name="model">The User data model</param>
    /// <returns>Returns AuthenticateResult or null if not authorized login</returns>
    AuthenticateResponse? Authenticate(AuthenticateRequest model, out string message);

    IEnumerable<User> GetAll();
    User? GetById(int id);

    /// <summary>
    /// Register The User in Database and make a normal login after
    /// </summary>
    /// <param name="model">The User data model</param>
    /// <returns>Return The User Data</returns>
    AuthenticateResponse? Register(AuthenticateRequest model, out string message);

    void Update(int id, UpdateRequest model);
    void Delete(int id);

    /// <summary>
    /// To Check if user is authorized or not
    /// </summary>
    /// <param name="token">the session token of the user</param>
    /// <returns>The Authorized User or null if not authorized</returns>
    User? CheckAuthentication(string? token);

    /// <summary>
    /// To Signout the user and remove token from database
    /// </summary>
    /// <param name="session">User SignedIn Token</param>
    void SignOut(string? session);

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

            if (user != null && CryptUtils.StringToSHA256(model.Password) == user.Password)
            {
                message = "Success";

                var response = new AuthenticateResponse

                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Token = CommonUtils.CreateTokenSession(user)
                };
                UpdateAndAddToken(user, response.Token);
                return response;
            }

            message = "Incorrect UserName or Password";
            return null;
        }


        public AuthenticateResponse? Register(AuthenticateRequest model, out string? message)
        {
            if (!CommonUtils.CheckStrings(model.FirstName, model.Password, model.EmailAddress))
            {
                message = "Missing Variables";
                return null;
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
                LastName = model.LastName,
                EmailAddress = model.EmailAddress
            };
            _context.Users.Add(newUser);

            var isSucceed = _context.SaveChanges();

            if (isSucceed != 0)
            {
                String token = CommonUtils.CreateTokenSession(newUser);
                _context.Tokens.Add(new Token
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
            return _context.Set<User>();
        }

        public User? GetById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.userID == id);
        }


        public void Update(int id, UpdateRequest model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _context.Users.Remove(new User() {userID = id});
        }

        public User? CheckAuthentication(string? token)
        {
            if (token == null)
            {
                return null;
            }


            foreach (var contextToken in _context.Tokens)
            {
                Console.WriteLine(contextToken.Token1 == token);
            }

            var tokenUser = _context.Tokens.SingleOrDefault(t => t.Token1 == token);
            if (tokenUser == null)
            {
                return null;
            }

            if (tokenUser.CreatedDate.AddHours(7) >= DateTime.Now)
            {
                return _context.Users.SingleOrDefault(u => u.userID == tokenUser.UserId);
            }

            _context.Tokens.Remove(tokenUser);
            _context.SaveChanges();
            return null;
        }

        public void SignOut(string? token)
        {
            var t = _context.Tokens.SingleOrDefault(tt => tt.Token1 == token);
            if (t == null) return;
            _context.Tokens.Remove(t);
            _context.SaveChanges();
        }

        private void UpdateAndAddToken(User user, string t)
        {
            var tokens = _context.Tokens.Where(token => token.UserId == user.userID);
            foreach (var token in tokens)
            {
                if (token.CreatedDate >= DateTime.Now) continue;
                _context.Tokens.Remove(token);
            }

            var newToken = new Token()
            {
                UserId = user.userID,
                CreatedDate = DateTime.Now,
                Token1 = t
            };
            _context.Tokens.Add(newToken);
            _context.SaveChanges();
        }
    }
}