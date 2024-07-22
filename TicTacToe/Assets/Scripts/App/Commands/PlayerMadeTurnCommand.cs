using UnityEngine;
using VitalRouter;

namespace Commands
{
	
	/// <summary>
	/// Notifies GameScreenPresenter that a turn have been made to sync visual state
	/// </summary>
	public readonly struct PlayerMadeTurnCommand : ICommand
	{
		public readonly int Owner;
		public readonly Vector2Int Position;

		public PlayerMadeTurnCommand(int owner, Vector2Int position)
		{
			Owner = owner;
			Position = position;
		}
	}
}