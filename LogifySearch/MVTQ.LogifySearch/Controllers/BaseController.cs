using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVTQ.LogifySearch.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
