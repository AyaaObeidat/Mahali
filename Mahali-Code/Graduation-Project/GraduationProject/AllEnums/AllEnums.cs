namespace AllEnums
{
    public class AllEnums
    {
        //all enums
        public enum RequestStatus
        {
            Pending,
            Approved,
            Rejected
        }

        public enum Sizes
        {
            ExtraSmall,
            Small,
            Medium,
            Large,
            ExtraLarge,
            DoubleExtraLarge,
            TripleExtraLarge,
            OneSize,
            Default,
            US_36,
            US_37,
            US_38,
            US_39,
            US_40,
            US_41,
            US_42,
            US_43,
            US_44,
            US_45,
            US_46,
        }

        public enum Colors
        {
            Red,
            Green,
            Blue,
            Yellow,
            Orange,
            Purple,
            Pink,
            Brown,
            Black,
            White,
            Gray,
            Cyan,
            Magenta,
            Turquoise,
            Lavender,
            Maroon,
            Navy,
            Olive,
            Teal,
            Coral,
        }
        public enum OrderStatus
        {
            Paid,
            NotPaid
        }
        public enum PaymentType
        {
            Cash,
            Visa
        }

        public enum OrderType
        {
            InStorePickup,
            Delivery
        }

    }
}
