using UnityEngine;

namespace Core
{
	/// <summary>
	/// Computed game result.
	/// </summary>
	public class GameResult
	{
		/// <summary>
		/// Indicates a winner.
		///  - if 0, it means DRAW
		///  - if >0, it means that winner is a playerId of the winner
		/// </summary>
		public readonly int Winner;
		
		/// <summary>
		/// Slot positions that led to the victory
		/// </summary>
		public readonly Vector2Int[] WinSlots;

		public GameResult(int winner, Vector2Int[] winSlots)
		{
			Winner = winner;
			WinSlots = winSlots;
		}
	}
}