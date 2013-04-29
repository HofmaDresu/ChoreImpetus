using System;
using System.Collections.Generic;
using ChoreImpetus.Core.Android.DatabaseObjects;
using System.Linq;

namespace ChoreImpetus.Core.Android.Databases
{
	public class ChoreDatabase : SQLiteConnection
	{
		static object locker = new object ();
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public ChoreDatabase (string path) : base (path)
		{
			// create the tables
			CreateTable<Recurrence> ();
			CreateTable<Chore> ();
			CreateTable<CompletedChore> ();
		}
		
		public IEnumerable<T> GetItems<T> () where T : Contracts.IDatabaseEntity, new ()
		{
			lock (locker) {
				return (from i in Table<T> () select i).ToList();
			}
		}
		
		public T GetItem<T> (int id) where T : Contracts.IDatabaseEntity, new ()
		{
			lock (locker) {
				return (from i in Table<T> ()
				        select i).ToList ().Where(i => i.ID == id).FirstOrDefault ();
			}
		}
		
		public int SaveItem<T> (T item) where T : Contracts.IDatabaseEntity
		{
			lock (locker) {
				if (item.ID != 0) {
					Update (item);
					return item.ID;
				} else {
					return Insert (item);
				}
			}
		}
		
		public int DeleteItem<T>(int id) where T : Contracts.IDatabaseEntity, new ()
		{
			lock (locker) {
				return Delete<T> (id );
			}
		}
	}
}

