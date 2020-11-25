using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
  public  class EcoleBLO
    {
        EcoleDAO EcoleRepo;
        public EcoleBLO(string dbFolder)
        {
            EcoleRepo = new EcoleDAO(dbFolder);
        }
        public void CreateEcole(Ecole ecole)
        {
            EcoleRepo.Add(ecole);
        }
        public void DeleteEcole(Ecole ecole)
        {
            EcoleRepo.Remove(ecole);
        }
        public IEnumerable<Ecole> GetAllEcoles()
        {
            return EcoleRepo.Find();
        }
        public IEnumerable<Ecole> GetByName(string Name)
        {
            return EcoleRepo.Find(x => x.Name == Name);
        }

        public IEnumerable<Ecole> GetBy(Func<Ecole, bool> predicate)
        {
            return EcoleRepo.Find(predicate);
        }

        public void EditUniversity(Ecole oldEcole, Ecole newEcole)
        {
            EcoleRepo.Set(oldEcole, newEcole);
        }
    }
}
