using System.ComponentModel.DataAnnotations;

namespace TEAM_ONE_AND_ZERO_BACKEND.Models;

public class Comment 
{
    public int CommentID { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Description { get; set; }

    public DateTime DateTime {get; set;}

}