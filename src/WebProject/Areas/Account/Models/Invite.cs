using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.Account.Models
{
    public class Invite
    {
        [Key]
        public string InviteCode { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? ValidTill { get; set; }
        public bool IsUsed { get; set; }
        public string UseDate { get; set; }

    }
    public enum InviteStatus
    {
        Valid,
        InValid,
        Used,
        Expired,
        Empty
    }
}
