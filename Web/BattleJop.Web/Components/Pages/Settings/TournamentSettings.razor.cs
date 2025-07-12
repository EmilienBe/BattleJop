using BattleJop.Web.Dto;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Pages.Settings
{
    public partial class TournamentSettings(NavigationManager navigation)
    {
        int RoundNumber { get; set; }
        private List<TeamDto> Teams { get; set; } = [];

        private Modal _modal = new();
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

        private void CreateTournament()
        {
            navigation.NavigateTo("/round");
        }
    }
}
