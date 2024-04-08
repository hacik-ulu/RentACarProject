using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.AdminViewComponents
{
    public class _AdminFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
