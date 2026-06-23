using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Validators
{
	public class HallServiceItemValidator
	: AbstractValidator<HallServiceItem>
	{
		public HallServiceItemValidator()
		{
			RuleFor(x => x.ServiceId)
				.NotEmpty();

			RuleFor(x => x.Price)
				.GreaterThanOrEqualTo(0)
				.PrecisionScale(10, 2, false);
		}
	}
}
