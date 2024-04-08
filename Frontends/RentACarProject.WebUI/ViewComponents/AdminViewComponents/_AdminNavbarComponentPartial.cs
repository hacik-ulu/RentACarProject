using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.AdminViewComponents
{
    public class _AdminNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
