using Commands;
using VitalRouter;

namespace Presentation
{
	[Routes]
	public partial class AppNavigationPresenter : StatelessPresenter<AppNavigationView>
	{
		private IPresenter _currentScreen;
		private IPresenterFactory _presenterFactory;
		private ICommandSubscribable _router;
		
		public AppNavigationPresenter(
			IModuleViewProvider<AppNavigationView> viewProvider, 
			IPresenterFactory presenterFactory,
			ICommandSubscribable router) : base(viewProvider)
		{
			_presenterFactory = presenterFactory;
			_router = router;
		}

		protected override void InitializeView(AppNavigationView view)
		{
			view.name = "Navigation";
		}

		protected override void OnActivate()
		{
			MapTo(_router);
		}

		protected override void OnDeactivate()
		{
			UnmapRoutes();
			_currentScreen?.Hide();
		}
		
		public void On(HomeStateActivated cmd) => GoTo<HomeScreenPresenter>();
		public void On(GameStateActivated cmd) => GoTo<GameScreenPresenter>();

		private void GoTo<TScreen>() where TScreen : IPresenter
		{
			_currentScreen?.Hide();
			_currentScreen = _presenterFactory.Create<TScreen>();
			_currentScreen.ShowUnder(View.transform);
		}
	}
}