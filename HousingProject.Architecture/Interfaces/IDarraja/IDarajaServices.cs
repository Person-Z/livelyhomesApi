﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingProject.Infrastructure.Interfaces.IDarraja
{
   public  interface IDarajaServices
    {
       Task<string> FetchAccessToken();
    }
}
