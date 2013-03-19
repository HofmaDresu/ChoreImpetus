
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChoreImpetus.Core.Android.DatabaseObjects;
using ChoreImpetus.Core.Android.BusinessLogic;

namespace ChoreImpetusAndroid.Activities
{
	[Activity (Label = "Create Chore")]			
	public class CreateChore : Activity
	{
		const int DATE_DIALOG_ID = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.CreateChore);

			var dueDate = FindViewById<EditText>(Resource.Id.DueDateInput);
			dueDate.Click += (sender, e) => { ShowDialog (DATE_DIALOG_ID); };
			dueDate.Touch += (sender, e) => { ShowDialog (DATE_DIALOG_ID); };


			Button cancelButton = FindViewById<Button> (Resource.Id.CancelButton);

			cancelButton.Click += (sender, e) => {
				StartActivity(typeof(MainActivity));
			};

			Button createButton = FindViewById<Button> (Resource.Id.CreateButton);

			createButton.Click += (sender, e) => {
				var choreName = FindViewById<EditText>(Resource.Id.ChoreNameInput);

				var c = new Chore() {
					ChoreName = choreName.Text,
					DueDate = DateTime.Parse(dueDate.Text)
				};
				ChoreManager.SaveChore(c);


				StartActivity(typeof(MainActivity));
			};




		}

		void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			var dueDate = FindViewById<EditText>(Resource.Id.DueDateInput);
			dueDate.Text = e.Date.ToShortDateString();
		}

		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
				case DATE_DIALOG_ID:
					DateTime dueDate;
					if( !(DateTime.TryParse(FindViewById<EditText>(Resource.Id.DueDateInput).Text, out dueDate)))
					{
						dueDate = DateTime.Now;
					}
					return new DatePickerDialog (this, OnDateSet, dueDate.Year, dueDate.Month - 1, dueDate.Day); 
			}
			return null;
		}
	}
}

