using Core;
using UnityEngine;

namespace App.Match
{
	
	/// <summary>
	/// Describes common player input interface 
	/// </summary>
	public interface IMatchPlayerInput
	{
		public int PlayerId { get; }
		public SymbolKey SymbolKey { get; }

		void Activate();
		void Deactivate();
	}
	
	/// <summary>
	/// Base match player input logic
	/// </summary>
	public abstract class MatchPlayerInput : IMatchPlayerInput
	{
		public int PlayerId { get; }
		public SymbolKey SymbolKey { get; }

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