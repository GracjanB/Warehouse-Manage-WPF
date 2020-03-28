﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Warehouse_Manage_WPF.Entities;

namespace Warehouse_Manage_WPF.EntitiesConfiguration
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(255);

            HasMany(c => c.Devices)
            .WithMany(c => c.Projects)
            .Map(m =>
            {
                m.ToTable("ProjectDevices");
                m.MapLeftKey("ProjectId");
                m.MapRightKey("DeviceId");
            });
        }
    }
}