using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concretes;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business.Concretes
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                NationaltyId = userForRegisterDto.NationaltyId,
                MobileNumber = userForRegisterDto.MobileNumber,
                DateOfBirth = userForRegisterDto.DateOfBirth,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            var result = _userService.Add(user);
            if (result.Success)
            {
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }

            return new ErrorDataResult<User>(result.Message);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IDataResult<User> PasswordUpdate(UserForPasswordUpdateDto userForPasswordUpdateDto, string newPassword)
        {
            var userToCheck = _userService.GetByMail(userForPasswordUpdateDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForPasswordUpdateDto.OldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword,out passwordHash,out passwordSalt);
            User user = new User
            {
                Id = userToCheck.Id,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var result = _userService.Update(user);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.PasswordChanged);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            if (user==null)
            {
                return new ErrorDataResult<AccessToken>(Messages.AccessTokenDontCreated);
            }
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
