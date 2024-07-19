using Presentation;
using VContainer;

namespace Infrastructure
{
	public class PresenterFactory : IPresenterFactory
	{
		private IObjectResolver _container;
		
		public PresenterFactory(IObjectResolver container)
		{
			_container = container;
		}
		
		public TPresenter Create<TPresenter>() where TPresenter : IPresenter => 
			_container.Resolve<TPresenter>();
	}
}