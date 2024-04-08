using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.AdminViewComponents
{
    public class _AdminScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
