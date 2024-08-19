namespace Mahali.Models
{
    public class AboutUs
    {
        public Guid Id { get; set; }
        public string ContentBody { get; private set; } = null!;
        public Guid AdminId { get;private set; }

        public AboutUs() { }
        public AboutUs (Guid adminId, string contentBody) 
        { 
            AdminId = adminId;
            ContentBody = contentBody;
        }
        public static AboutUs Create (Guid adminId, string contentBody)
        {
            if(adminId == Guid.Empty) { throw new ArgumentNullException(nameof(adminId)); }
            if(string.IsNullOrEmpty(contentBody)) {  throw new ArgumentNullException();}

            return new AboutUs (adminId, contentBody);
        }

        public void SetContentBody (string contentBody)
        {
            ContentBody = contentBody;  
        }
    }

   
}
