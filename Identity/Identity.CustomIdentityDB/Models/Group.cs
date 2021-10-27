using System;

namespace Identity.CustomIdentityDB.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}