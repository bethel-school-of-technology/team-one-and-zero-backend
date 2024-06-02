using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TEAM_ONE_AND_ZERO_BACKEND.Models;

public class User
{
    [JsonIgnore]
    public int UserId {get; set;}

    public string? Username {get; set;}

    [Required]
    public string? Password{get; set;}

    [Required]
    [EmailAddress]
    public string? Email {get; set;}

    [Required]
    public string? Description {get; set;}

    public string? Birthdate {get; set;}

    [Required]
    public string? ProfilePhoto {get; set;}
}