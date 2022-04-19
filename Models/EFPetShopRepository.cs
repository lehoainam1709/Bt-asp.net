using System.Linq;
namespace PetShop.Data
{
    public class EFPetShopRepository : IPetShopRepository
    {
        private PetShopContext context;
        public EFPetShopRepository(PetShopContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Pet> Pet => context.Pet;
    }
}
