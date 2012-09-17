using System;

using Android.OS;
using Java.Interop;

namespace ParcelableDemo
{
	public class SelectListItem : Java.Lang.Object, IParcelable
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
	public class GenericParcelableCreator<T> : Java.Lang.Object, IParcelableCreator
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

