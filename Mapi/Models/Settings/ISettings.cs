using System;
namespace Mapi.Models.Settings
{
	public interface ISettings
	{
		public string GoogleApiKey { get; set; }
		public string GoogleApiUrl { get; set; }
	}
}

