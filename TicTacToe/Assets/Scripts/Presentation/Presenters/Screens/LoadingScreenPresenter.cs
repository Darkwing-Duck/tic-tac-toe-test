using App.States;
using App.States.Gameplay;

namespace Presentation
{
	public class LoadingScreenPresenter : StatelessPresenter<LoadingScreenView>
	{
		private readonly IAppNavigatorService _appNavigator;
		
		public LoadingScreenPresenter(IModuleViewProvider<LoadingScreenView> viewProvider, IAppNavigatorService appNavigator) : base(viewProvider)
		{
			_appNavigator = appNavigator;
		}
		
		protected override void InitializeView(LoadingScreenView view)
		{
			view.name = "LoadingScreen";
		}

		protected override void OnActivate()
		{
			_appNavigator.GoToState<GameState>();
		}
	}
}