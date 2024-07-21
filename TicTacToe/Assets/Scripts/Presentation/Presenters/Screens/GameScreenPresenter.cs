using App.Services;
using App.States;
using App.States.Gameplay;
using Commands;
using Core;
using UnityEngine;
using VitalRouter;

namespace Presentation
{
	[Routes]
	public partial class GameScreenPresenter : StatelessPresenter<GameScreenView>
	{
		private readonly IPresenterFactory _factory;
		private readonly IPlayerService _playerService;
		private readonly ICommandSubscribable _router;
		private readonly IAppNavigatorService _navigator;
		
		public GameScreenPresenter(
			IModuleViewProvider<GameScreenView> viewProvider, 
			IPresenterFactory factory,
			ICommandSubscribable router,
			IPlayerService playerService,
			IAppNavigatorService navigator) : base(viewProvider)
		{
			_factory = factory;
			_router = router;
			_playerService = playerService;
			_navigator = navigator;
		}

		protected override void InitializeView(GameScreenView view)
		{
			view.name = "GameScreen";
		}

		protected override void OnActivate()
		{
			// subscribe presenter to get notifications from the router
			MapTo(_router);
		}

		protected override void OnDeactivate()
		{
			UnmapRoutes();
		}

		public void On(PlayerTurnCommand cmd)
		{
			AddBoardElementAt(cmd.Owner, cmd.Position);
		}
		
		public void On(NextTurnCommand cmd)
		{
			View.HUD.SetTurnNumber(cmd.TurnNumber);
			View.HUD.SetTurnOwner(cmd.TurnOwner);
		}
		
		public void On(GameFinishedCommand cmd)
		{
			_navigator.GoToState<GameState>();
		}
		
		private void AddBoardElementAt(int turnOwner, Vector2Int position)
		{
			var slotIndex = BoardUtils.PositionToIndex(position, 3);
			var slotView = View.Board.GetSlot(slotIndex);
			var provider = new ResourcesViewProvider<BoardElementView>();
			var view = provider.Get(slotView.transform);
			var ownerPlayerInfo = _playerService.GetPlayer(turnOwner);
			
			view.SetIcon(ownerPlayerInfo.SymbolKey.ToString());
		}
	}
}