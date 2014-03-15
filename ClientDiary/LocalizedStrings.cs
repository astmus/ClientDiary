using ClientDiary.Resources;

namespace ClientDiary
{
	/// <summary>
	/// Provides access to string resources.
	/// </summary>
	public class LocalizedStrings
	{
		private static AppResources _localizedResources = new AppResources();

		public AppResources Localization { get { return _localizedResources; } }
	}
}