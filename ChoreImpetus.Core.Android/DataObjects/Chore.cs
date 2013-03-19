using System;
using ChoreImpetus.Core.Android.Contracts;

namespace ChoreImpetus.Core.Android.DatabaseObjects
{
	public class Chore : IDatabaseEntity
	{
		public Chore ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string ChoreName { get; set; }
		public int RecurrenceID { get; set; }
		public DateTime DueDate { get; set; }
	}
}

