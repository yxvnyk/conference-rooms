using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRooms.Application.Helpers
{
	public static class BookingPriceHelper
	{
		public static decimal CalculateTotalPrice(
			decimal baseHallCost,
			IEnumerable<decimal> servicePrices,
			DateTime startTime,
			DateTime endTime)
		{
			// 1. calcualte services costs
			decimal totalServicesCost = servicePrices.Sum();

			// 2. calculate hall cost
			decimal totalHallCost = 0;
			DateTime currentTime = startTime;

			// calculate each hour separetely
			while (currentTime < endTime)
			{
				decimal hourlyRate = baseHallCost;

				decimal multiplier = GetMultiplierForHour(currentTime.Hour);

				totalHallCost += (hourlyRate * multiplier);

				currentTime = currentTime.AddHours(1);
			}

			return totalHallCost + totalServicesCost;
		}

		private static decimal GetMultiplierForHour(int hour)
		{
			// Standart (09:00 - 18:00) -> 1.0
			// Evening (18:00 - 23:00) -> 0.8 (-20%)
			// Morning (06:00 - 09:00) -> 0.9 (-10%)
			// Peak (12:00 - 14:00) -> 1.15 (+15%)

			if (hour >= 18 && hour < 23) return 0.8m;
			if (hour >= 6 && hour < 9) return 0.9m;
			if (hour >= 12 && hour < 14) return 1.15m;

			return 1.0m;
		}
	}
}
