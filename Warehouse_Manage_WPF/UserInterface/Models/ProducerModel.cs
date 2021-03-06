﻿using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class ProducerModel : IProducerEntityConversion
    {
        public string Name { get; set; }

        public string URL { get; set; }

        public Producer ConvertToProducerEntity()
        {
            Producer producerEntity = new Producer
            {
                Name = this.Name,
                URL = this.URL
            };

            return producerEntity;
        }
    }
}
