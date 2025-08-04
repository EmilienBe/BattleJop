namespace BattleJop.Web.Dao
{
    public class RankDao
    {
        public int Rank { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public int NumberOfVictory { get; set; }
        public int NumberOfDefeat { get; set; }
        public int TotalScore { get; set; }
        public int TotalRemainingPuck { get; set; }
    }
}
