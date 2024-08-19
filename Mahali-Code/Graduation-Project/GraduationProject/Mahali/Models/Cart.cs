using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Cart
    {
        
        public Guid Id { get; set; }
        public decimal TotalAmount { get; private set; }

        [Required]
        public Guid CustomerId { get;private set; }
        public List<CartProducts> Products { get; private set; }

        private Cart() { }

        private Cart(Guid customerId)
        {
            CustomerId = customerId;
        }

        public static Cart Create(Guid customerId)
        {
            if(customerId == Guid.Empty) { throw new ArgumentNullException(); }
            return new Cart(customerId);
        }

        public void SetProducts(List<CartProducts> products)
        {
            Products = products;
        }
        public void SetTotalAmount()
        {
            TotalAmount = 0;
            if(Products == null) { throw new ArgumentNullException(); }
            foreach(CartProducts p in Products)
            {
                TotalAmount += p.UnitPrice;
                     
            }
        }
    }
}
