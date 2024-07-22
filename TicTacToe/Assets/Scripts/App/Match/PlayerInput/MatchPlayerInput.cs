using UnityEngine;

namespace App.Match
{
	public interface IMatchPlayerInput
	{
		
	}
	
	public abstract class MatchPlayerInput
	{
		public readonly int PlayerId;
		public readonly SymbolKey SymbolKey;
		protected readonly IMatchPlayerOutput _output;

		protected MatchPlayerInput(int playerId, SymbolKey symbolKey, IMatchPlayerOutput output)
		{
			PlayerId = playerId;
			SymbolKey = symbolKey;
			_output = output;
		}

		/// <summary>
		/// Should be called by subclass to send the player's decision of the turn 
		/// </summary>
		protected void MakeTurnAt(Vector2Int position)
		{
			_output.MakeTurn(PlayerId, position);
		}
	}
}