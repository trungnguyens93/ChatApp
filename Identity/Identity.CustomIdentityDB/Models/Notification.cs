using System;

namespace Identity.CustomIdentityDB.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool IsSent { get; set; }
        public string GroupId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}