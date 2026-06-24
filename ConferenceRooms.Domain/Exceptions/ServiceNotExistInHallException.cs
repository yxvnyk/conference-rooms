using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Domain.Exceptions
{
	public class ServiceNotExistInHallException(string message = "Service not exist in hall", string title = "Invalid data") :
		BaseCustomlException(message, 409, title);
}
