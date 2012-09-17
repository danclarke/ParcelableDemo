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

using Android.OS;
using Java.Interop;

namespace ParcelableDemo
{
	public sealed class SelectListItem : Java.Lang.Object, IParcelable
	{
		// Convenience constructors
		public SelectListItem() {}
		public SelectListItem(int id, string name)
		{
			Id = id;
			Name = name;
		}

		// The actual properties for this class
		public int Id { get; set; }
		public string Name { get; set; }

		#region IParcelable implementation

		// The creator creates an instance of the specified object
		private static readonly GenericParcelableCreator<SelectListItem> _creator 
			= new GenericParcelableCreator<SelectListItem>((parcel) => new SelectListItem(parcel));

		[ExportField("CREATOR")]
		public static GenericParcelableCreator<SelectListItem> GetCreator()
		{
			return _creator;
		}

		// Create a new SelectListItem populated with the values in parcel
		private SelectListItem(Parcel parcel)
		{
			Id = parcel.ReadInt();
			Name = parcel.ReadString();
		}

		public int DescribeContents()
		{
			return 0;
		}

		// Save this instance's values to the parcel
		public void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
		{
			dest.WriteInt(Id);
			dest.WriteString(Name);
		}

		// Closest to the 'Java' way of implementing the creator
		/*public sealed class SelectListItemCreator : Java.Lang.Object, IParcelableCreator
		{
			public Java.Lang.Object CreateFromParcel(Parcel source)
			{
				return new SelectListItem(source);
			}

			public Java.Lang.Object[] NewArray(int size)
			{
				return new SelectListItem[size];
			}
		}*/

		#endregion
	}

	/// <summary>
	/// Generic Parcelable creator that can be used to create objects from parcels
	/// </summary>
	public sealed class GenericParcelableCreator<T> : Java.Lang.Object, IParcelableCreator
		where T : Java.Lang.Object, new()
	{
		private readonly Func<Parcel, T> _createFunc;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParcelableDemo.GenericParcelableCreator`1"/> class.
		/// </summary>
		/// <param name='createFromParcelFunc'>
		/// Func that creates an instance of T, populated with the values from the parcel parameter
		/// </param>
		public GenericParcelableCreator(Func<Parcel, T> createFromParcelFunc)
		{
			_createFunc = createFromParcelFunc;
		}

		#region IParcelableCreator Implementation

		public Java.Lang.Object CreateFromParcel(Parcel source)
		{
			return _createFunc(source);
		}
		
		public Java.Lang.Object[] NewArray(int size)
		{
			return new T[size];
		}

		#endregion
	}
}

