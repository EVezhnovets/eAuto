using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eAuto.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartService<ShoppingCartDataModel> _shoppingCartService;
        public ShoppingCartViewComponent(IShoppingCartService<ShoppingCartDataModel> shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(WebConstants.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(WebConstants.SessionCart));
                }
                else
                {
                    var result = await _shoppingCartService.GetShoppingCartModelsAsync(claim);
                    HttpContext.Session.SetInt32(WebConstants.SessionCart, result.Count());
                        
                    return View(HttpContext.Session.GetInt32(WebConstants.SessionCart));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}