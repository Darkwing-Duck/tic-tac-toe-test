using Core;
using VitalRouter;

namespace Commands
{
	/// <summary>
	/// Notifies GameScreenPresenter that game was finished
	/// </summary>
	public struct GameFinishedCommand : ICommand
	{
		public readonly GameResult Result;

		public GameFinishedCommand(GameResult result)
		{
			Result = result;
		}
	}
}