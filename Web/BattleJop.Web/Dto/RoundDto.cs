namespace BattleJop.Web.Dto
{
    public class RoundDto
    {
        public List<MatchDto> Matches { get; set; } = [];
        public int RunningOrder { get; set; }
    }
}
