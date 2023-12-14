using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Game
    {
        public int Id { get; private set; }
        public int IdUserHost { get; private set; }
        public string Name { get; private set; }
        public int? MaxValue { get; private set; }
        public int? MinValue { get; private set; }
        public bool IsFinished { get; private set; }

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

            MaxValue = maxValue;
        }

        public void AddMinValue(int minValue)
        {
            if (minValue < 0)
                throw new Exception("MinValue is invalid");

            MinValue = minValue;
        }
    }
}
