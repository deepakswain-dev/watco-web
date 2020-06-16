using System;
using System.Web.Mvc;
using System.Web.Security;

using ODLSystem.BusinessLayer.Repository.Intrefaces;
using ODLSystem.Library.Common.EntityModel;
using WATCOWebBBSR.ObjectFactory;

namespace WATCOWebBBSR.Controllers
{
    public class LoginController : Controller
    {
        IAuthenticationBusinessRepository _IAuthenticationBusinessRepository;

        public LoginController()
        {
            _IAuthenticationBusinessRepository = BusinessObjectFactory.GetAuthenticationBusinessRepositoryObject();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateUserCredential(string username, string password, string cityVal, string rememberMe)
        {
            string validateUser = String.Empty;
            UsersEntityModel objUsersEntityModel = new UsersEntityModel();
            try
            {
                objUsersEntityModel = _IAuthenticationBusinessRepository.ValidateUser(username, password);

                if (Convert.ToInt32(cityVal) == 0)
                {
                    validateUser = "You have to select Project Type";
                }
                else if (objUsersEntityModel.UserId == 0)
                {
                    validateUser = "Wrong username or password, please retry.";
                }
                else if (objUsersEntityModel.CityId != Convert.ToInt32(cityVal))
                {
                    validateUser = "You do not have authorize to this page";
                }
                else
                {
                    validateUser = cityVal == "1" ? "Cuttack" : "Bhubaneswar";
                    FormsAuthentication.SetAuthCookie(objUsersEntityModel.UserName, false);
                    //CoreUserAuthorizationDetail.UserName = objUsersEntityModel.UserName;
                    //CoreUserAuthorizationDetail.Password = objUsersEntityModel.UserPassword;
                    //CoreUserAuthorizationDetail.ActualCityId = objUsersEntityModel.CityId;
                    //CoreUserAuthorizationDetail.SelectedCityId = Convert.ToInt32(cityVal);
                    CoreUserAuthorizationDetail.SelectedCityName = "Bhubaneswar";
                    Session["Region"] = "Bhubaneswar";
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return Content(validateUser);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}