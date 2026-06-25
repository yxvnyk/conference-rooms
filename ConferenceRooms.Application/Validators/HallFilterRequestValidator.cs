using ConferenceRooms.Application.DTO.Halls;
using FluentValidation;

namespace ConferenceRooms.Application.Validators
{
	public class HallFilterRequestValidator : AbstractValidator<HallFilterRequest>
	{
		public HallFilterRequestValidator() 
		{
			RuleFor(x => x.StartTime)
				.Must(x => x > DateTime.UtcNow)
				.WithMessage("Start time must be in the future.");

			RuleFor(x => x.Duration)
				.Must(x => x > 0 && x < 12)
				.WithMessage("Duration must be more then at least 1 hour, and not more then 12 hours");

			RuleFor(x => x.Capacity)
				.GreaterThanOrEqualTo(0)
				.LessThanOrEqualTo(1000);
		}
	}
}
