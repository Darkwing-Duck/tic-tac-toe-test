using Core;
using UnityEngine;

namespace App.Match
{
	public interface IMatchPlayerInput
	{
		void Activate();
		void Deactivate();
	}
	
	public abstract class MatchPlayerInput : IMatchPlayerInput
	{
		public readonly int PlayerId;
		public readonly SymbolKey SymbolKey;

		protected readonly IEngineReadOnly _matchData;
		private readonly IMatchPlayerOutput _output;

		protected MatchPlayerInput(int playerId, SymbolKey symbolKey, IEngineReadOnly matchData, IMatchPlayerOutput output)
		{
			PlayerId = playerId;
			SymbolKey = symbolKey;
			_matchData = matchData;
			_output = output;
		}

		public void Activate() => OnActivate();
		public void Deactivate() => OnDeactivate();
		
		protected virtual void OnActivate()
		{ }

		protected virtual void OnDeactivate()
		{ }

		/// <summary>
		/// Should be called by subclass to send the player's decision of the turn 
		/// </summary>
		protected void MakeTurnAt(Vector2Int position)
		{
			_output.MakeTurn(PlayerId, position);
		}
	}
}