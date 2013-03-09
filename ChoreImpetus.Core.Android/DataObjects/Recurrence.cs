using System;
using ChoreImpetus.Core.Android.Contracts;
using System.Collections.Generic;

namespace ChoreImpetus.Core.Android.DatabaseObjects
{
	public class Recurrence : IDatabaseEntity
	{
		public Recurrence ()
		{
		}
		
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public Pattern RecurrencePattern { get; set; }
		public IList<DayOfWeek> RecurrenceDays { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}

	public enum Pattern
	{
		OneTime,
		Daily,
		Weekly,
		Monthly,
		Yearly
	}

	public enum DayOfWeek
	{
		Monday,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday
	}
}

