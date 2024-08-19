using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class Order
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public Guid CartId { get;private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderType TypeOfOrder { get; private set; }
        public PaymentType TypeOfPayment { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderProducts> Products { get; private set; }
        public List<ShopOrders> Shops { get; private set; }
        public long? PhoneNumber { get; private set; }
        public decimal? TotalAmount { get; private set; }
        
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits.")]
        public string? CardNumber { get;private set; }
        private Order() { }
        private Order(Guid cartId, OrderType typeOfOrder, PaymentType typeOfPayment)
        {
            CartId = cartId;
            CreatedAt = DateTime.Now;
            TypeOfOrder = typeOfOrder;
            TypeOfPayment = typeOfPayment;
            Status = OrderStatus.NotPaid;
        }

        public static Order Create (Guid cartId, OrderType typeOfOrder, PaymentType typeOfPayment)
        {
            if(cartId==Guid.Empty) { throw new ArgumentException(); }
            return new Order(cartId , typeOfOrder , typeOfPayment);
        }

        public void SetProducts(List<OrderProducts> products)
        {
            Products = products;
        }

        public void SetShops(List<ShopOrders> shops)
        {
            Shops = shops;
        }

        public void SetPhoneNumber (long phoneNumber)
        {
            PhoneNumber = phoneNumber;  
        }
        public void SetCardNumber(string cardNumber)
        {
            CardNumber = cardNumber;
        }
        public void SetOrderStatus()
        {
           Status = OrderStatus.Paid;
        }
        public void SetTotalAmount()
        {
            TotalAmount = 0;
            foreach(var product in Products)
            {
                TotalAmount += product.UnitPrice;
            }
        }

    }
}
