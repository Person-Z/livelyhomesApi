﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingProject.Core.Models.Houses.HouseUnitRegistration
{
 public   class HouseUnitsStatus
    {
        [Key]
        public int HouseidstatusID { get; set; }
        public string HouseName { get; set; }
        public int HouseID { get; set; }
        public  int DoorNumber { get; set; }
        public bool Occupied { get; set; }
        public string Tenant_Email { get; set; }
        public DateTime DateOccupied { get; set; } = DateTime.Now;
    }
}
