﻿using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DataAccess.DataAcc;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class WarehouseViewModel : Screen, IHandle<DeviceCredentialsChangedEvent>
    {
        private SimpleContainer _container { get; set; }

        private IWindowManager _windowManager { get; set; }

        private IDeviceAccess _deviceAccess { get; set; }

        private IProducerAccess _producerAccess;

        // Warehouse project Id
        private const int ProjectId = 5;


        public WarehouseViewModel(SimpleContainer simpleContainer, IWindowManager windowManager, IDeviceAccess deviceAccess, IProducerAccess producerAccess)
        {
            _container = simpleContainer;
            _windowManager = windowManager;
            _deviceAccess = deviceAccess;
            _producerAccess = producerAccess;
        }


        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadDevices();
        }

        private async Task LoadDevices()
        {
            var devices = await _deviceAccess.GetDevicesAll(ProjectId);
            Devices = new BindableCollection<DeviceModel>();

            foreach (var device in devices)
            {
                Devices.Add(new DeviceModel(device, _producerAccess));
            }
        }

        #endregion


        #region Device Grid

        private BindableCollection<DeviceModel> _devices;

        public DeviceModel SelectedDevice { get; set; }


        public BindableCollection<DeviceModel> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                NotifyOfPropertyChange(() => Devices);
            }
        }

        public void SelectDeviceClick()
        {
            if (SelectedDevice != null)
            {
                DeviceDetailsViewModel deviceDetailsVM = _container.GetInstance<DeviceDetailsViewModel>();
                deviceDetailsVM.LoadDevice(SelectedDevice);
                _windowManager.ShowDialog(deviceDetailsVM);

            }
            else
            {
                MessageBox.Show("Błąd ładowania urządzenia. Spróbuj ponownie");
            }
                
        }

        #endregion


        #region Events

        public async void Handle(DeviceCredentialsChangedEvent deviceCredentialsChangedEvent)
        {
            await LoadDevices();
        }

        #endregion

    }
}
