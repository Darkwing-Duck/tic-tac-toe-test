using VitalRouter;

namespace Commands
{
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