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
                "INSERT INTO user (userName, nickname, password, createdAt) " +
                "VALUES (@UserName, @NickName, @Password, @CreatedAt); " +
                "SELECT LAST_INSERT_ID();",
                new
                {
                    UserName = user.UserName,
                    NickName = user.Nickname,
                    Password = user.Password,
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

        public async Task<User?> GetUserById(int userId)
        {
            var user = await _connection.QueryFirstOrDefaultAsync<UserDto>(
                                "SELECT * FROM user WHERE id = @UserId;",
                                new { UserId = userId });

            if (user is null)
                return null;

            return user.GetUser();
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

        public async Task UpdateUser(int Id, User user)
        {
            await _connection.ExecuteAsync(
                "UPDATE user SET " +
                "nickname = @Nickname, " +
                "password = @Password  " +
                "WHERE id = @Id;",
                new
                {
                    Id = Id,
                    Nickname = user.Nickname,
                    Password = user.Password,
                });
        }
    }
}
