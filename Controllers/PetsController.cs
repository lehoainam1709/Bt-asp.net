using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;

namespace PetShop.Controllers
{
    public class PetsController : Controller
    {
        private readonly PetShopContext _context;

        public PetsController(PetShopContext context)
        {
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index(string GiongLoaiThuCung, string KiTuTimKiem, string minPrice, string maxPrice, string StartDate, string EndDate)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from b in _context.Pet
                                            orderby b.GiongLoai
                                            select b.GiongLoai;
            var pets = from b in _context.Pet
                        select b;
            if (!string.IsNullOrEmpty(KiTuTimKiem))
            {
                pets = pets.Where(s => s.ThuCung!.Contains(KiTuTimKiem));
            }
            if (!string.IsNullOrEmpty(GiongLoaiThuCung))
            {
                pets = pets.Where(x => x.GiongLoai == GiongLoaiThuCung);
            }
            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = int.Parse(minPrice);
                pets = pets.Where(b => b.Gia >= min);
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = int.Parse(maxPrice);
                pets = pets.Where(b => b.Gia <= max);
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                var startdate = DateTime.Parse(StartDate);
                pets = pets.Where(b => b.NgayMua >= startdate);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var enddate = DateTime.Parse(EndDate);
                pets = pets.Where(b => b.NgayMua <= enddate);
            }
            var PetSpeciesVM = new PetSpeciesViewModel
            {
                TenGiongLoai = new SelectList(await
           genreQuery.Distinct().ToListAsync()),
                TenThuCung = await pets.ToListAsync()
            };
            return View(PetSpeciesVM);
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ThuCung,NgayMua,GiongLoai,Gia,GioiTinh,ProfilePicture")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ThuCung,NgayMua,GiongLoai,Gia,GioiTinh,ProfilePicture")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            _context.Pet.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.Id == id);
        }
    }
}
