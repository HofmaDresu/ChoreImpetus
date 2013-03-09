using System;
using ChoreImpetus.Core.Android.Contracts;

namespace ChoreImpetus.Core.Android.DatabaseObjects
{
	public class CompletedChore : IDatabaseEntity
	{
		public CompletedChore()
		{
		}

		public CompletedChore (Chore c, DateTime? completionDate = null, string completedBy = "")
		{
			ChoreName = c.ChoreName;
			CompletionDate = completionDate.GetValueOrDefault (DateTime.Now);
			CompletedBy = completedBy;
			ParentChoreId = c.ID;
		}
		
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string ChoreName { get; set; }
		public readonly int ParentChoreId;
		public DateTime? CompletionDate { get; set; }
		public string CompletedBy { get; set; }
	}
}

