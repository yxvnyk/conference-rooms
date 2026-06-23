using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Domain.Exceptions
{
	public class NotUniqueHallNameException(string message = "Not unique hall name", string title = "Invalid data") :
		BaseCustomlException(message, 409, title);
}
