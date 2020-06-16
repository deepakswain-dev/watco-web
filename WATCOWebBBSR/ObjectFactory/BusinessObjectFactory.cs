#region Copyright
/******************************************************************************
* Copyright (C) 2018-2022 MicroGlobalTranz - All Rights Reserved.
*
* Proprietary and confidential. Unauthorized copying of this file, via any
* medium is strictly prohibited without the explicit permission of MicroGlobalTranz.
* Powered By Deepak Swain
******************************************************************************/
#endregion Copyright

using ODLSystem.BusinessLayer.Repository;
using ODLSystem.BusinessLayer.Repository.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATCOWebBBSR.ObjectFactory
{
    public class BusinessObjectFactory
    {
        public static IAuthenticationBusinessRepository GetAuthenticationBusinessRepositoryObject()
        {
            return new AuthenticationBusinessRepository();
        }
    }
}