using System.ComponentModel.DataAnnotations;

namespace APBD5.Animal;

public class AnimalDTO
{
    [Required]
    [Length(0, 200)]
    public string Name { get; set; }
    [Length(0, 200)]
    public string? Description { get; set; }
    [Required]
    [Length(0, 200)]
    public string Category { get; set; }
    [Required]
    [Length(0, 200)]
    public string Area { get; set; }
}