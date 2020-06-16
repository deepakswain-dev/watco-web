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
    public class UsersEntityModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserFullName { get; set; }

        public string UserPassword { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public string CityName { get; set; }

        public int CityId { get; set; }
    }
}
