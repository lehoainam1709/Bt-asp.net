using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Data;
using System;
using System.Linq;

namespace PetShop.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            PetShopContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<PetShopContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            // Kiểm tra thông tin Pet đã tồn tại hay chưa
            if (context.Pet.Any())
            {
                return; // Không thêm nếu Pet đã tồn tại trong DB
            }
            context.Pet.AddRange(
            new Pet
            {
                ThuCung = "Mèo Xiêm",
                ThoiGian = DateTime.Parse("2022-10-16"),
                GiongLoai = "Mèo",
                Gia = 500000,
                GioiTinh = "Đực",
                ProfilePicture = "https://sieupet.com/sites/default/files/meo_xiem_hoang_gia2.jpg"
            },
            new Pet
            {
                ThuCung = "Mèo Ragdoll",
                ThoiGian = DateTime.Parse("2022-8-3"),
                GiongLoai = "Mèo",
                Gia = 700000,
                GioiTinh = "Cái",
                ProfilePicture = "https://sieupet.com/sites/default/files/giong_meo_ragdoll9.jpg"
            }
            );
            context.SaveChanges();//lưu dữ liệu
        
        }
    }
}
