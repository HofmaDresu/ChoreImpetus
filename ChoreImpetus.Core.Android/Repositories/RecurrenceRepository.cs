using System;
using ChoreImpetus.Core.Android;
using ChoreImpetus.Core.Android.DatabaseObjects;
using System.Collections.Generic;

namespace RecurrenceImpetus.Core.Android.Repositories
{
	public class RecurrenceRepository : BaseRepository
	{
		public static Recurrence GetRecurrence(int id)
		{
			return db.GetItem<Recurrence>(id);
		}
		
		public static int SaveRecurrence (Recurrence item)
		{
			return db.SaveItem<Recurrence>(item);
		}
		
		public static int DeleteRecurrence(int id)
		{
			return db.DeleteItem<Recurrence>(id);
		}
	}
}

