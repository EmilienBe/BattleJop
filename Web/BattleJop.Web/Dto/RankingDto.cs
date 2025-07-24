namespace BattleJop.Web.Dto
{
    public class RankingDto
    {
        public TeamDto Team { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Pucks { get; set; }

        public int Points { get; set; }
    }
}
