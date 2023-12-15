using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Game
    {
        public int Id { get; private set; }
        public int IdUserHost { get; private set; }
        public string Name { get; private set; }
        public int? MaxPrice { get; private set; }
        public int? MinPrice { get; private set; }
        public bool IsFinished { get; private set; }

        [JsonIgnore]
        public string GameCode { get; private set; }

        public Game(int id, int userIdHost, string name, int? maxValue, int? minValue, bool isFinished)
        {
            Id = id;
            AddIdUserHost(userIdHost);
            AddName(name);

            if(maxValue is not null)
                AddMaxValue(maxValue.Value);

            if(minValue is not null)
                AddMinValue(minValue.Value);

            IsFinished = isFinished;
        }

        public Game(int idUserHost, string name)
        {
            AddIdUserHost(idUserHost);
            AddName(name);
            GenerateGameCode();
        }

        public void AddIdUserHost(int idUserHost)
        {
            if (idUserHost <= 0)
                throw new Exception("IdUserHost is invalid");

            IdUserHost = idUserHost;
        }

        public void AddName(string name)
        {
            if (name.Length < 5)
                throw new Exception("Name is invalid");

            Name = name;
        }

        public void AddMaxValue(int maxValue)
        {
            if (maxValue < 0)
                throw new Exception("MaxValue is invalid");

            MaxPrice = maxValue;
        }

        public void AddMinValue(int minValue)
        {
            if (minValue < 0)
                throw new Exception("MinValue is invalid");

            MinPrice = minValue;
        }

        public void GenerateGameCode()
        {
            var guid = Guid.NewGuid().ToString().Replace("-","");
            GameCode = guid.Substring(0, 6);
        }
    }
}
