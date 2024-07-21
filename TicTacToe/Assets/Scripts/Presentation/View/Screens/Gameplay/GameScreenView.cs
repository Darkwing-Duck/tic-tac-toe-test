using UnityEngine;

namespace Presentation
{
	public class GameScreenView : PresentationView
	{
		[SerializeField]
		private BoardView _boardView;
		
		[SerializeField]
		private GameHUDView _hud;
		
		public BoardView Board => _boardView;
		public GameHUDView HUD => _hud;
	}
}