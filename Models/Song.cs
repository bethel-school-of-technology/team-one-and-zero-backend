using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEAM_ONE_AND_ZERO_BACKEND.Models;

public class Song 
{
    public int SongId { get; set; }

    [Required]
    public string? SongName { get; set; }

    [Required]
    public string? SongArtist { get; set; }
    
    public string? SpotifyId { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}