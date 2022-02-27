using FluentValidation;
using PatPortal.Domain.Entities.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Validators.Comments
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(comment => comment.Content)
                .NotEmpty()
                .NotNull()
                .Length(2, 500)
                .WithMessage("Comment length must be between 2 and 500 characters.");

            RuleFor(comment => comment.AddedDate)
                .NotEmpty()
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now + TimeSpan.FromMinutes(1))
                .WithMessage("Added time could not be from feature.");

            RuleFor(comment => comment.EditedTime)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(comment => comment.AddedDate)
                .WithMessage("Edited time could not be earlier than added time.");
        }
    }
}
