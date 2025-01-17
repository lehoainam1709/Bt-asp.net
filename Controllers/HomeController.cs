﻿using Microsoft.AspNetCore.Mvc;
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
        public int PageSize = 4;
        private IPetShopRepository repository;

        public string CurrentSpecies { get; private set; }
        public IQueryable<Pet> Pet { get; private set; }

        public HomeController(IPetShopRepository repo)
        {
            repository = repo;
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult ThuCung(string SearchString, string MinPrice, string MaxPrice, string species, int ProductPage = 1)
        {
            var pets = from b in repository.Pet
                       select b;
            if (!string.IsNullOrEmpty(MinPrice))
            {
                var min = int.Parse(MinPrice);
                pets = pets.Where(b => b.Gia >= min);
            }

            if (!string.IsNullOrEmpty(MaxPrice))
            {
                var max = int.Parse(MaxPrice);
                pets = pets.Where(b => b.Gia <= max);
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                pets = pets.Where(s => s.ThuCung!.Contains(SearchString));
            }
            var PetListVM = new PetListViewModel
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
            };
            
            return View(PetListVM);
        }

        public async Task<IActionResult> Index()
        {
            var pets = from b in repository.Pet
                        select b;
           
            return View(await pets.ToListAsync());          
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
