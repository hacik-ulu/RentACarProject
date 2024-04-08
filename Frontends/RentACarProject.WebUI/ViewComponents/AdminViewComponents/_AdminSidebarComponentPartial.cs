using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.AdminViewComponents
{
    public class _AdminSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
