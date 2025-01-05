namespace Renting.Entities;

public class Costume
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTime? RentingTime { get; set; }
}
