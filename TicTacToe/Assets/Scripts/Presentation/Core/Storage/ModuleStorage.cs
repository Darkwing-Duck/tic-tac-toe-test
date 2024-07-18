using System.IO;
using Unity.Plastic.Newtonsoft.Json;

namespace Modules.Core.Storage
{
	
	/// <summary>
	/// Describes common module's storage implementation.
	/// </summary>
	public abstract class ModuleStorage : IModuleStorage
	{
		private const string RootPath = "Data/Modules";
		private readonly string _modulePath;

		protected ModuleStorage(string modulePath)
		{
			_modulePath = modulePath;
		}

		public bool Has() => HasValue(GetFullPathOf());

		public void Set(object value)
		{
			var jsonData = JsonConvert.SerializeObject(value);
			SetValue(GetFullPathOf(), jsonData);
		}

		public T Get<T>(T defaultValue)
		{
			var data = GetValue(GetFullPathOf(), null);
			return string.IsNullOrEmpty(data) 
				? defaultValue 
				: JsonConvert.DeserializeObject<T>(data);
		}

		public void Remove()
		{
			RemoveValue(GetFullPathOf());
		}
		
		public abstract void Clear();

		protected abstract void SetValue(string path, string value);
		protected abstract string GetValue(string path, string value);
		protected abstract bool HasValue(string path);
		protected abstract void RemoveValue(string path);
		protected string GetFullPathOf() => Path.Combine(RootPath, _modulePath);
	}
}