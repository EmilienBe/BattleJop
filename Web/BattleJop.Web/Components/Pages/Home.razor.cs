using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BattleJop.Web.Components.Pages
{
    public partial class Home(NavigationManager navigation, TournamentApiClient client, TournamentState tournamentState, ProtectedLocalStorage localStorage)
    {

        private bool _isLoading = false;
        private async Task GoToRoundAsync()
        {
            _isLoading = true;
            var id = await client.GetCurrentTournamentId();
            if (id == Guid.Empty)
            {
                GoToCreation();
                return;
            }
            tournamentState.CurrentTournamentId = id;
            await localStorage.SetAsync("tournamentId", id);
            navigation.NavigateTo("/round");
        }
        private void GoToCreation() => navigation.NavigateTo("/settings/tournament");
    }
}
