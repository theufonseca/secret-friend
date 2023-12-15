using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGameRepository
    {
        Task<int> AddGameAsync(Game game);
        Task<Game?> GetById(int idGame);
        Task<Game?> GetByGameCode(string gameCode);
        Task<List<Game>> GetGamesByUserId(int userId);
        Task UpdateGamePrice(int gameId, int? minPrice, int? maxPrice);
        Task UpdateGameStatus(int id, bool isFinished);
        Task UpdateNewGameCode(int id, string newGameCode);
    }
}
