using BattleJop.Web.Dto;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;


namespace BattleJop.Web.Components.Modals
{
    public partial class EditScoreModal
    {

        [Parameter] public required MatchDto Match { get; set; }
        [Parameter] public EventCallback<Modal> OnModalReady { get; set; }
        [Parameter] public EventCallback<(int, int, int, int)> OnConfirmed { get; set; }

        private int RemainingPuckA { get; set; }
        private int RemainingPuckB { get; set; }

        private int EditableScoreA;
        private int EditableScoreB;

        private Modal InternalModal;
        protected override void OnInitialized()
        {
            EditableScoreA = Match.ScoreA;
            EditableScoreB = Match.ScoreB;
            RemainingPuckA = Match.RemainingPuckA;
            RemainingPuckB = Match.RemainingPuckB;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && OnModalReady.HasDelegate)
            {
                await OnModalReady.InvokeAsync(InternalModal);
            }
        }

        private async Task OnConfirmedModalClick()
        {
            if (IsScoresCorrect() && IsRemainingPuckCorrect())
            {
                await OnConfirmed.InvokeAsync((EditableScoreA, EditableScoreB, RemainingPuckA, RemainingPuckB));
                await InternalModal.HideAsync();
            }
        }
        private bool IsScoresCorrect()
        {
            return EditableScoreA is >= 0 && EditableScoreB is >= 0;
        }

        private bool IsRemainingPuckCorrect()
        {
            return RemainingPuckA is >= 0 and <= 5 && RemainingPuckB is >= 0 and <= 5;
        }
    }
}
