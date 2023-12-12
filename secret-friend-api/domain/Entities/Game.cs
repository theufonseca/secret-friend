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
    }
}
