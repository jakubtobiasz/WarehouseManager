using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.UI.Common;
using WarehouseManager.UI.Components.ProductsComponent.Views;
using WarehouseManager.UI.Components.WarehousesComponent.Views;
using WarehouseManager.UI.Data;
using WarehouseManager.UI.Models;

namespace WarehouseManager.UI.Components.ProductsComponent.ViewModels
{
    public class ProductsViewModel : ObservableObject, IPageViewModel
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<ProductModel> _products;

        private ICommand _addProductCommand;
        private ICommand _editProductCommand;
        private ICommand _removeProductCommand;

        private Window _productFormView;

        public ProductsViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            FillData();
        }

        #region Properties

        public string Name => "Produkty";

        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                if (_products == value) return;

                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private Window ProductFormView
        {
            get => _productFormView;
            set
            {
                if (_productFormView == value) return;

                _productFormView = value;
                OnPropertyChanged(nameof(ProductFormView));
            }
        }

        #endregion

        #region Properties - Commands

        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand is null)
                {
                    _addProductCommand = new RelayCommand(
                        _ => AddProduct(),
                        _ => ProductFormView is null
                    );
                }

                return _addProductCommand;
            }
        }

        public ICommand EditProductCommand
        {
            get
            {
                if (_editProductCommand is null)
                {
                    _editProductCommand = new RelayCommand(
                        p => EditProduct(p as ProductModel)
                    );
                }

                return _editProductCommand;
            }
        }

        public ICommand RemoveProductCommand
        {
            get
            {
                if (_removeProductCommand is null)
                {
                    _removeProductCommand = new RelayCommand(
                        p => RemoveProduct(p as ProductModel)
                    );
                }

                return _removeProductCommand;
            }
        }

        #endregion

        #region Methods

        private void AddProduct()
        {
            ProductFormView = new ProductFormView(new ProductFormViewModel(_dbContext));
            ProductFormView.Show();
            ProductFormView.Closed += OnFormClosed;
        }

        private void EditProduct(ProductModel product)
        {
            ProductFormView = new ProductFormView(new ProductFormViewModel(_dbContext, product));
            ProductFormView.Show();
            ProductFormView.Closed += OnFormClosed;
        }

        private void RemoveProduct(ProductModel product)
        {
            var messageBoxResult = MessageBox.Show($"Czy na pewno chcesz usunąć produkt {product.Name}?",
                "Usuwanie produktu", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No) return;

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            FillData();
        }

        private void OnFormClosed(object sender, EventArgs args)
        {
            ProductFormView = null;
            Products = null;
            FillData();
        }

        private void FillData()
        {
            Products = new ObservableCollection<ProductModel>(_dbContext.Products);
        }

        #endregion
    }
}
