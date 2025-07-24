using BattleJop.Web.Dto;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Pages.Round
{
    public partial class Round(NavigationManager navigation)
    {
        private RoundDto _round = new();

        private bool _isLastRound = true;

        protected override async Task OnInitializedAsync()
        {
            _round = await TournamentApi.GetCurrentRound();
        }

        private void EndRound()
        {
            if (!_isLastRound)
                navigation.NavigateTo("/round");
            else
                navigation.NavigateTo("/ranking");
        }

        private void CheckRanking()
        {
            navigation.NavigateTo("/ranking");
        }
    }
}
