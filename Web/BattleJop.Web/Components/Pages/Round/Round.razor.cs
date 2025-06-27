using TournamentAggregate = BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Web.Components.Pages.Round
{
    public partial class Round
    {
        private TournamentAggregate.Round _round = new();

        protected override async Task OnInitializedAsync()
        {
            _round = await TournamentApi.GetCurrentRound();
        }
    }
}
