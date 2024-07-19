using Commands;
using Core;
using UnityEngine;
using VitalRouter;

namespace Presentation
{
	[Routes]
	public partial class GameScreenPresenter : Presenter<GameScreenView, GameEngine>, IScreen
	{
		private IPresenterFactory _factory;
		
		public GameScreenPresenter(IModuleViewProvider<GameScreenView> viewProvider, GameEngine model, IPresenterFactory factory) : base(viewProvider, model)
		{
			_factory = factory;
		}

		protected override void InitializeView(GameScreenView view)
		{
			view.name = "GameScreen";
		}

		protected override void OnActivate()
		{ }

		protected override void OnDeactivate()
		{ }

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