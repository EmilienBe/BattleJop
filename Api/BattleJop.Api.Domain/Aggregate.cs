namespace BattleJop.Api.Domain;
public class Aggregate
{
    public Guid Id { get; init; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public bool Desactivated { get; set; }
}