using Core;
using VitalRouter;

namespace Commands
{
	public struct GameFinishedCommand : ICommand
	{
		public readonly GameResult Result;

		public GameFinishedCommand(GameResult result)
		{
			Result = result;
		}
	}
}