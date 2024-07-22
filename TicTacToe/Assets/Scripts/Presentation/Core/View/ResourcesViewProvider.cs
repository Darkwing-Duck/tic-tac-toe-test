using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Presentation
{
	public class ResourcesViewProvider<TView> : IViewProvider<TView>, ICanRelease<TView>
		where TView : MonoBehaviour
	{
		private HashSet<TView> _views = new();

		public TView Get(Transform parent = null)
		{
			var prefab = Resources.Load<TView>($"P_{typeof(TView).Name}");
			var view = Object.Instantiate(prefab, parent);
			_views.Add(view);
			return view;
		}

		public void Release(TView view)
		{
			if (!_views.Contains(view)) {
				throw new ArgumentException($"The view '{view.name}' was not created through the provider, so, it can't be released here.");
			}

			_views.Remove(view);
			Object.Destroy(view.gameObject);
		}
	}
}