using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEAM_ONE_AND_ZERO_BACKEND.Models;

public class Comment 
{
    public int CommentID { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Description { get; set; }

    public DateTime CreatedAt {get; set;}

    [ForeignKey("User")]
    public int UserId { get; set; }

    public User? User { get; set; }

    [ForeignKey("Song")]
    public int SongId { get; set; }

    public Song? Song { get; set; }
}