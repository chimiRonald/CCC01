using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class EtudiantBLO
    {
        EtudiantDAO EtudiantRepo;
        public EtudiantBLO(string dbFolder)
        {
            EtudiantRepo = new EtudiantDAO(dbFolder);
        }
        public void CreateEtudiant(Etudiant etudiant)
        {
            EtudiantRepo.Add(etudiant);
        }
        public void DeleteEtudiant(Etudiant etudiant)
        {
            EtudiantRepo.Remove(etudiant);
        }
        public IEnumerable<Etudiant> GetAllEtudiants()
        {
            return EtudiantRepo.Find();
        }
        public IEnumerable<Etudiant> GetByMatricule(string matricule)
        {
            return EtudiantRepo.Find(x => x.Matricule == matricule);
        }

        public IEnumerable<Etudiant> GetBy(Func<Etudiant, bool> predicate)
        {
            return EtudiantRepo.Find(predicate);
        }

        public void EditEtudiant(Etudiant oldEtudiant, Etudiant newEtudiant)
        {
            EtudiantRepo.Set(oldEtudiant, oldEtudiant);
        }
    }
}
