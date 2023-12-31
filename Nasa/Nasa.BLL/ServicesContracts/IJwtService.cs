﻿using Nasa.Common.DTO.User;
using Nasa.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.ServicesContracts
{
    public interface IJwtService
    {
        Task<AuthorizationResponse> GetJwt(User user);
    }
}
