using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int IdUserHost { get; set; }
        public string Name { get; set; }
        public int? MaxValue { get; set; }
        public int? MinValue { get; set; }
        public bool IsFinished { get; set; }

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
