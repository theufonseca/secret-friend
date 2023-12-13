using Application.Interfaces;
using domain.Entities;
using MySqlConnector;
using Dapper;
using Infra.Data.Models;

namespace Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlConnection _connection;
        public UserRepository(DataContext context)
        {
            this._connection = context.connection;
        }

        public async Task<int> AddUserAsync(User user)
        {
            var id = await _connection.ExecuteScalarAsync<int>(
                "INSERT INTO user (phonenumber, nickname, password, confirmationcode, endateConfirmation, confirmed, createdAt) " +
                "VALUES (@PhoneNumber, @NickName, @Password, @ConfirmationCode, @EndDateConfirmation, @Confirmed, @CreatedAt); " +
                "SELECT LAST_INSERT_ID();",
                new
                {
                    PhoneNumber = user.PhoneNumber,
                    NickName = user.Nickname,
                    Password = user.Password,
                    ConfirmationCode = user.ConfirmationCode,
                    EndDateConfirmation = user.EndDateConfirmation,
                    Confirmed = user.Confirmed,
                    CreatedAt = DateTime.Now
                });

            return id;
        }

        public async Task<User?> GetUserByConfirmCodeAsync(string confirmCode)
        {
            var userDto = await _connection.QueryFirstOrDefaultAsync<UserDto>(
                               "SELECT * FROM user WHERE confirmationcode = @ConfirmCode;",
                               new { ConfirmCode = confirmCode });

            if (userDto is null)
                return null;

            return userDto.GetUser();
        }

        public async Task<User?> GetUserByPhoneAsync(long phoneNumber)
        {
            var userDto = await _connection.QueryFirstOrDefaultAsync<UserDto>(
                                              "SELECT * FROM user WHERE phonenumber = @PhoneNumber;",
                                              new { PhoneNumber = phoneNumber });

            if (userDto is null)
                return null;

            return userDto.GetUser();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserToConfirmedAsync(long phoneNumber)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE user SET confirmed = 1 WHERE phonenumber = @PhoneNumber;",
                new { PhoneNumber = phoneNumber });
        }
    }
}
