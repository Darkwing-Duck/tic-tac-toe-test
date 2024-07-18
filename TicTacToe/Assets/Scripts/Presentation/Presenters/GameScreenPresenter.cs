using Presentation.Core;
using Presentation.View;
using TicTacToe.Engine;

namespace Presentation.Presenters
{
	public class GameScreenPresenter : Presenter<GameScreenView, GameEngine>
	{
		public GameScreenPresenter(IModuleViewProvider<GameScreenView> viewProvider, GameEngine model) : base(viewProvider, model)
		{
		}
	}
}