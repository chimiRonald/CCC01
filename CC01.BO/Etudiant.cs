using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Etudiant: Ecole
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailS { get; set; }
        public int TelS { get; set; }
        public string Sexe { get; set; }
        public DateTime BornOn { get; set; }
        public string BornAt { get; set; }
        public byte[] Photo { get; set; }
        public string Matricule { get; set; }

        public static int count = 0;

        public Etudiant()
        {

        }

        public Etudiant(string firstName, string lastName, string emailS, int telS, string sexe, DateTime bornOn, string bornAt, byte[] photo, Ecole u)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailS = emailS;
            TelS = telS;
            Sexe = sexe;
            BornOn = bornOn;
            BornAt = bornAt;
            Photo = photo;
            Matricule = $"{FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";

        }

        public Etudiant(Etudiant e, Ecole s)
            : base(s.Name, s.Tel, s.Logo, s.Email)
        {
            FirstName = e.FirstName;
            LastName = e.LastName;
            EmailS = e.EmailS;
            TelS = e.TelS;
            Sexe = e.Sexe;
            BornOn = e.BornOn;
            BornAt = e.BornAt;
            Photo = e.Photo;
            Matricule = $"{FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";

        }

        public override bool Equals(object obj)
        {
            return obj is Etudiant student &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   Matricule == student.Matricule;
        }

        public override int GetHashCode()
        {
            int hashCode = -1381900569;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Matricule);
            return hashCode;
        }
    }
}
