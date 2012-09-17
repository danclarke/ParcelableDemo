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

using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
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

