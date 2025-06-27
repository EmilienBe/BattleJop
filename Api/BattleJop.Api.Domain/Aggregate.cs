using System.ComponentModel.DataAnnotations.Schema;

namespace BattleJop.Api.Domain;
public class Aggregate
{
    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public bool Desactivated { get; set; }
}