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
    public class CustomerAccess
    {
        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    customers = await context.Customers.ToListAsync();
                }
            }
            catch { }

            return customers;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingCustomer = context.Customers.FirstOrDefault(x => x.Name == customer.Name);

                    if (existingCustomer == null)
                    {
                        context.Customers.Add(customer);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch { }

            return true;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingCustomer = context.Customers.FirstOrDefault(x => x.Id == customer.Id);

                    if(existingCustomer != null)
                    {
                        existingCustomer.Name = customer.Name;
                        existingCustomer.Address = customer.Address;
                        existingCustomer.City = customer.City;

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch { }

            return true;
        }
    }
}