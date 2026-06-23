using ConferenceRooms.Application.DTO.Halls;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Validators
{
	public class CreateHallRequestValidator : AbstractValidator<AddHallRequest>
	{
		public CreateHallRequestValidator() {
			RuleFor(x => x.Name)
				.NotNull()
				.MaximumLength(100);

			RuleFor(x => x.Capacity)
				.GreaterThanOrEqualTo(0)
				.LessThanOrEqualTo(1000);

			RuleFor(x => x.Cost)
				.GreaterThanOrEqualTo(0)
				.PrecisionScale(10, 2, false);

			RuleForEach(x => x.Services)
				.SetValidator(new HallServiceItemValidator());

		}
	}
}
