using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Domain.Exceptions
{
	public class HallNotExistException(string message = "Hall not exist", string title = "Invalid data") :
		BaseCustomlException(message, 409, title);
}
