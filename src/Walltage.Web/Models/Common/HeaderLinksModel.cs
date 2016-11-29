namespace Walltage.Web.Models.Common
{
    public partial class HeaderLinksModel
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        
        public bool AllowPrivateMessages { get; set; }
        public string UnreadPrivateMessages { get; set; }
        public string AlertMessage { get; set; }
    }
}