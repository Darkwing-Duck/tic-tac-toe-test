using App.Common;
using UnityEngine;

namespace App.Match
{
	public enum SymbolKey
	{
		Cross,
		Circle
	}

	/// <summary>
	/// Result of the match.
	/// MatchService uses a custom match result to pass it to presentation layer
	/// </summary>
	public class MatchResult
	{
		public readonly int Winner;
		public readonly Vector2Int[] WinSlots;

		public MatchResult(int winner, Vector2Int[] winSlots)
		{
			Winner = winner;
			WinSlots = winSlots;
		}
	}
	
	/// <summary>
	/// Describes common match service interface
	/// </summary>
	public interface IMatchService : IUpdatable
	{
		MatchType CurrentMatchType { get; }
		void StartMatch(MatchType type);
		void ActivateInput();
		SymbolKey GetPlayerSymbol(int playerId);
		bool TryGetResult(out MatchResult result);
	}
}