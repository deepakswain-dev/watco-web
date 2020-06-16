#region Copyright
/******************************************************************************
* Copyright (C) 2018-2022 MicroGlobalTranz - All Rights Reserved.
*
* Proprietary and confidential. Unauthorized copying of this file, via any
* medium is strictly prohibited without the explicit permission of MicroGlobalTranz.
* Powered By Deepak Swain
******************************************************************************/
#endregion Copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.Library.Common.EntityModel
{
    public static class CoreUserAuthorizationDetail
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static int ActualCityId { get; set; }
        public static int SelectedCityId { get; set; }
        public static string SelectedCityName { get; set; }
    }
}
