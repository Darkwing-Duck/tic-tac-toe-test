using UnityEngine;

namespace Core
{
	public class GameResult
	{
		public readonly int Winner;
		public readonly Vector2Int[] WinSlots;

		public GameResult(int winner, Vector2Int[] winSlots)
		{
			Winner = winner;
			WinSlots = winSlots;
		}
	}
}