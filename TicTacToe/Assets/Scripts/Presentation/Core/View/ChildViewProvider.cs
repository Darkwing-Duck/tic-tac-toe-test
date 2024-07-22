using UnityEngine;

namespace Presentation
{
	public class ChildViewProvider<TView> : IViewProvider<TView>
		where TView : PresentationView
	{
		private TView _view;

		public ChildViewProvider(TView view)
		{
			_view = view;
		}

		public TView Get(Transform parent = null) => _view;

		public void Release() { }
	}
}