using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
