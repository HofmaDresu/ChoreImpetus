using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ChoreImpetus.Core.Android.DatabaseObjects;
using ChoreImpetus.Core.Android.BusinessLogic;

namespace ChoreImpetusAndroid
{
	[Activity (Label = "ChoreImpetusAndroid", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var chores = ChoreManager.GetChores ();

			ListView choreList = FindViewById<ListView> (Resource.Id.ChoreList);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
			};
		}
	}
}


