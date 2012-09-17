
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

namespace ParcelableDemo
{
	[Activity (Label = "Select Item")]			
	public class SecondActivity : ListActivity
	{
		public const string ItemsValueKey = "Items";
		public const string SelectedValueKey = "SelectedValue";

		private SelectListItem[] _items;
		private string[] _stringItems;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Get the items from the intent, it's in an IList with no type info, so we cast and put into an array
			_items = Intent.GetParcelableArrayListExtra(ItemsValueKey).Cast<SelectListItem>().ToArray();
			_stringItems = _items.Select(item => item.Name).ToArray();

			ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, _stringItems);
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var intent = new Intent((string)null);

			// Return the selected value as a string
			intent.PutExtra(SelectedValueKey, string.Format("{0} - {1}", _items[position].Id, _items[position].Name));
			SetResult(Result.Ok, intent);

			// The user has selected so we're done now
			Finish();
		}
	}
}

