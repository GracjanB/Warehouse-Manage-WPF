﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;

namespace Warehouse_Manage_WPF.UserInterface.Helpers
{
    interface IDeviceEntityConversion
    {
        Task<Device> ConvertToDeviceEntity();

        Task<int> GetProducerId(string Name);
    }
}