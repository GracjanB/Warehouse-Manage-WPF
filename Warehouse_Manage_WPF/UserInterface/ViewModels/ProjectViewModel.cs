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

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectViewModel : Screen, IHandle<AddedNewDeviceToProjectEvent>, IHandle<DeviceCredentialsChangedEvent>, IHandle<ChangedProjectCredentialsEvent>
    {
		private SimpleContainer _container { get; set; }

		private ProjectAccess _projectAccess { get; set; }

		private DeviceAccess _deviceAccess { get; set; }

		private IWindowManager _windowManager { get; set; }


		public ProjectViewModel(SimpleContainer simpleContainer, IEventAggregator eventAggregator, IWindowManager windowManager)
		{
			_container = simpleContainer;
			_projectAccess = _container.GetInstance<ProjectAccess>();
			_deviceAccess = _container.GetInstance<DeviceAccess>();

			_windowManager = windowManager;
			eventAggregator.Subscribe(this);
		}


		#region Window Operations


		public void LoadProject(ProjectModel project)
		{
			projectModel = project;
			LoadProjectInf();
		}

		public async Task LoadProject2(int projectId)
		{
			var project = await _projectAccess.GetProjectById(projectId);

			if(project != null)
			{
				projectModel = new ProjectModel(project);

				CustomerName = projectModel.CustomerName;
				ProjectStatus = projectModel.Status;
				Comment = projectModel.Comment;

				await LoadDevices();
			}
			else
			{
				MessageBox.Show("This project doesn't exists.");
			}
		}

		private async Task LoadProjectInfo(int Id)
		{
			var project = await _projectAccess.GetProjectById(Id);

			if(project != null)
			{
				projectModel = new ProjectModel(project);
				LoadProjectInf();
			}
			else
			{
				MessageBox.Show("This project doesn't exists.");
			}
		}

		private void LoadProjectInf()
		{
			CustomerName = projectModel.CustomerName;
			ProjectStatus = projectModel.Status;
			Comment = projectModel.Comment;
		}

		private async Task LoadDevices()
		{
			var devices = await _deviceAccess.GetDevicesAll(projectModel.Id);
			ProjectDevices = new BindableCollection<DeviceModel>();

			foreach (var device in devices)
				ProjectDevices.Add(new DeviceModel(device));
		}

		#endregion


		#region Project

		private ProjectModel _project { get; set; }

		public ProjectModel projectModel
		{
			get { return _project; }
			set
			{
				_project = value;
				NotifyOfPropertyChange(() => projectModel);
			}
		}

		#endregion


		#region PopUp Menu

		public void AddNewDevice()
		{
			var ProjectNewDeviceVM = _container.GetInstance<ProjectNewDeviceViewModel>();
			ProjectNewDeviceVM.SetProjectId(projectModel.Id);
			_windowManager.ShowDialog(ProjectNewDeviceVM);
		}

		public async void ExchangeDevices()
		{
			var ProjectAddDeviesFromWarehouseVM = _container.GetInstance<ProjectAddDevicesFromWarehouseViewModel>();
			await ProjectAddDeviesFromWarehouseVM.LoadProjectDevices(this.projectModel.Id);
			_windowManager.ShowDialog(ProjectAddDeviesFromWarehouseVM);
		}

		public void EditProject()
		{
			// Show edit window
			var projectDetailsVM = _container.GetInstance<ProjectDetailsViewModel>();
			projectDetailsVM.LoadProject(projectModel);
			_windowManager.ShowDialog(projectDetailsVM);
		}

		public void CloseProject()
		{
			var mainVM = (MainViewModel) this.Parent;
			var projectListVM = _container.GetInstance<ProjectListViewModel>();
			mainVM.ActivateItem(projectListVM);
		}

		#endregion


		#region Project Information Card

		private string _customerName;
		private string _projectStatus;
		private string _comment;

		public string CustomerName
		{
			get { return _customerName; }
			set 
			{ 
				_customerName = value;
				NotifyOfPropertyChange(() => CustomerName);
			}
		}

		public string ProjectStatus
		{
			get { return _projectStatus; }
			set 
			{ 
				_projectStatus = value;
				NotifyOfPropertyChange(() => ProjectStatus);
			}
		}

		public string Comment
		{
			get { return _comment; }
			set 
			{ 
				_comment = value;
				NotifyOfPropertyChange(() => Comment);
			}
		}

		#endregion


		#region Devices List Card

		private BindableCollection<DeviceModel> _projectDevices;
		private DeviceModel _selectedDevice;


		public BindableCollection<DeviceModel> ProjectDevices
		{
			get { return _projectDevices; }
			set
			{
				_projectDevices = value;
				NotifyOfPropertyChange(() => ProjectDevices);
			}
		}

		public DeviceModel SelectedDevice
		{
			get { return _selectedDevice; }
			set 
			{
				_selectedDevice = value;
				NotifyOfPropertyChange(() => SelectedDevice);
			}
		}

		public void MouseDoubleClick_DataGrid()
		{
			if(SelectedDevice != null)
			{
				var deviceDetailsVM = _container.GetInstance<DeviceDetailsViewModel>();
				deviceDetailsVM.LoadDevice(SelectedDevice);
				_windowManager.ShowDialog(deviceDetailsVM);
			}
		}

		#endregion


		#region Events

		public async void Handle(AddedNewDeviceToProjectEvent newDeviceAdded)
		{
			await LoadDevices();
		}

		public async void Handle(DeviceCredentialsChangedEvent deviceCredentialsChanged)
		{
			await LoadDevices();
		}

		public async void Handle(ChangedProjectCredentialsEvent changedProjectCredentialsEvent)
		{
			await LoadProject2(changedProjectCredentialsEvent.ProjectId);
		}

		#endregion

	}
}

