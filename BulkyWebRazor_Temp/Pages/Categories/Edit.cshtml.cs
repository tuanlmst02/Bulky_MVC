using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category? Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            if (id != 0 || id != null)
            {
                Category = _db.Categories.Where(item => item.Id == id).SingleOrDefault();
            }
        }

        public IActionResult OnPost()
        { 
            _db.Update(Category);
            _db.SaveChanges();
            TempData["success"] = "Category modified successfully";
            return RedirectToPage("Index");
        }
    }
}
