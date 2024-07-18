namespace Modules.Core.Storage
{
	
	/// <summary>
	/// Describes common module's storage interface.
	/// </summary>
	public interface IModuleStorage
	{
		bool Has();
		void Set(object value);
		T Get<T>(T defaultValue);

		void Remove();
		void Clear();
	}
}