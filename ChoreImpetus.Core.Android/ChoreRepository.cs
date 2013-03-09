using System;
using ChoreImpetus.Core.Android.Databases;
using ChoreImpetus.Core.Android.DatabaseObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChoreImpetus.Core.Android
{
	public class ChoreRepository
	{
		ChoreDatabase db = null;
		protected static string dbLocation;		
		protected static ChoreRepository me;		
		
		static ChoreRepository ()
		{
			me = new ChoreRepository();
		}
		
		protected ChoreRepository()
		{
			// set the db location
			dbLocation = DatabaseFilePath;
			
			// instantiate the database	
			db = new ChoreDatabase(dbLocation);
		}
		
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "ChoreImpetus-ChoreDB.db3";
				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
				#else
				
				#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "../Library/"); // Library folder
				#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
				#endif		
				return path;	
			}
		}
		
		public static Chore GetChore(int id)
		{
			return me.db.GetItem<Chore>(id);
		}
		
		public static IEnumerable<Chore> GetChores ()
		{
			return me.db.GetItems<Chore>();
		}
		
		public static int SaveChore (Chore item)
		{
			return me.db.SaveItem<Chore>(item);
		}
		
		public static int DeleteChore(int id)
		{
			return me.db.DeleteItem<Chore>(id);
		}
		
		public static CompletedChore GetCompletedChore(int id)
		{
			return me.db.GetItem<CompletedChore>(id);
		}
		
		public static IEnumerable<CompletedChore> GetCompletedChore ()
		{
			return me.db.GetItems<CompletedChore>();
		}

		public static CompletedChore GetMostRecentCompletedChore(int choreId)
		{
			return me.db.GetItems<CompletedChore>().Where(cc => cc.ParentChoreId == choreId).OrderByDescending(cc => cc.CompletionDate).FirstOrDefault();
		}

		public static int SaveCompletedChore (CompletedChore item)
		{
			return me.db.SaveItem<CompletedChore>(item);
		}

		public static int DeleteCompletedChore (int id)
		{
			return me.db.DeleteItem<CompletedChore>(id);
		}
	}
}

