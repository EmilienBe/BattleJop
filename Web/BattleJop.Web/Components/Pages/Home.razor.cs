using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Pages
{
    public partial class Home(NavigationManager navigation)
    {
        private Modal _modal;
        private async Task CreateTournamentAsync() => await _modal?.ShowAsync();

        private void GoToRound() => navigation.NavigateTo("/round");
        private void HandleModalReady(Modal modal)
        {
            _modal = modal;
        }
    }
}
