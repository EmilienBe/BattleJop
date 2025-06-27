using BattleJop.Api.Web.Dtos;
using FluentValidation;

namespace BattleJop.Api.Web.Validator;

public class AddTeamValidator : AbstractValidator<AddTeamRequest>
{
    public AddTeamValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
        RuleFor(r => r.Players).NotNull();

        When(r => r.Players != null, () =>
        {
            RuleFor(r => r.Players.Count).GreaterThan(0);
            RuleForEach(r => r.Players).NotEmpty();
        });
    }
}