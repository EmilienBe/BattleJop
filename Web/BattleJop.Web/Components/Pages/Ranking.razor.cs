using BattleJop.Web.Dto;
using Microsoft.AspNetCore.Components;


namespace BattleJop.Web.Components.Pages
{
    public partial class Ranking(TournamentApiClient apiClient, NavigationManager navigation)
    {
        private List<RankingDto> _rankings;

        protected override async void OnInitialized()
        {
            _rankings = await apiClient.GetRankings();
        }

        private void OnReturn()
        {
            navigation.NavigateTo("/round");
        }
    }
}
