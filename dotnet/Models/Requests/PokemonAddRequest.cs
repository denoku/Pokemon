using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests
{
    public class PokemonAddRequest
    {
        public int NationalPokédexNumber { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string PrimaryImageUrl { get; set; }
        public string Summary { get; set; }
        public bool Gender { get; set; }
        public int CategoryId { get; set; }
        public List<int> Abilities { get; set; }
        public List<int> Type { get; set; }
        public List<int> Weaknesses { get; set; }

    }
}
