
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
		protected enum DialogIds
		{
			DUE_DATE_DIALOG,
			END_DATE_DIALOG
		};


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.CreateChore);

			var dueDate = FindViewById<EditText>(Resource.Id.DueDateInput);
			dueDate.Click += (sender, e) => { ShowDialog ((int)DialogIds.DUE_DATE_DIALOG); };
			dueDate.Touch += (sender, e) => { ShowDialog ((int)DialogIds.DUE_DATE_DIALOG); };

			var endDate = FindViewById<EditText>(Resource.Id.EndDateInput);
			endDate.Click += (sender, e) => { ShowDialog ((int)DialogIds.END_DATE_DIALOG); };
			endDate.Touch += (sender, e) => { ShowDialog ((int)DialogIds.END_DATE_DIALOG); };
			endDate.Enabled = false;


			var recurrence = FindViewById<Spinner>(Resource.Id.RecurrencePicker);
			recurrence.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
			var array = Enum.GetValues(typeof(RecurrencePattern)).Cast<RecurrencePattern>();

			var adapter = new ArrayAdapter<RecurrencePattern>(this, Android.Resource.Layout.SimpleSpinnerItem, array.ToList<RecurrencePattern>());
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

				
			recurrence.Adapter = adapter;

			Button cancelButton = FindViewById<Button> (Resource.Id.CancelButton);

			cancelButton.Click += (sender, e) => {
				StartActivity(typeof(MainActivity));
			};

			Button createButton = FindViewById<Button> (Resource.Id.CreateButton);

			createButton.Click += CreateButtonClicked;
		}

		private void CreateButtonClicked(Object sender, EventArgs e)
		{
			var choreName = FindViewById<EditText>(Resource.Id.ChoreNameInput);
			var dueDate = FindViewById<EditText>(Resource.Id.DueDateInput);
			var endDate = FindViewById<EditText>(Resource.Id.EndDateInput);
			var recurrencePicker = FindViewById<Spinner>(Resource.Id.RecurrencePicker);

			var c = new Chore() {
				ChoreName = choreName.Text,
				DueDate = DateTime.Parse(dueDate.Text)
			};
			ChoreManager.SaveChore(c);

			DateTime endRecurrence;
			
			var recurrence = new Recurrence()
			{
				ChoreID = c.ID,
				EndDate = DateTime.TryParse(endDate.Text, out endRecurrence) ? endRecurrence : (DateTime?)null,
				Pattern = (RecurrencePattern)recurrencePicker.SelectedItemId,
				StartDate = DateTime.Parse(dueDate.Text)
			};

			RecurrenceManager.SaveRecurrence(recurrence);
			c.RecurrenceID = recurrence.ID;
			ChoreManager.SaveChore(c);			
			
			StartActivity(typeof(MainActivity));
		}

		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var recurrence = FindViewById<Spinner>(Resource.Id.RecurrencePicker);
			var endDate = FindViewById<EditText>(Resource.Id.EndDateInput);
			Spinner spinner = (Spinner)sender;
			if ((RecurrencePattern)spinner.SelectedItemId == RecurrencePattern.OneTime) {
				endDate.Text = String.Empty;
				endDate.Enabled = false;
			}
			else {
				endDate.Enabled = true;
			}
		}

		void OnDueDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			var dueDate = FindViewById<EditText>(Resource.Id.DueDateInput);
			dueDate.Text = e.Date.ToShortDateString();
		}
		
		void OnEndDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			var endDate = FindViewById<EditText>(Resource.Id.EndDateInput);
			endDate.Text = e.Date.ToShortDateString();
		}

		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
				case (int)DialogIds.DUE_DATE_DIALOG:
					DateTime dueDate;
					if( !(DateTime.TryParse(FindViewById<EditText>(Resource.Id.DueDateInput).Text, out dueDate)))
					{
						dueDate = DateTime.Now;
					}
					return new DatePickerDialog (this, OnDueDateSet, dueDate.Year, dueDate.Month - 1, dueDate.Day); 
				case (int)DialogIds.END_DATE_DIALOG:
					DateTime endDate;
					if(!(DateTime.TryParse(FindViewById<EditText>(Resource.Id.EndDateInput).Text, out endDate)))
					{
						endDate = DateTime.Now;
					}
					return new DatePickerDialog (this, OnEndDateSet, endDate.Year, endDate.Month - 1, endDate.Day); 
				default:
					throw new Exception("Dialog type not supported");
			}
		}
	}
}

