using System;
namespace TrueLayerChallenge.Models
{
	public class SpeciesResponse
	{
		public string name { get; set; }
		public Flavor_Text_Entries[] flavor_text_entries { get; set; }
	}
	public class Flavor_Text_Entries
	{
		public string flavor_text { get; set; }
		public Language language { get; set; }
		public Version version { get; set; }
	}
	public class Language
	{
		public string name { get; set; }
		public string url { get; set; }
	}

	public class TranslationResponse
	{
		public Success success { get; set; }
		public Contents contents { get; set; }
	}
	public class Success
	{
		public int total { get; set; }
	}
	public class Contents
	{
		public string translated { get; set; }
		public string text { get; set; }
		public string translation { get; set; }
	}
}
