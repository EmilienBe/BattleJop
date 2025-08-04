using BattleJop.Web.Dto;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BattleJop.Web.Components.Pages.Settings
{
    public partial class TournamentSettings(NavigationManager navigation, TournamentApiClient client, TournamentState tournamentState, ProtectedLocalStorage localStorage)
    {
        int RoundNumber { get; set; }
        string TournamentName { get; set; } = "JOP 2025";
        private List<TeamDto> Teams { get; set; } = [];

        private Modal _modal = new();
        private bool _isLoading = false;
        protected override void OnInitialized()
        {

            if (Teams.Count == 0)
            {
                Teams.Add(new() { Name = "Les foufous", Players = new Tuple<string, string>("Maud", "Emilien") });
            }
        }
        private void HandleModalReady(Modal modal)
        {
            _modal = modal;
        }

        private async Task OnShowModalClick() => await _modal?.ShowAsync();

        private void OnAddTeam(TeamDto team)
        {
            Teams.Add(team);
        }

        private void OnDeleteTeam(TeamDto team)
        {
            Teams.Remove(team);
        }

        private async Task CreateTournamentAsync()
        {
            _isLoading = true;
            Guid tournamentId = await client.CreateTournament(TournamentName, RoundNumber, Teams);

            tournamentState.CurrentTournamentId = tournamentId;
            await localStorage.SetAsync("tournamentId", tournamentId);

            navigation.NavigateTo("/round");
        }
    }
}
