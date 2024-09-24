using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models.DTO
{
    public class CharacterDTO
    {
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; } 
        public int Defence { get; set; } 
        public RpgClass Class { get; set; }
    }
}
