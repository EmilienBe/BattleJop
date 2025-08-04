using BattleJop.Web.Enum;

namespace BattleJop.Web.Dao
{
    public class MatchDao
    {
        public Guid Id { get; set; }
        public int RunningOrder { get; set; }
        public RoundState State { get; set; }

        public TeamDao FirstTeam { get; set; }
        public TeamDao SecondTeam { get; set; }
    }
}
