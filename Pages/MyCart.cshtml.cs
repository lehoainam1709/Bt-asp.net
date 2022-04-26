using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShop.MyTagHelper;
using PetShop.Data;
using System.Linq;
namespace PetShop.Pages
{
    public class MyCartModel : PageModel
    {
        private IPetShopRepository repository;
        public MyCartModel(IPetShopRepository repo)
        {
            repository = repo;
        }
        public MyCart myCart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            myCart = HttpContext.Session.GetJson<MyCart>("mycart") ?? new MyCart();
        }
        public IActionResult OnPost(long Id, string returnUrl)
        {
            Pet pet = repository.Pet
            .FirstOrDefault(b => b.Id == Id);
            myCart = HttpContext.Session.GetJson<MyCart>("mycart") ?? new MyCart();
            myCart.AddItem(pet, 1);
            HttpContext.Session.SetJson("mycart", myCart);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}