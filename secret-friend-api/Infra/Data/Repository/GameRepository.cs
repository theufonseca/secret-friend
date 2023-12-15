using Application.Interfaces;
using Dapper;
using domain.Entities;
using Infra.Data.Models;
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
                               "INSERT INTO game (IdUserHost, name, minPrice, maxPrice, createdAt, gameCode) " +
                               "VALUES (@IdUserHost, @Name, @MinValue, @MaxValue, @CreatedAt, @gameCode); " +
                               "SELECT LAST_INSERT_ID();",
                new
                {
                    IdUserHost = game.IdUserHost,
                    Name = game.Name,
                    MinValue = game.MinPrice,
                    MaxValue = game.MaxPrice,
                    CreatedAt = DateTime.Now,
                    gameCode = game.GameCode
                });

            return id;
        }

        public async Task<Game?> GetByGameCode(string gameCode)
        {
            var gameDto = await _connection.QueryFirstOrDefaultAsync<GameDto>(
                               "SELECT * FROM game WHERE gameCode = @GameCode and IsFinished = 0",
                               new { GameCode = gameCode });

            if (gameDto is null)
                return null;

            return gameDto.GetGame();
        }

        public async Task<Game?> GetById(int idGame)
        {
            var gameDto = await _connection.QueryFirstOrDefaultAsync<GameDto>(
                "SELECT * FROM game WHERE id = @Id",
                new { Id = idGame });

            if (gameDto is null)
                return null;

            return gameDto.GetGame();
        }

        public async Task<List<Game>> GetGamesByUserId(int userId)
        {
            var games = await _connection.QueryAsync<GameDto>(
                               "SELECT distinct g.* FROM game g inner join participants p on p.IdGame = g.id  WHERE g.idUserHost = @userId or p.IdUser = @userId",
                               new { userId = userId });

            return games.Select(g => g.GetGame()).ToList();
        }

        public async Task UpdateGamePrice(int gameId, int? minPrice, int? maxPrice)
        {
            await _connection.ExecuteAsync(
                "UPDATE game SET minPrice = @MinPrice, maxPrice = @MaxPrice WHERE id = @Id",
                new { MinPrice = minPrice, MaxPrice = maxPrice, Id = gameId });
        }

        public async Task UpdateGameStatus(int id, bool isFinished)
        {
             await _connection.ExecuteAsync(
                "UPDATE game SET isFinished = @IsFinished WHERE id = @Id",
                new { Id = id, IsFinished = isFinished });
        }

        public async Task UpdateNewGameCode(int id, string newGameCode)
        {
            await _connection.ExecuteAsync(
                "UPDATE game SET gameCode = @GameCode WHERE id = @Id",
                new { Id = id, GameCode = newGameCode });
        }
    }
}
