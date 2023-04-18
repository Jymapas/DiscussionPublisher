namespace DiscussionPublisherAPI.Models
{
    public class PackageInfo
    {
        public PackageInfo(string header, string description)
        {
            Header = header;
            Description = description;
        }

        public string Header { get; set; }
        public string Description { get; set; }

        public string GetMessage()
        {
            return $"Header = {Header}, Description = {Description}";
        }
    }
}
