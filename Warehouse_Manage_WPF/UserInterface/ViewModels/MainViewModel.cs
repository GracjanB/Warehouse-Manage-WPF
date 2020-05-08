﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Warehouse_Manage_WPF.UserInterface.Models;
using System.Windows;
using System.Windows.Input;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        private SimpleContainer _simpleContainer { get; set; }

        private IWindowManager _windowManager { get; set; }


        public MainViewModel(SimpleContainer simpleContainer, IWindowManager windowManager)
        {
            _simpleContainer = simpleContainer;
            _windowManager = windowManager;
            ActivateItem(_simpleContainer.GetInstance<WarehouseViewModel>());
        }


        #region Left Menu

        public void NewDeviceViewOpen()
        {
            ChangeActiveItem(_simpleContainer.GetInstance<NewDeviceViewModel>(), true);
        }

        public void WarehouseViewOpen()
        {
            ChangeActiveItem(_simpleContainer.GetInstance<WarehouseViewModel>(), true);
        }

        public void ProjectListViewOpen()
        {
            ChangeActiveItem(_simpleContainer.GetInstance<ProjectListViewModel>(), true);
        }

        #endregion


        #region PopUp Menu

        public void OpenSettings()
        {
            var SettingsVM = _simpleContainer.GetInstance<SettingsViewModel>();
            _windowManager.ShowDialog(SettingsVM);
        }

        public void CloseAppButton()
        {
            if (ActiveItem != null)
                DeactivateItem(ActiveItem, true);

            TryClose();
        }

        #endregion

    }
}
