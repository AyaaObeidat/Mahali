using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;

namespace Mahali.Services
{
    public class CheckOutOpertaionService
    {
        private readonly IOrderInterface _orderInterface;
        private readonly ICartInterface _cartInterface;
        private readonly IProductInterface _productInterface;
        private readonly IOrderProductsInterface _orderProductsInterface;
        private readonly IShopOrdersInterface _shopOrdersInterface;

        public CheckOutOpertaionService(IOrderInterface orderInterface , ICartInterface cartInterface , IOrderProductsInterface orderProductsInterface , IProductInterface productInterface , IShopOrdersInterface shopOrdersInterface)
        {
            _orderInterface = orderInterface;
            _cartInterface = cartInterface;
            _productInterface = productInterface;
            _orderProductsInterface = orderProductsInterface;
            _shopOrdersInterface = shopOrdersInterface;
        }

        public async Task PlaceOrderAsync(OrderCreateParameters parameters)
        {
            var cart  = await _cartInterface.GetByIdAsync(parameters.CartId);
            if(cart == null) { return; }

            var order = Order.Create(parameters.CartId, parameters.TypeOfOrder, parameters.TypeOfPayment);
            await _orderInterface.AddAsync(order);

            var cartProducts = cart.Products.ToList();
            foreach (var cartProduct in cartProducts)
            {
                var orderProduct = OrderProducts.Create(order.Id, cartProduct.ProductId, cartProduct.Quantity, cartProduct.UnitPrice, cartProduct.Color, cartProduct.Size);
                await _orderProductsInterface.AddAsync(orderProduct);

                var product = await _productInterface.GetByIdAsync(cartProduct.ProductId);
                var shopOrder = ShopOrders.Create(order.Id, product.ShopId);
                await _shopOrdersInterface.AddAsync(shopOrder);
            }

            order.SetTotalAmount();
            await _orderInterface.UpdateAsync(order);

           
        }

        public async Task CompletePlaceOrderByVisaAsync(OrderGetByParameters parameters)
        {
            var order = await _orderInterface.GetByIdAsync(parameters.OrderId);
            if (order == null) { return; };
            if(order.TypeOfPayment == AllEnums.AllEnums.PaymentType.Visa)
            {
                order.SetPhoneNumber(parameters.PhoneNumber);
                order.SetCardNumber(parameters.CardNumber);
                order.SetOrderStatus();
                await _orderInterface.UpdateAsync(order);
            }
          return;
        }

        public async Task UpdateOrderStatusAsync(OrderGetById order)
        {
            var selectedOrder = await _orderInterface.GetByIdAsync(order.OrderId);
            if (selectedOrder == null) return;
            selectedOrder.SetOrderStatus();
            await _orderInterface.UpdateAsync(selectedOrder);
            
        }

        public async Task<List<OrderDetails>?> GetAllAsync()
        {
            var orders = await _orderInterface.GetAllAsync();
            return orders.Select(x => new OrderDetails
            {
                Id = x.Id,
                TotalAmount = x.TotalAmount,
                CreatedAt = x.CreatedAt,
                CartId = x.CartId,
                TypeOfOrder = x.TypeOfOrder,
                TypeOfPayment = x.TypeOfPayment,
                Status = x.Status,
            }).ToList();
        }
        public async Task<OrderDetails> GetByIdAsync(OrderGetById order)
        {
            var selectedOrder = await _orderInterface.GetByIdAsync(order.OrderId);
            return new OrderDetails
            {
                Id = selectedOrder.Id,
                TotalAmount = selectedOrder.TotalAmount,
                CreatedAt = selectedOrder.CreatedAt,
                CartId = selectedOrder.CartId,
                TypeOfOrder = selectedOrder.TypeOfOrder,
                TypeOfPayment = selectedOrder.TypeOfPayment,
                Status = selectedOrder.Status
            };
        }
    }
}
