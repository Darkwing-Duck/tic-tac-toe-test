using UnityEngine;

namespace Presentation.Core
{
	public class ResourcesViewProvider<TView> : IModuleViewProvider<TView>
		where TView : MonoBehaviour
	{
		private TView _view;

		public TView Get(Transform parent = null)
		{
			var prefab = Resources.Load<TView>($"P_{typeof(TView).Name}");
			_view = Object.Instantiate(prefab, parent);
			return _view;
		}

		public void Release()
		{
			Object.Destroy(_view.gameObject);
		}
	}
}