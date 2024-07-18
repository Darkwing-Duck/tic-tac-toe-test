using UnityEngine;

namespace Modules.Core.Storage
{
	
	/// <summary>
	/// Describes PlayerPrefs module's storage implementation.
	/// </summary>
	public class PlayerPrefsModuleStorage : ModuleStorage
	{
		public PlayerPrefsModuleStorage(string modulePath) : base(modulePath)
		{ }

		protected override void SetValue(string path, string value)
		{
			PlayerPrefs.SetString(path, value);
			PlayerPrefs.Save();
		}

		protected override string GetValue(string path, string defaultValue) => 
			PlayerPrefs.GetString(path, defaultValue);

		protected override bool HasValue(string path) => PlayerPrefs.HasKey(path);
		protected override void RemoveValue(string path) => PlayerPrefs.DeleteKey(path);
		public override void Clear() => PlayerPrefs.DeleteAll();
	}
}