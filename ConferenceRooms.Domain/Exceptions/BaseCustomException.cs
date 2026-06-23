using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Domain.Exceptions
{
	public class BaseCustomlException : Exception
	{
		protected BaseCustomlException(string message, int statusCode, string? errorCode = null)
			: base(message)
		{
			StatusCode = statusCode;
			ErrorCode = errorCode!;
		}

		public int StatusCode { get; }

		public string ErrorCode { get; }
	}
}
