using System;
using ChoreImpetus.Core.Android.Contracts;

namespace ChoreImpetus.Core.Android.DatabaseObjects
{
	public class CompletedChore : IDatabaseEntity
	{
		public CompletedChore()
		{
		}

		public CompletedChore (Chore c, DateTime? completionDate, string completedBy)
		{
			ChoreName = c.ChoreName;
			CompletionDate = completionDate.GetValueOrDefault (DateTime.Now);
			CompletedBy = completedBy ?? String.Empty;
		}
		
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string ChoreName { get; set; }
		public DateTime? CompletionDate { get; set; }
		public string CompletedBy { get; set; }
	}
}

