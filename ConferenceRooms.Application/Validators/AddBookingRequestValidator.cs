using ConferenceRooms.Application.DTO.Booking;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Validators
{
	public class AddBookingRequestValidator : AbstractValidator<AddBookingRequest>
	{
		public AddBookingRequestValidator() {
			RuleFor(x => x.HallId)
				.NotNull();

			RuleFor(x => x.StartTime)
				.Must(x => x > DateTime.UtcNow)
				.WithMessage("Start time must be in the future.");


			RuleFor(x => x.DurationHours)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(12);

		}
	}
}
