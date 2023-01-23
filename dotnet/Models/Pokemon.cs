using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string NationalPokédexNumber { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string PrimaryImageUrl {get; set;}
        public string Summary { get; set; }
        public bool Gender { get; set; }
        public LookUp Category { get; set; }
        public List<Ability> Abilities { get; set; }
        public List<LookUp> Types { get; set; }
        public List<LookUp> Weaknesses { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
