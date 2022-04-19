using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Data
{
    public interface IPetShopRepository
    {
        IQueryable<Pet> Pet { get; }

    }
}
