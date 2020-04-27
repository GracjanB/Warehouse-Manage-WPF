﻿using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.Validators;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectNewDeviceViewModel : Screen
    {
		private ProducerAccess _producerAccess { get; set; }

		private DeviceAccess _deviceAccess { get; set; }

		private int ProjectId { get; set; }

		private IEventAggregator _events { get; set; }


		public ProjectNewDeviceViewModel(IEventAggregator eventAggregator)
		{
			_producerAccess = new ProducerAccess();
			_deviceAccess = new DeviceAccess();
			_events = eventAggregator;
		}


		#region Window Operations

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducers();

		}

		public void SetProjectId(int projectId)
		{
			ProjectId = projectId;
		}

		private async Task LoadProducers()
		{
			var producers = await _producerAccess.GetProducerNamesAll();
			Producers = new BindableCollection<string>(producers);
		}

		#endregion


		#region Form Controls

		private string _deviceName;
		private string _articleNumber;
		private BindableCollection<string> _producers;
		private string _selectedProducer;
		private string _location;
		private int _quantity;


		public string DeviceName
		{
			get { return _deviceName; }
			set 
			{ 
				_deviceName = value;
				NotifyOfPropertyChange(() => DeviceName);
			}
		}

		public string ArticleNumber
		{
			get { return _articleNumber; }
			set 
			{ 
				_articleNumber = value;
				NotifyOfPropertyChange(() => ArticleNumber);
			}
		}

		public BindableCollection<string> Producers
		{
			get { return _producers; }
			set 
			{ 
				_producers = value;
				NotifyOfPropertyChange(() => Producers);
			}
		}

		public string SelectedProducer
		{
			get { return _selectedProducer; }
			set 
			{
				_selectedProducer = value;
				NotifyOfPropertyChange(() => SelectedProducer);
			}
		}

		public string Location
		{
			get { return _location; }
			set 
			{ 
				_location = value;
				NotifyOfPropertyChange(() => Location);
			}
		}

		public int Quantity
		{
			get { return _quantity; }
			set 
			{ 
				_quantity = value;
				NotifyOfPropertyChange(() => Quantity);
			}
		}

		public async void SaveButton()
		{
			var device = new DeviceModel
			{
				Name = DeviceName,
				ArticleNumber = ArticleNumber,
				ProducerName = SelectedProducer,
				Location = Location,
				Quantity = Quantity
			};

			var deviceValidator = new DeviceModelValidator();
			var result = deviceValidator.Validate(device);

			if(result.IsValid)
			{
				var deviceEntity = await device.ConvertToDeviceEntity();
				deviceEntity.ProjectID = ProjectId;
				var resultTask = await _deviceAccess.AddDevice(deviceEntity);

				if(resultTask)
				{
					_events.PublishOnUIThread(new AddedNewDeviceToProjectEvent());
					this.TryClose();
				}
				else
				{
					MessageBox.Show("Urządzenie nie zostało dodane.");
				}
			}
			else
			{
				MessageBox.Show("Błąd walidacji");
			}
		}

		public void ClearButton()
		{
			// Clear form
		}

        #endregion

    }
}