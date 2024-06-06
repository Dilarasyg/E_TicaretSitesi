using Microsoft.AspNetCore.Mvc;

namespace ETicaret.ViewComponents.Categories
{
    public class CategoryIndex : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
