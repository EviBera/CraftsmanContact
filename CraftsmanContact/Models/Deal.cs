using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class Deal
{
    [Key] public int Id { get; set; }
    [Required] public string CraftsmanId { get; set; }
    [Required] public string ClientId { get; set; }
    public int OfferedServiceId { get; set; }
    public DateTime CreatedAt { get; set; }
    [DefaultValue(false)] public bool IsAcceptedByCraftsman { get; set; }
    [DefaultValue(false)] public bool IsClosedByCraftsman { get; set; }
    [DefaultValue(false)] public bool IsClosedByClient { get; set; }

    //Plans for the future:
    //public int RateByCraftsman { get; set; }
    //public int RateByClient { get; set; } 

}