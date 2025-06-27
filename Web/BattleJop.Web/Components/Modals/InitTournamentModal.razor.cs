using BlazorBootstrap;
using Microsoft.AspNetCore.Components;


namespace BattleJop.Web.Components.Modals
{
    public partial class InitTournamentModal(NavigationManager navigation)
    {
        [Parameter] public EventCallback<Modal> OnModalReady { get; set; }

        private Modal InternalModal;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && OnModalReady.HasDelegate)
            {
                await OnModalReady.InvokeAsync(InternalModal);
            }
        }

        private async Task OnConfirmedModalClick()
        {
            await InternalModal.HideAsync();
            navigation.NavigateTo("/round");
        }
    }
}
