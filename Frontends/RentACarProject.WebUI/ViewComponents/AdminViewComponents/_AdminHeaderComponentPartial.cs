﻿using Microsoft.AspNetCore.Mvc;

namespace RentACarProject.WebUI.ViewComponents.AdminViewComponents
{
    public class _AdminHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
