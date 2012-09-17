using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ParcelableDemo
{
	[Activity(Label = "Parcelable Demo", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		private readonly SelectListItem[] Items = new[]
		{
			new SelectListItem(0, "Item 1"),
			new SelectListItem(1, "Item 2"),
			new SelectListItem(2, "Item 3"),
			new SelectListItem(3, "Item 4"),
			new SelectListItem(4, "Item 5"),
		};

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			FindViewById<Button>(Resource.Id.selectItemButton).Click += OnSelectItemClicked;
		}

		protected virtual void OnSelectItemClicked(object sender, EventArgs e)
		{
			var intent = new Intent(this, typeof(SecondActivity));

			// Put the items list into the intent
			intent.PutParcelableArrayListExtra(SecondActivity.ItemsValueKey, Items);

			// Start up the activity, requesting a result back
			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			// Ensure user didn't cancel
			if (resultCode != Result.Ok)
				return;

			// Set the text box to the returned value
			FindViewById<TextView>(Resource.Id.selectedText).Text = data.GetStringExtra(SecondActivity.SelectedValueKey);
		}
	}
}


