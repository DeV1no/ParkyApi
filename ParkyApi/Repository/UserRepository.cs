using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Parky.Data;
using Parky.DTO;
using Parky.Entity;
using Parky.Repository.IRepository;
using ParkyApi;

namespace Parky.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public bool IsUniqueUser(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users
                .SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }


        public User Register(UserLoginDTO model)
        {
            var userObj = _mapper.Map<User>(model);
            userObj.Role = "User";
            bool isExisted = IsUniqueUser(userObj.Username);
            if (isExisted)
                return null;
            _context.Users.Add(userObj);
            _context.SaveChanges();
            return userObj;
        }
    }
}