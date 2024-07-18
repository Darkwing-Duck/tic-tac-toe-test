using Modules.Core.Storage;

namespace Presentation.Core
{
	
	/// <summary>
	/// Persistent model of the module.
	/// The model manages the persistent state object of the object,
	/// and can be used to save the state to passed storage.
	/// </summary>
	/// <typeparam name="TData"></typeparam>
	public abstract class PersistentModel<TData> where TData : new()
	{
		protected TData PersistentData = new();
		
		public void LoadFrom(IModuleStorage storage) => PersistentData = storage.Get(PersistentData);
		public void SaveTo(IModuleStorage storage) => storage.Set(PersistentData);
	}
}