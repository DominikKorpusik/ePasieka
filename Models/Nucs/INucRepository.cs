using System.Collections.Generic;

namespace ePasieka.Models
{
    public interface INucRepository
    {
        IEnumerable<Nuc> AllNucs();
        IEnumerable<Nuc> NucsByBeeGarden(int id);
        Nuc NucsByID(int id);
        void AddNuc(Nuc nuc);
        void UpdateNuc(Nuc nuc);
        void DeleteNuc(int id);
    }
}
