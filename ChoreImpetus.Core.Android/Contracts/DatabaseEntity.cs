using System;

namespace ChoreImpetus.Core.Android.Contracts
{
	public class DatabaseEntity : IDatabaseEntity {
		public DatabaseEntity ()
		{
		}
		
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}

