using BattleJop.Web.Enum;

namespace BattleJop.Web.Dto
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public TeamDto TeamA { get; set; }
        public TeamDto TeamB { get; set; }

        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public int RemainingPuckA { get; set; }
        public int RemainingPuckB { get; set; }

        public MatchState State { get; set; }
    }
}
