using ConferenceRooms.Application.DTO.Halls;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Validators
{
	public class HallFilterRequestValidator : AbstractValidator<HallFilterRequest>
	{
		public HallFilterRequestValidator() {
		}
	}
}
