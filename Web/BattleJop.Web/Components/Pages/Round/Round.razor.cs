using BattleJop.Web.Dto;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Pages.Round
{
    public partial class Round(NavigationManager navigation)
    {
        private RoundDto _round = new();

        private bool _isLastRound = true;
        private bool _isLoading = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _round = await TournamentApi.GetCurrentRound();
            _isLoading = false;
            StateHasChanged();
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
