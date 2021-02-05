using System.Collections.Generic;

namespace ePasieka.Models
{
    public interface IBeehiveRepository
    {
        IEnumerable<Beehive> AllBeehives();
        IEnumerable<Beehive> BeehivesByBeeGarden(int id);
        Beehive BeehiveByID(int beehiveID);
        void AddBeehive(Beehive beehive, int howMany);
        void UpdateBeehive(Beehive beehive);
        void DeleteBeehive(int id);
    }
}
