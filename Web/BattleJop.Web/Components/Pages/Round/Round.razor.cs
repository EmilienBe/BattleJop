using BattleJop.Web.Dto;

namespace BattleJop.Web.Components.Pages.Round
{
    public partial class Round
    {
        private RoundDto _round = new();

        protected override async Task OnInitializedAsync()
        {
            _round = await TournamentApi.GetCurrentRound();
        }
    }
}
