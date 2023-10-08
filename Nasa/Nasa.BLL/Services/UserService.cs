using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nasa.BLL.Exceptions;
using Nasa.BLL.Services.Abstract;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO;
using Nasa.Common.DTO.User;
using Nasa.Common.Security;
using Nasa.DAL.Context;
using Nasa.DAL.Entities;

namespace Nasa.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(NasaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDto> CreateUserAsync(RegisterUserDto userDto)
        {
            if (await GetUserByEmailAsync(userDto.Email) is not null)
            {
                throw new EmailAlreadyExistException();
            }
            var userEntity = _mapper.Map<User>(userDto);
            var salt = SecurityHelper.GetRandomBytes();

            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.PasswordHash = SecurityHelper.HashPassword(userDto.Password, salt);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(userEntity);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                throw new NotFoundException(nameof(User));
            }

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
