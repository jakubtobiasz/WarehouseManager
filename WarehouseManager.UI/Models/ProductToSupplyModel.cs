using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.UI.Common;

namespace WarehouseManager.UI.Models
{
    public class ProductToSupplyModel : ObservableObject
    {
        private int _productToSupplyId;
        private int _quantity;

        private int _supplyId;
        private SupplyModel _supply;
        private int _productId;
        private ProductModel _product;

        [Key]
        public int ProductToSupplyId
        {
            get => _productToSupplyId;
            set
            {
                if (_productToSupplyId == value) return;

                _productToSupplyId = value;
                OnPropertyChanged(nameof(ProductToSupplyId));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value) return;

                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        [ForeignKey(nameof(Supply))]
        public int SupplyId
        {
            get => _supplyId;
            set
            {
                if (_supplyId == value) return;

                _supplyId = value;
                OnPropertyChanged(nameof(SupplyId));
            }
        }

        public virtual SupplyModel Supply
        {
            get => _supply;
            set
            {
                if (_supply == value) return;

                _supply = value;
                OnPropertyChanged(nameof(Supply));
            }
        }

        [ForeignKey(nameof(Product))]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (_productId == value) return;

                _productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        public virtual ProductModel Product
        {
            get => _product;
            set
            {
                if (_product == value) return;

                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }
    }
}
