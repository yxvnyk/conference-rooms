using ConferenceRooms.Application.DTO.Halls;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Validators
{
	public class UpdateHallRequestValidator : AbstractValidator<UpdateHallRequest>
	{
		public UpdateHallRequestValidator()
		{
			RuleForEach(x => x.ServiceItems).SetValidator(new HallServiceItemValidator());
		}
	}
}
