using System;
using ChoreImpetus.Core.Android.Databases;
using System.IO;

namespace ChoreImpetus.Core.Android
{
	public class BaseRepository
	{
		protected static ChoreDatabase db = null;
		protected static string dbLocation;		
		protected static BaseRepository me;		
		
		static BaseRepository ()
		{
			me = new BaseRepository();
		}
		
		protected BaseRepository()
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
	}
}

