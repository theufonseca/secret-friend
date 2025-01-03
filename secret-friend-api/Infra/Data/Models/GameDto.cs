﻿using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public int IdUserHost { get; set; }
        public string Name { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
        public bool IsFinished { get; set; }


        public Game GetGame()
        {
            return new Game(Id, IdUserHost, Name, MaxPrice, MinPrice, IsFinished);
        }
    }
}
