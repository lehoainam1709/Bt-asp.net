using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PetShop.Data
{
    public class PetSpeciesViewModel
    {
        public List<Pet>? TenThuCung { get; set; }
        public SelectList? TenGiongLoai { get; set; }
        public string? GiongLoaiThuCung { get; set; }
        public string? KiTuTimKiem { get; set; }
    }
}
