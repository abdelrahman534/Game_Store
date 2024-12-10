using System;
namespace GameStore.Entities;

public class VideoGameUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; } // Optional
    public string? Genre { get; set; } // Optional
    public int? Stock { get; set; } // Optional
    public string? Platform { get; set; } // Optional
    public decimal? Price { get; set; } // Optional
}

