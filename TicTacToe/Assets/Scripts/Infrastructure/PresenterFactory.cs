using Presentation;
using VContainer;

namespace Infrastructure
{
	/// <summary>
	/// Presenters factory
	/// </summary>
	public class PresenterFactory : IPresenterFactory
	{
		private IObjectResolver _container;
		
		public PresenterFactory(IObjectResolver container)
		{
			_container = container;
		}
		
		/// <summary>
		/// Creates presenter of specified type and resolves constructor dependencies
		/// </summary>
		public TPresenter Create<TPresenter>() where TPresenter : IPresenter => 
			_container.Resolve<TPresenter>();
	}
}