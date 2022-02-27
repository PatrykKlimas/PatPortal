using FluentValidation;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Enums;

namespace PatPortal.Domain.Validators.Posts
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(post => post.Content)
                .NotNull()
                .NotEmpty()
                .Length(2, 500)
                .WithMessage("Content length must be between 2 and 500 characters.");

            RuleFor(post => post.Access)
                .NotEqual(DataAccess.Undefined)
                .WithMessage("Invalid date access has benn provided.");

            RuleFor(post => post.Photo)
                .HasValidImage();

            RuleFor(post => post.AddedDate)
                .NotNull()
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now + TimeSpan.FromMinutes(1))
                .WithMessage("Added time could not be from feature.");

            RuleFor(post => post.EditedTime)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(p => p.AddedDate)
                .WithMessage("Edited time could not be earlier than added time.");
        }
    }
}
