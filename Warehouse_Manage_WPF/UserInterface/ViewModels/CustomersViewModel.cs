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
    public class CustomersViewModel : Screen, IHandle<CustomerCredentialsChangedEvent>
    {
        private CustomerAccess _customers;

        private IWindowManager _windowManager;

        private SimpleContainer _container;

        public CustomersViewModel(IWindowManager windowManager, SimpleContainer simpleContainer)
        {
            _customers = new CustomerAccess();
            NewCustomer = new CustomerModel();
            _windowManager = windowManager;
            _container = simpleContainer;
        }

        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            var customers = await _customers.GetCustomers();
            Customers = new BindableCollection<CustomerModel>();

            foreach (var cust in customers)
                Customers.Add(new CustomerModel(cust));
        }

        #endregion


        #region Customers List

        private BindableCollection<CustomerModel> customers;

        public BindableCollection<CustomerModel> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }

        public void EditCustomer(object dataContext)
        {
            var customer = (CustomerModel)dataContext;

            var CustomerDetailsVM = _container.GetInstance<CustomerDetailsViewModel>();
            CustomerDetailsVM.LoadCustomer(customer);

            _windowManager.ShowDialog(CustomerDetailsVM);
        }

        #endregion

        #region New Customer Form

        private CustomerModel _newCustomer;

        public CustomerModel NewCustomer
        {
            get { return _newCustomer; }
            set 
            { 
                _newCustomer = value;
                NotifyOfPropertyChange(() => NewCustomer);
            }
        }

        public async void SaveButton()
        {
            var customerFormValidator = new CustomerFormValidator();
            var validationResult = customerFormValidator.Validate(NewCustomer);

            if(validationResult.IsValid)
            {
                var customerEntity = NewCustomer.ConvertToCustomerEntity();
                var resultTask = await _customers.AddCustomer(customerEntity);

                if(resultTask)
                {
                    ClearFields();
                    await LoadCustomers();
                }
                else
                {
                    MessageBox.Show("An error occured.");
                }
            }
            else
            {
                MessageBox.Show("Błąd walidacji danych.");
            }
        }

        public void ClearButton()
        {
            ClearFields();
        }

        private void ClearFields()
        {
            NewCustomer.Name = "";
            NewCustomer.Address = "";
            NewCustomer.City = "";
        }

        #endregion


        #region Events

        public async void Handle(CustomerCredentialsChangedEvent customerCredentialsChangedEvent)
        {
            await LoadCustomers();
        }

        #endregion


    }
}