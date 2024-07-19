namespace Presentation
{
	public interface IScreen
	{ }
	
	public interface IAppNavigator
	{
		void GoTo<TScreen>() where TScreen : IScreen, IPresenter;
	}
	
	public class AppNavigationPresenter : StatelessPresenter<AppNavigationView>, IAppNavigator
	{
		private IPresenter _currentScreen;
		private IPresenterFactory _presenterFactory;
		
		public AppNavigationPresenter(IModuleViewProvider<AppNavigationView> viewProvider, IPresenterFactory presenterFactory) : base(viewProvider)
		{
			_presenterFactory = presenterFactory;
		}

		protected override void InitializeView(AppNavigationView view)
		{
			view.name = "Navigation";
		}

		protected override void OnActivate()
		{
			GoTo<LoadingScreenPresenter>();
		}

		protected override void OnDeactivate()
		{
			_currentScreen?.Hide();
		}

		public void GoTo<TScreen>() where TScreen : IScreen, IPresenter
		{
			_currentScreen?.Hide();
			_currentScreen = _presenterFactory.Create<TScreen>();
			_currentScreen.ShowUnder(View.transform);
		}
	}
}