using Microsoft.AspNetCore.Mvc;
using BENKIEN.Data;
using Microsoft.EntityFrameworkCore;

namespace BENKIEN.ViewComponents
{
    public class Categories : ViewComponent // Bir class ın ViewComponent olabilmek için ViewComponent sınıfından miras almalıyız.
    {
        private readonly DatabaseContext _context;
         
        public Categories(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}
