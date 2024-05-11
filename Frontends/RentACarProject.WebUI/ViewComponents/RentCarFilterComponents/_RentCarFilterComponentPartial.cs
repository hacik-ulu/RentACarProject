using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.RentCarFilterComponents
{
    public class _RentCarFilterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
