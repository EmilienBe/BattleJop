namespace BattleJop.Api.Web.Validator;

using BattleJop.Api.Web.Dtos.AddTournament;
using FluentValidation;

public class AddTournamentValidator : AbstractValidator<AddTournamentRequest>
{
    public AddTournamentValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
        RuleFor(t => t.NumberRounds).NotNull().GreaterThan(0);
    }
}
