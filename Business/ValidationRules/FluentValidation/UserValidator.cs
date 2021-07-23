using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concretes;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Mail adresi geçerli değil!");
            RuleFor(u => u.FirstName).MinimumLength(2).WithMessage("İsim en az {PropertyValue} kadar olabilir");
            RuleFor(u => u.Status).NotNull();

        }
    }
}
