namespace BattleJop.Web.Dto
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Tuple<string, string> Players { get; set; }

    }
}
