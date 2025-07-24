using BattleJop.Web.Dto;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;


namespace BattleJop.Web.Components.Cards
{
    public partial class TeamCard
    {
        [Parameter] public TeamDto Team { get; set; }
        [Parameter] public EventCallback<TeamDto> OnTeamDeleted { get; set; }


        private Modal _modal;
        private void HandleModalReady(Modal modal)
        {
            _modal = modal;
        }
        private async Task OnShowModalClick() => await _modal?.ShowAsync();

        private void OnTeamSaved(TeamDto team)
        {
            Team.Name = team.Name;
            Team.Players = team.Players;
        }

        private async Task OnTeamDeletedInternal(TeamDto team)
        {
            if (OnTeamDeleted.HasDelegate)
                await OnTeamDeleted.InvokeAsync(team);
        }
    }
}
