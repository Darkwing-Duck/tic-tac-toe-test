using System;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Match
{
	/// <summary>
	/// Describes simple AI player input logic
	/// Generally, it chooses a random free cell on the board 
	/// </summary>
	public class BotPlayerInput : MatchPlayerInput
	{
		public BotPlayerInput(
			int playerId, 
			SymbolKey symbolKey, 
			IEngineReadOnly matchData, 
			IMatchPlayerOutput output) : base(playerId, symbolKey, matchData, output)
		{ }

		protected override async void OnActivate()
		{
			// bot thinking time (ms)
			var delay = Random.Range(200, 1000); 
			await UniTask.Delay(TimeSpan.FromMilliseconds(delay), ignoreTimeScale: false);
			
			// make a turn
			MakeTurnAt(MakeTurnDecision());
		}

		private Vector2Int MakeTurnDecision()
		{
			var freeCells = _matchData.Board.GetFreeCells();
			var randomIndex = Random.Range(0, freeCells.Count);

			return freeCells[randomIndex];
		}
	}
}