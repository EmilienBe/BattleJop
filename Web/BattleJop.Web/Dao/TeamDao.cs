namespace BattleJop.Web.Dao
{
    public class TeamDao
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }

        public int Score { get; set; }

        public bool IsWinner { get; set; }

        public int RemainingPuck { get; set; }
    }
}
