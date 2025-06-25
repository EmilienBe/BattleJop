using BattleJop.Api.Domain.TournamentAggregate;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;

namespace BattleJop.Web.Components.Cards
{
    public partial class MatchCard
    {
        [Parameter] public required Match Match { get; set; }
        [Parameter] public required int ScoreA { get; set; }
        [Parameter] public required int ScoreB { get; set; }

        private MatchState _matchState = MatchState.WaintingForStart;

        public enum MatchState
        {
            WaintingForStart = 0,
            InProgress = 1,
            Ended = 2
        }

        private Modal _modal;

        private void HandleModalReady(Modal modal)
        {
            _modal = modal;
        }

        private async Task OnShowModalClick() => await _modal?.ShowAsync();

        private void UpdateMatchState(MatchState state)
        {
            _matchState = state;
        }

        private void HandleScoreConfirmed((int a, int b) result)
        {
            ScoreA = result.a;
            ScoreB = result.b;
            UpdateMatchState(MatchState.Ended);
        }

    }
}
