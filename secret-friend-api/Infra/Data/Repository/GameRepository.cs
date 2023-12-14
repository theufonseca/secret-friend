using Application.Interfaces;
using Dapper;
using domain.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly MySqlConnection _connection;
        public GameRepository(DataContext context)
        {
            this._connection = context.connection;
        }

        public async Task<int> AddGameAsync(Game game)
        {
            var id = await _connection.ExecuteScalarAsync<int>(
                               "INSERT INTO game (IdUserHost, name, minPrice, maxPrice, createdAt) " +
                               "VALUES (@IdUserHost, @Name, @MinValue, @MaxValue, @CreatedAt); " +
                               "SELECT LAST_INSERT_ID();",
                new
                {
                    IdUserHost = game.IdUserHost,
                    Name = game.Name,
                    MinValue = game.MinValue,
                    MaxValue = game.MaxValue,
                    CreatedAt = DateTime.Now
                });

            return id;
        }
    }
}
