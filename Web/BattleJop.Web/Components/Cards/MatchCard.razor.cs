using BattleJop.Web.Dto;
using BattleJop.Web.Enum;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Cards
{
    public partial class MatchCard
    {
        [Parameter] public required MatchDto Match { get; set; }

        private Modal _modal;

        private void HandleModalReady(Modal modal)
        {
            _modal = modal;
        }

        private async Task OnShowModalClick() => await _modal?.ShowAsync();

        private void UpdateMatchState(MatchState state)
        {
            Match.State = state;
        }

        private void HandleScoreConfirmed((int a, int b) result)
        {
            Match.ScoreA = result.a;
            Match.ScoreB = result.b;
            UpdateMatchState(MatchState.Ended);
        }

    }
}
