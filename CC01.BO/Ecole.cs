using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public  class Ecole
    {
        public string Name { get; set; }
        public int Tel { get; set; }
        public byte[] Logo { get; set; }
        public string Email { get; set; }

        public Ecole()
        {

        }

        public Ecole(string name, int tel, byte[] logo, string email)
        {
            Name = name;
            Tel = tel;
            Logo = logo;
            Email = email;
        }

        public Ecole(Ecole u)
        {
            Name = u.Name;
            Tel = u.Tel;
            Logo = u.Logo;
            Email = u.Email;

        }

        public override bool Equals(object obj)
        {
            return obj is Ecole ecole &&
                   Name == ecole.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
