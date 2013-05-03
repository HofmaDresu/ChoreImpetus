using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ChoreImpetus.Core.Android.DatabaseObjects;
using ChoreImpetus.Core.Android.BusinessLogic;

namespace ChoreImpetusAndroid.Activities
{
	[Activity (Label = "Chore Impetus", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private Adapters.ChoreListAdapter choreAdapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			ListView choreList = FindViewById<ListView> (Resource.Id.ChoreList);

			choreList.ItemClick += OnItemClick;
			choreList.ItemLongClick += OnItemLongClick;

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += (sender, e) => {
				StartActivity(typeof(CreateChore));
			};
		}

		private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var editIntent = new Intent(this, typeof(EditChore));
			editIntent.PutExtra("ChoreId", ChoreManager.GetChores().OrderBy(c => c.DueDate).ToList()[(int)e.Id].ID);
			StartActivity(editIntent);
		}

		private void OnItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			ChoreManager.CompleteChore(ChoreManager.GetChores().OrderBy(c => c.DueDate).ToList()[(int)e.Id].ID);
			RefreshChores();
		}

		protected override void OnResume()
		{
			base.OnResume ();

			RefreshChores();
		}

		void RefreshChores()
		{
			choreAdapter = new Adapters.ChoreListAdapter (this, ChoreManager.GetChores().OrderBy(c => c.DueDate));
			ListView choreList = FindViewById<ListView> (Resource.Id.ChoreList);
			choreList.Adapter = choreAdapter;
		}
	}
}


