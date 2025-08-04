using BattleJop.Web.Dto;
using Microsoft.AspNetCore.Components;


namespace BattleJop.Web.Components.Pages
{
    public partial class Ranking(TournamentApiClient apiClient, NavigationManager navigation)
    {
        private List<RankingDto> _rankings;
        private bool _isLoading = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _rankings = await apiClient.GetRankings();
            _isLoading = false;
            StateHasChanged();
        }

        private void OnReturn()
        {
            navigation.NavigateTo("/round");
        }
    }
}
