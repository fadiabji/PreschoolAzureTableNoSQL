using Microsoft.AspNetCore.Mvc;
using Preschool.Services;

namespace Preschool.ViewComponents
{
    public class ChildSubscriptionsHistoryViewComponent : ViewComponent
    {
        private readonly IChildService _childService;

        public ChildSubscriptionsHistoryViewComponent( IChildService childService)
        {
            _childService = childService;

        }
        public IViewComponentResult Invoke(int id)
        {
            var childSubscriptionsHistory = _childService.GetChildById(id).Result.Subscriptions.OrderByDescending(s => s.CreatedAt).ToList();
            return View("Index", childSubscriptionsHistory);
        }
    }
}
