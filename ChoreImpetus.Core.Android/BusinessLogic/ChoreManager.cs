using System;
using ChoreImpetus.Core.Android.DatabaseObjects;
using ChoreImpetus.Core.Android.Repositories;
using System.Collections.Generic;
using System.Linq;
using RecurrenceImpetus.Core.Android.Repositories;

namespace ChoreImpetus.Core.Android.BusinessLogic
{
	public static class ChoreManager
	{
		static ChoreManager ()
		{
		}
			
		public static Chore GetChore(int id)
		{
			return ChoreRepository.GetChore(id);
		}
		
		public static IList<Chore> GetChores ()
		{
			return new List<Chore>(ChoreRepository.GetChores());
		}
		
		public static int SaveChore (Chore item)
		{
			return ChoreRepository.SaveChore(item);
		}
		
		public static int DeleteChore(int id)
		{
			return ChoreRepository.DeleteChore(id);
		}

		public static int CompleteChore(int id)
		{
			var chore = GetChore(id);
			var recurrence = RecurrenceRepository.GetRecurrence (chore.RecurrenceID);
			var nextDueDate = CalculateNextDueDate (chore, recurrence);
			var completed = new CompletedChore(chore, DateTime.Now);
			ChoreRepository.SaveCompletedChore(completed);

			if (nextDueDate.HasValue && nextDueDate.Value <= recurrence.EndDate.GetValueOrDefault(new DateTime(9999, 12, 31))) {
				chore.DueDate = nextDueDate.Value;
				return SaveChore(chore);
			}
			else {
				return DeleteChore(id);
			}
		}

		private static DateTime? CalculateNextDueDate(Chore c, Recurrence r)
		{
			DateTime? nextDate = null;
			DateTime today = DateTime.Now.Date;
			DateTime baseNextDate = (c.DueDate.Date < today) ? today : c.DueDate.Date;
			if (r != null) {
				switch (r.Pattern) 
				{
					case RecurrencePattern.Daily:
						nextDate = c.DueDate;
						while (nextDate.Value.Date <= baseNextDate) {
							nextDate = nextDate.Value.AddDays(1);
						}
						break;
					case RecurrencePattern.Weekly:
						nextDate = c.DueDate;
						while (nextDate.Value.Date <= baseNextDate) {
							nextDate = nextDate.Value.AddDays(7);
						}
					break;
					case RecurrencePattern.BiWeekly:
						nextDate = c.DueDate;
						while (nextDate.Value.Date <= baseNextDate) {
							nextDate = nextDate.Value.AddDays(14);
						}
						break;
					case RecurrencePattern.Monthly:
						nextDate = c.DueDate;
						while (nextDate.Value.Date <= baseNextDate) {
							nextDate = nextDate.Value.AddMonths(1);
						}
						break;
					case RecurrencePattern.Yearly:
						nextDate = c.DueDate;
						while (nextDate.Value.Date <= baseNextDate) {
							nextDate = nextDate.Value.AddYears(1);
						}
						break;
				}
			}
			return nextDate;
		}
	}
}

