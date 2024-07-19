using UnityEngine;

namespace Presentation
{
	public class GameScreenView : PresentationView
	{
		[SerializeField]
		private BoardView _boardView;
		public BoardView Board => _boardView;
	}
}