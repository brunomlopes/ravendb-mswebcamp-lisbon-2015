namespace RavenDB.MsWebCamp2015.Models
{
    public class SiteConfig
    {
        public static string SiteConfigId = "SiteConfig";
        public string Id { get; set; }
        public EmailSiteConfig Email { get; set; }
        public string SiteName { get; set; }
    }

    public class EmailSiteConfig
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
    }
}