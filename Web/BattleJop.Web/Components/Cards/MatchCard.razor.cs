using BattleJop.Web.Dto;
using BattleJop.Web.Enum;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Cards
{
    public partial class MatchCard(TournamentApiClient client)
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

        private async Task HandleScoreConfirmedAsync((int scoreA, int scoreB, int remainingPuckA, int remainingPuckB) result)
        {
            Match.ScoreA = result.scoreA;
            Match.ScoreB = result.scoreB;
            Match.RemainingPuckA = result.remainingPuckA;
            Match.RemainingPuckB = result.remainingPuckB;

            await client.EndMatch(Match);

            UpdateMatchState(MatchState.Ended);
        }

    }
}
