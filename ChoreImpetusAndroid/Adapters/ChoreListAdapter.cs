using System;
using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Widget;

using ChoreImpetus.Core.Android.DatabaseObjects;
using ChoreImpetus.Core.Android.BusinessLogic;
using Android.Graphics;

namespace ChoreImpetusAndroid.Adapters
{
	public class ChoreListAdapter : BaseAdapter<Chore> 
	{
		protected Activity context = null;
		protected IList<Chore> chores = new List<Chore>();
		
		public ChoreListAdapter (Activity context, IEnumerable<Chore> chores) : base ()
		{
			this.context = context;
			this.chores = chores.ToList ();
		}
		
		public override Chore this[int position]
		{
			get { return chores[position]; }
		}
		
		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override int Count
		{
			get { return chores.Count; }
		}
		
		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get our object for position
			var item = chores[position];			
			
			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
			var view = (convertView ?? 
			            context.LayoutInflater.Inflate(
				Resource.Layout.ChoreListItem, 
				parent, 
				false)) as LinearLayout;
			
			// Find references to each subview in the list item's view
			var name = view.FindViewById<TextView>(Resource.Id.name);
			var description = view.FindViewById<TextView>(Resource.Id.dueDate);
			
			//Assign item's values to the various subviews
			name.SetText (item.ChoreName, TextView.BufferType.Normal);
			description.SetText (item.DueDate.ToShortDateString(), TextView.BufferType.Normal);

			//Color if overdue
			if (item.DueDate.Date < DateTime.Now.Date) {
				name.SetTextColor(new Color(255, 0, 0));
				description.SetTextColor(new Color(255, 0, 0));
			} else 

			//Color if Due Today
			if (item.DueDate.Date == DateTime.Now.Date) {
				name.SetTextColor(new Color(255, 165, 0));
				description.SetTextColor(new Color(255, 165, 0));
			} else {
				name.SetTextColor(new Color(255, 255, 255));
				description.SetTextColor(new Color(255, 255, 255));
			}

			//Finally return the view
			return view;

		}
	}
}

