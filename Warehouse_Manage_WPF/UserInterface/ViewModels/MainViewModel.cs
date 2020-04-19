﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.DataAccess;
using System.Windows;
using System.Windows.Input;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        private SimpleContainer _simpleContainer { get; set; }


        public MainViewModel(SimpleContainer simpleContainer)
        {
            _simpleContainer = simpleContainer;
            ActivateItem(_simpleContainer.GetInstance<WarehouseViewModel>());
        }

        public void NewDeviceViewOpen()
        {
            ActivateItem(_simpleContainer.GetInstance<NewDeviceViewModel>());
        }

        public void WarehouseViewOpen()
        {
            ActivateItem(_simpleContainer.GetInstance<WarehouseViewModel>());
        }

        public void ProjectListViewOpen()
        {
            ActivateItem(_simpleContainer.GetInstance<ProjectListViewModel>());
        }

        public void ProjectViewOpen()
        {
            ActivateItem(_simpleContainer.GetInstance<ProjectViewModel>());
        }

        public void CloseAppButton()
        {
            if (ActiveItem != null)
                DeactivateItem(ActiveItem, true);

            TryClose();
        }
    }
}
