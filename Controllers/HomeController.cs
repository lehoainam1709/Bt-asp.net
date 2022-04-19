using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetShop.Data;
using PetShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 3;
        private IPetShopRepository repository;
        public HomeController(IPetShopRepository repo)
        {
            repository = repo;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult ThuCung(string species, int ProductPage = 1)
        => View(new PetListViewModel
        {
            Pet = repository.Pet
               .Where(p => species == null || p.GiongLoai == species)
               .OrderBy(p => p.Id)
               .Skip((ProductPage - 1) * PageSize)
               .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = ProductPage,
                ItemsPerPage = PageSize,
                TotalItems = species == null ?
                repository.Pet.Count() :
                repository.Pet.Where(e => e.GiongLoai == species).Count()
            },
            CurrentSpecies = species
        });

        public async Task<IActionResult> Index(string searchString)
        {
            var pets = from b in repository.Pet
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                pets = pets.Where(s => s.ThuCung!.Contains(searchString));
            }
            return View(await pets.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
