namespace ConferenceRooms.Application.Helpers
{
	/// <summary>
	/// A helper class for calculating the final price of conference hall bookings.
	/// </summary>
	public static class BookingPriceHelper
	{
		/// <summary>
		/// Calculates the total price for a booking, taking into account the base hourly rate, 
		/// selected services, and time-based discounts or markups.
		/// </summary>
		/// <remarks>
		/// The calculation processes the booking hour by hour to accurately apply dynamic pricing:
		/// <list type="bullet">
		/// <item>Morning (06:00 - 09:00): 10% discount</item>
		/// <item>Standard (09:00 - 12:00, 14:00 - 18:00): Base price</item>
		/// <item>Peak (12:00 - 14:00): 15% markup</item>
		/// <item>Evening (18:00 - 23:00): 20% discount</item>
		/// </list>
		/// </remarks>
		/// <param name="baseHallCost">The base cost of the hall per hour.</param>
		/// <param name="servicePrices">A collection of prices for the additionally requested services.</param>
		/// <param name="startTime">The start date and time of the booking.</param>
		/// <param name="endTime">The end date and time of the booking.</param>
		/// <returns>The calculated total price of the booking, including all services and hourly rate modifiers.</returns>
		public static decimal CalculateTotalPrice(
			decimal baseHallCost,
			IEnumerable<decimal> servicePrices,
			DateTime startTime,
			DateTime endTime)
		{
			// 1. Calculate services costs
			decimal totalServicesCost = servicePrices.Sum();

			// 2. Calculate hall cost
			decimal totalHallCost = 0;
			DateTime currentTime = startTime;

			// Calculate each hour separately
			while (currentTime < endTime)
			{
				decimal hourlyRate = baseHallCost;

				decimal multiplier = GetMultiplierForHour(currentTime.Hour);

				totalHallCost += (hourlyRate * multiplier);

				currentTime = currentTime.AddHours(1);
			}

			return totalHallCost + totalServicesCost;
		}

		/// <summary>
		/// Determines the pricing multiplier for a specific hour of the day based on business rules.
		/// </summary>
		/// <param name="hour">The hour of the day (0-23) to evaluate.</param>
		/// <returns>A decimal multiplier (e.g., 0.8 for a 20% discount, 1.15 for a 15% markup).</returns>
		private static decimal GetMultiplierForHour(int hour)
		{
			// Standard (09:00 - 18:00) -> 1.0
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