using Presentation;
using VContainer.Unity;

namespace Infrastructure
{
	public class Startup : IInitializable
	{
		private LifetimeScope _scope;
		private IPresenterFactory _presenterFactory;

		public Startup(LifetimeScope scope, IPresenterFactory presenterFactory)
		{
			_scope = scope;
			_presenterFactory = presenterFactory;
		}

		public void Initialize()
		{
			var presenter = _presenterFactory.Create<AppNavigationPresenter>();
			presenter.ShowUnder(_scope.transform);
		}
	}
}