#region Copyright
/******************************************************************************
* Copyright (C) 2018-2022 MicroGlobalTranz - All Rights Reserved.
*
* Proprietary and confidential. Unauthorized copying of this file, via any
* medium is strictly prohibited without the explicit permission of MicroGlobalTranz.
* Powered By Deepak Swain
******************************************************************************/
#endregion Copyright

using ODLSystem.BusinessLayer.Repository.Intrefaces;
using ODLSystem.Library.Common.EntityModel;
using WATCOWebBBSR.ObjectFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WATCOWebBBSR.Models
{
    public class CityAuthorizationPrivilegeFilter : ActionFilterAttribute
    {
        public CityAuthorizationPrivilegeFilter()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CoreUserAuthorizationDetail.ActualCityId != CoreUserAuthorizationDetail.SelectedCityId)
            {
                Controller controller = filterContext.Controller as Controller;

                if (controller != null)
                {
                    controller.HttpContext.Response.Redirect("./Login");
                }
            }
           
            base.OnActionExecuting(filterContext);
        }
    }
}