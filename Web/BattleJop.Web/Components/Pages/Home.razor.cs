using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Pages
{
    public partial class Home(NavigationManager navigation)
    {
        private void GoToRound() => navigation.NavigateTo("/round");
        private void GoToCreation() => navigation.NavigateTo("/settings/tournament");
    }
}
