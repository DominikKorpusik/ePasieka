using System.Collections.Generic;

namespace ePasieka.Models
{
    public interface IBeeGardenRepository
    {
        IEnumerable<BeeGarden> AllBeeGardens();
        IEnumerable<string> AllBeeGardensByName();
        BeeGarden BeeGardenByID(int beeGardenID);
        void AddBeeGarden(BeeGarden beeGarden);
        void UpdateBeeGarden(BeeGarden beeGarden);
        void DeleteBeeGarden(int id);
    }
}
