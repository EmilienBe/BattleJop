using BattleJop.Web.Enum;

namespace BattleJop.Web.Dto
{
    public class MatchDto
    {
        public TeamDto TeamA { get; set; }
        public TeamDto TeamB { get; set; }

        public int ScoreA { get; set; }
        public int ScoreB { get; set; }

        public MatchState State { get; set; }
    }
}
