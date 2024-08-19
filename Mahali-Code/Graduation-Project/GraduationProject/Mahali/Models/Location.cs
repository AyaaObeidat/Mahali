using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Location
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Address text is required.")]
        public string AddressText { get; private set; } = null!;

        [Required(ErrorMessage = "Address direction is required.")]
        public string AddressDirection { get;private set; }=null!;

        public Guid ShopId { get; private set; }

        private Location() { }
        private Location(string addressText, string addressDirection)
        {
            
            AddressText = addressText;
            AddressDirection = addressDirection;
        }

        public static Location Create(string addressText, string addressDirection)
        {
            if (string.IsNullOrEmpty(addressText)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(addressDirection)) { throw new ArgumentNullException(); }
            return new Location(addressText, addressDirection);
        }

        public void SetAddressText(string addressText)
        {
            AddressText = addressText;
        }
        public void SetAddressDirection(string addressDirection) 
        { 
            AddressDirection = addressDirection;
        }
    }
}
