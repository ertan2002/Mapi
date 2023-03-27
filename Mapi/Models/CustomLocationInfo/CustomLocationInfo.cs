using System;
namespace Mapi.Models.CustomLocationInfo
{
	public class CustomLocationInfo
	{
		public string Address { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
        public AddressType AddressType { get; set; }
    }
}