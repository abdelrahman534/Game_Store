using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Entities;

public class VideoGame
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures auto-increment behavior
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Genre { get; set; }

    public int Stock { get; set; }
    public bool IsAvailable => Stock > 0; // a derived property

    public string? Platform { get; set; }

    public decimal? Price { get; set; }
}
