using System.ComponentModel.DataAnnotations;

namespace DiscussionPublisherAPI.Models
{
    public class PackageInfo
    {
        public PackageInfo(string header, string description)
        {
            Header = header;
            Description = description;
        }

        [Required]
        public string Header { get; set; }
        [Required]
        public string Description { get; set; }

        public string GetMessage()
        {
            return $"Header = {Header}, Description = {Description}";
        }
    }
}
