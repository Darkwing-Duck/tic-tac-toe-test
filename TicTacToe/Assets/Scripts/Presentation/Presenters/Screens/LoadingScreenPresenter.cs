namespace Presentation
{
	public class LoadingScreenPresenter : StatelessPresenter<LoadingScreenView>, IScreen
	{
		private readonly IAppNavigator _navigator;
		
		public LoadingScreenPresenter(IModuleViewProvider<LoadingScreenView> viewProvider, IAppNavigator navigator) : base(viewProvider)
		{
			_navigator = navigator;
		}
		
		protected override void InitializeView(LoadingScreenView view)
		{
			view.name = "LoadingScreen";
		}

		protected override void OnActivate()
		{
			_navigator.GoTo<GameScreenPresenter>();
		}
	}
}