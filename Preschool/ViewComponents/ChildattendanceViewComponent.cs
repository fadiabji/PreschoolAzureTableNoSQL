using Microsoft.AspNetCore.Mvc;
using Preschool.Services;

namespace Preschool.ViewComponents
{
    public class ChildattendanceViewComponent : ViewComponent
    {
        private readonly IChildService _childService;
        public ChildattendanceViewComponent( IChildService childService)
        {
                _childService = childService;
        }

        public IViewComponentResult Invoke( int id)
        {
            var childattendnce = _childService.GetChildById(id).Result.Attendances.OrderBy(a => a.Date).ToList();
            return View("Index", childattendnce);
        }
    }
}
