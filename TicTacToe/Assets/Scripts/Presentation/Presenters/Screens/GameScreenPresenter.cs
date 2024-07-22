using App.Match;
using Commands;
using Presentation.Popups;
using UnityEngine;
using Utils;
using VitalRouter;

namespace Presentation
{
	[Routes]
	public partial class GameScreenPresenter : StatelessPresenter<GameScreenView>
	{
		private readonly IPresenterFactory _factory;
		private readonly IMatchService _matchService;
		private readonly ICommandSubscribable _router;
		private readonly IPopupService _popupService;
		
		public GameScreenPresenter(
			IViewProvider<GameScreenView> viewProvider, 
			IPresenterFactory factory,
			ICommandSubscribable router,
			IMatchService matchService,
			IPopupService popupService) : base(viewProvider)
		{
			_factory = factory;
			_router = router;
			_matchService = matchService;
			_popupService = popupService;
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

		public void On(PlayerMadeTurnCommand cmd)
		{
			AddBoardElementAt(cmd.Owner, cmd.Position);
		}
		
		public void On(NextTurnCommand cmd)
		{
			View.HUD.SetTurnNumber(cmd.TurnNumber);
			View.HUD.SetTurnOwner(cmd.TurnOwner);
		}
		
		public void On(GameFinishedCommand _)
		{
			_popupService.Show<GameResultPopupPresenter>();
		}
		
		private void AddBoardElementAt(int turnOwner, Vector2Int position)
		{
			var slotIndex = BoardUtils.PositionToIndex(position, 3);
			var slotView = View.Board.GetSlot(slotIndex);
			var provider = new ResourcesViewProvider<BoardElementView>();
			var view = provider.Get(slotView.transform);
			var playerSymbol = _matchService.GetPlayerSymbol(turnOwner);
			
			view.SetIcon(playerSymbol.ToString());
		}
	}
}