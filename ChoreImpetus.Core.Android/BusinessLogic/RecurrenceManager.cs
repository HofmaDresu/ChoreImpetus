using System;
using ChoreImpetus.Core.Android.DatabaseObjects;
using RecurrenceImpetus.Core.Android.Repositories;

namespace ChoreImpetus.Core.Android.BusinessLogic
{
	public static class RecurrenceManager
	{
		static RecurrenceManager ()
		{
		}

		public static Recurrence GetRecurrence(int id)
		{
			return RecurrenceRepository.GetRecurrence(id);
		}
		
		public static int SaveRecurrence (Recurrence item)
		{
			return RecurrenceRepository.SaveRecurrence(item);
		}
		
		public static int DeleteRecurrence(int id)
		{
			return RecurrenceRepository.DeleteRecurrence(id);
		}
	}
}

