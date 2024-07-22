using VitalRouter;

namespace Commands
{
	/// <summary>
	/// Notifies GameScreenPresenter to prepare visual game state for the next turn
	/// </summary>
	public struct NextTurnCommand : ICommand
	{
		public readonly int TurnNumber;
		public readonly int TurnOwner;

		public NextTurnCommand(int turnNumber, int turnOwner)
		{
			TurnNumber = turnNumber;
			TurnOwner = turnOwner;
		}
	}
}