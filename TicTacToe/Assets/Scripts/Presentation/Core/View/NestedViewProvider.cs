using UnityEngine;

namespace Presentation
{
	/// <summary>
	/// Describes view provider for view that already exists in other presenter and just passed here to delegate
	/// management of it to another presenter 
	/// </summary>
	public class NestedViewProvider<TView> : IViewProvider<TView>
		where TView : PresentationView
	{
		private TView _view;

		public NestedViewProvider(TView view)
		{
			_view = view;
		}

		public TView Get(Transform parent = null) => _view;
	}
}