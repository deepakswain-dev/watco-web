#region Copyright
/******************************************************************************
* Copyright (C) 2018-2022 MicroGlobalTranz - All Rights Reserved.
*
* Proprietary and confidential. Unauthorized copying of this file, via any
* medium is strictly prohibited without the explicit permission of MicroGlobalTranz.
* Powered By Deepak Swain
******************************************************************************/
#endregion Copyright

using ODLSystem.BusinessLayer.ObjectFactory;
using ODLSystem.BusinessLayer.Repository.Intrefaces;
using ODLSystem.Library.Common.EntityModel;
using ODLSystem.PersistenceLayer.Repository.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.BusinessLayer.Repository
{
    public class AuthenticationBusinessRepository : IAuthenticationBusinessRepository
    {
        IAuthenticationPersistenceRepository _IAuthenticationPersistenceRepository;

        public AuthenticationBusinessRepository()
        {
            _IAuthenticationPersistenceRepository = PersistenceObjectFactory.GetAuthenticationPersistenceRepositoryObject();
        }

        public UsersEntityModel ValidateUser(string username, string password)
        {
            UsersEntityModel objUsersEntityModel = new UsersEntityModel();

            try
            {
                objUsersEntityModel = _IAuthenticationPersistenceRepository.ValidateUser(username, password);
            }
            catch (Exception)
            {

                throw;
            }

            return objUsersEntityModel;
        }
    }
}
