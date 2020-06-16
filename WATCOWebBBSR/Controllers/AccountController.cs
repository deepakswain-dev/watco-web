using System.Web.Security;
using System.Web.Mvc;

namespace WATCOWebBBSR.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logoff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return View();
        }
    }
}