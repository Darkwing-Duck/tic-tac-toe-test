using UnityEngine;
using VitalRouter;

namespace Commands
{
	public readonly struct PlayerTurnCommand : ICommand
	{
		public readonly int Owner;
		public readonly Vector2Int Position;

		public PlayerTurnCommand(int owner, Vector2Int position)
		{
			Owner = owner;
			Position = position;
		}
	}
}