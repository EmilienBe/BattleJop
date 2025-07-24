using BattleJop.Web.Dto;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;


namespace BattleJop.Web.Components.Modals
{
    public partial class EditTeamModal
    {
        [Parameter] public TeamDto Team { get; set; } = new TeamDto();
        [Parameter] public EventCallback<Modal> OnModalReady { get; set; }
        [Parameter] public EventCallback<TeamDto> OnTeamSaved { get; set; }
        [Parameter] public EventCallback<TeamDto> OnTeamDeleted { get; set; }

        private string TeamName { get; set; } = "";

        private string Player1Name { get; set; } = "";
        private string Player2Name { get; set; } = "";

        private Modal InternalModal = new();

        protected override void OnParametersSet()
        {
            TeamName = Team?.Name ?? string.Empty;
            Player1Name = Team?.Players?.Item1 ?? string.Empty;
            Player2Name = Team?.Players?.Item2 ?? string.Empty;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && OnModalReady.HasDelegate)
            {
                await OnModalReady.InvokeAsync(InternalModal);
            }
        }

        private async Task OnValidateAsync()
        {
            var team = new TeamDto
            {
                Name = TeamName,
                Players = new Tuple<string, string>(Player1Name, Player2Name)
            };

            if (OnTeamSaved.HasDelegate)
                await OnTeamSaved.InvokeAsync(team);

            await InternalModal.HideAsync();
        }

        private async Task OnDeleteAsync()
        {
            if (OnTeamDeleted.HasDelegate)
            {
                await OnTeamDeleted.InvokeAsync(Team);
                await InternalModal.HideAsync();
            }
        }
    }
}
