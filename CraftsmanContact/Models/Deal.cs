using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class Deal
{
    [Key] public int DealId { get; set; }
    public string CraftsmanId { get; set; } = String.Empty;
    public string ClientId { get; set; } = String.Empty;
    public int OfferedServiceId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [DefaultValue(false)] public bool IsAcceptedByCraftsman { get; set; }
    [DefaultValue(false)] public bool IsClosedByCraftsman { get; set; }
    [DefaultValue(false)] public bool IsClosedByClient { get; set; }

    //Plans for the future:
    //public int RateByCraftsman { get; set; }
    //public int RateByClient { get; set; } 
}