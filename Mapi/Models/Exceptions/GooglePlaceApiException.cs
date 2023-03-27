using System;
namespace Mapi.Models.Exceptions
{
	public class GooglePlaceApiException : Exception
	{
		public GooglePlaceApiException(string message) : base(message) { }
	}
}