using ApplicationCore.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Features.ChangePassword;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters")
            .Matches(RegularExpressions.AtLeastOneDigit)
            .WithMessage("Password must contain at least 1 digit")
            .Matches(RegularExpressions.AtLeastOneLetter)
            .WithMessage("Password must contain at least 1 letter")
            .Matches(RegularExpressions.AtLeastOneLowercase)
            .WithMessage("Password must contain at least 1 lowercase letter")
            .Matches(RegularExpressions.AtLeastOneUppercase)
            .WithMessage("Password must contain at least 1 uppercase letter")
            .Matches(RegularExpressions.AtLeastOneSpecialCharacter)
            .WithMessage("Password must contain at least 1 special character");

        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters")
            .Matches(RegularExpressions.AtLeastOneDigit)
            .WithMessage("Password must contain at least 1 digit")
            .Matches(RegularExpressions.AtLeastOneLetter)
            .WithMessage("Password must contain at least 1 letter")
            .Matches(RegularExpressions.AtLeastOneLowercase)
            .WithMessage("Password must contain at least 1 lowercase letter")
            .Matches(RegularExpressions.AtLeastOneUppercase)
            .WithMessage("Password must contain at least 1 uppercase letter")
            .Matches(RegularExpressions.AtLeastOneSpecialCharacter)
            .WithMessage("Password must contain at least 1 special character");
    }
}
