﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ArticleNumber { get; set; }

        public string Location { get; set; }

        public int Quantity { get; set; }

        public int ProducerID { get; set; }

        public string ProducerName { get; set; }

        public ICollection<Project> Projects { get; set; }

        public Device(Entities.Device device)
        {
            Id = device.Id;
            Name = device.Name;
            ArticleNumber = device.ArticleNumber;
            Location = device.Location;
            Quantity = device.Quantity;
            ProducerID = device.ProducerID;
            ProducerName = device.Producer.Name;
            

        }

    }
}