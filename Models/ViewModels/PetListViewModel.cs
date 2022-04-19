using PetShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models.ViewModels
{
    public class PetListViewModel
    {
        public IEnumerable<Pet> Pet { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentSpecies { get; set; }
    }
}
