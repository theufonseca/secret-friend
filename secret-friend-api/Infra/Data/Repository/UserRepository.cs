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
                "INSERT INTO user (userName, nickname, password, confirmationcode, endateConfirmation, confirmed, createdAt) " +
                "VALUES (@UserName, @NickName, @Password, @ConfirmationCode, @EndDateConfirmation, @Confirmed, @CreatedAt); " +
                "SELECT LAST_INSERT_ID();",
                new
                {
                    UserName = user.UserName,
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

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            var userDto = await _connection.QueryFirstOrDefaultAsync<UserDto>(
                                              "SELECT * FROM user WHERE userName = @UserName;",
                                              new { UserName = userName });

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
