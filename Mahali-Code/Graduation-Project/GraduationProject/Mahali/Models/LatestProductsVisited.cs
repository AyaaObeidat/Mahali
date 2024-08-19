namespace Mahali.Models
{
    public class LatestProductsVisited
    {
        
        public Guid Id { get; set; }
        public DateTime VisitedDateTime { get;private set; }
        public Guid CustomerId { get;private set; }
        public Guid ProductId { get;private set; }

        private LatestProductsVisited() { }
        private LatestProductsVisited (Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
            VisitedDateTime = DateTime.Now;
        }
        public static LatestProductsVisited Create(Guid customerId, Guid productId)
        {
            if (customerId == Guid.Empty) { throw new ArgumentNullException(); }
            if (productId == Guid.Empty) { throw new ArgumentNullException(); }
            return new LatestProductsVisited(customerId, productId);
        }

        public void SetVisitedDateTime(DateTime visitedDateTime)
        {
            // Assuming visitedDateTime should not be in the future
            if (visitedDateTime > DateTime.Now)
            {
                throw new ArgumentException("VisitedDateTime cannot be in the future.");
            }

            VisitedDateTime = visitedDateTime;
        }
    }
}
