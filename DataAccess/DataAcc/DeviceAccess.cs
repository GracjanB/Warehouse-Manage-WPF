﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.EntityModel;

namespace DataAccess.DataAcc
{
    public class DeviceAccess
    {
        public async Task<bool> AddDevice(Device device)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var deviceExists = context.Devices.FirstOrDefault(x => x.ArticleNumber == device.ArticleNumber);

                    if (deviceExists != null)
                    {
                        deviceExists.Quantity += device.Quantity;
                    }
                    else
                    {
                        context.Devices.Add(device);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Device>> GetDevicesAll()
        {
            List<Device> devices = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    devices = await context.Devices.Include("Producer").ToListAsync<Device>();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return devices;
        }
    }
}
