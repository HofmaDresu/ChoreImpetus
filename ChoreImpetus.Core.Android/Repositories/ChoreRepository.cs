using System;
using ChoreImpetus.Core.Android.Databases;
using ChoreImpetus.Core.Android.DatabaseObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChoreImpetus.Core.Android.Repositories
{
	public class ChoreRepository : BaseRepository
	{
		
		public static Chore GetChore(int id)
		{
			return db.GetItem<Chore>(id);
		}
		
		public static IEnumerable<Chore> GetChores ()
		{
			return db.GetItems<Chore>();
		}
		
		public static int SaveChore (Chore item)
		{
			return db.SaveItem<Chore>(item);
		}
		
		public static int DeleteChore(int id)
		{
			return db.DeleteItem<Chore>(id);
		}
		
		public static CompletedChore GetCompletedChore(int id)
		{
			return db.GetItem<CompletedChore>(id);
		}
		
		public static IEnumerable<CompletedChore> GetCompletedChore ()
		{
			return db.GetItems<CompletedChore>();
		}

		public static CompletedChore GetMostRecentCompletedChore(int choreId)
		{
			return db.GetItems<CompletedChore>().Where(cc => cc.ParentChoreId == choreId).OrderByDescending(cc => cc.CompletionDate).FirstOrDefault();
		}

		public static int SaveCompletedChore (CompletedChore item)
		{
			return db.SaveItem<CompletedChore>(item);
		}

		public static int DeleteCompletedChore (int id)
		{
			return db.DeleteItem<CompletedChore>(id);
		}
	}
}

