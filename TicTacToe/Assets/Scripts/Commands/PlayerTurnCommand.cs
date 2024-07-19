using UnityEngine;
using VitalRouter;

namespace Commands
{
	public readonly struct PlayerTurnCommand : ICommand
	{
		public readonly Vector2Int Position;

		public PlayerTurnCommand(Vector2Int position)
		{
			Position = position;
		}
	}
}