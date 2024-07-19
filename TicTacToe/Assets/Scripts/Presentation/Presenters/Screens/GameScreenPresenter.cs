using Commands;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace Presentation
{
	[Routes]
	public partial class GameScreenPresenter : StatelessPresenter<GameScreenView>, IScreen
	{
		private IPresenterFactory _factory;
		private IGameplayInstaller _installer;
		private LifetimeScope _gameplayeScope;
		
		public GameScreenPresenter(
			IModuleViewProvider<GameScreenView> viewProvider, 
			IPresenterFactory factory,
			IGameplayInstaller installer) : base(viewProvider)
		{
			_factory = factory;
			_installer = installer;
		}

		protected override void InitializeView(GameScreenView view)
		{
			view.name = "GameScreen";
		}

		protected override void OnActivate()
		{
			// install gameplay dependencies
			var parentScope = View.transform.GetComponentInParent<LifetimeScope>();
			_gameplayeScope = parentScope.CreateChild(_installer);
			_gameplayeScope.transform.SetParent(View.transform);
			_gameplayeScope.name = "Gameplay DI Scope";

			// subscribe presenter to get notifications from the router
			var router = _gameplayeScope.Container.Resolve<Router>();
			MapTo(router);
		}

		protected override void OnDeactivate()
		{
			// dispose gameplay dependencies scope
			_gameplayeScope.Dispose();
		}

		public void On(PlayerTurnCommand cmd)
		{
			AddBoardElementAt(cmd.Position);
		}
		
		public void AddBoardElementAt(Vector2Int position)
		{
			Debug.Log($"Player has tapped at {position}");
			var slotIndex = position.y * 3 + position.x;
			var slotView = View.Board.GetSlot(slotIndex);
			var provider = new ResourcesViewProvider<BoardElementView>();
			provider.Get(slotView.transform);
		}
	}
}