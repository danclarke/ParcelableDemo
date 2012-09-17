/*

Licensed under the BSD license:

Copyright (c) 2012, Dan Clarke
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of Dan Clarke nor the names of contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;

using Android.App;
using Android.Content;
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


