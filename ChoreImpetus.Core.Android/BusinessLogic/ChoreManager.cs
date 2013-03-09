using System;
using ChoreImpetus.Core.Android.DatabaseObjects;
using System.Collections.Generic;
using System.Linq;

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
			var chore = GetChore (id);
			var nextDueDate = CalculateNextDueDate (chore);
			var completed = new CompletedChore(chore, DateTime.Now);
			ChoreRepository.SaveCompletedChore(completed);

			if (nextDueDate.HasValue && nextDueDate.Value <= chore.ChoreRecurrence.EndDate) {
				chore.DueDate = nextDueDate.Value;
				return SaveChore(chore);
			}
			else {
				return DeleteChore(id);
			}
		}

		private static DateTime? CalculateNextDueDate(Chore c)
		{
			DateTime? nextDate = null;
			switch (c.ChoreRecurrence.Pattern) 
			{
				case RecurrencePattern.Daily:
					nextDate = c.DueDate;
					while (nextDate.Value.Date <= DateTime.Now.Date) {
						nextDate = nextDate.Value.AddDays(1);
					}
					break;
				case RecurrencePattern.Weekly:
					nextDate = c.DueDate;
					while (nextDate.Value.Date <= DateTime.Now.Date) {
						nextDate = nextDate.Value.AddDays(7);
					}
				break;
				case RecurrencePattern.BiWeekly:
					nextDate = c.DueDate;
					while (nextDate.Value.Date <= DateTime.Now.Date) {
						nextDate = nextDate.Value.AddDays(14);
					}
					break;
				case RecurrencePattern.Monthly:
					nextDate = c.DueDate;
					while (nextDate.Value.Date <= DateTime.Now.Date) {
						nextDate = nextDate.Value.AddMonths(1);
					}
					break;
				case RecurrencePattern.Yearly:
					nextDate = c.DueDate;
					while (nextDate.Value.Date <= DateTime.Now.Date) {
						nextDate = nextDate.Value.AddYears(1);
					}
					break;
			}

			return nextDate;
		}
	}
}

