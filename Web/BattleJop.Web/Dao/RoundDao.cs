using BattleJop.Web.Enum;

namespace BattleJop.Web.Dao
{
    public class RoundDao
    {
        public Guid Id { get; set; }

        public int RunningOrder { get; set; }

        public RoundState State { get; set; }

        public List<MatchDao> Matchs { get; set; } = [];
    }
}
