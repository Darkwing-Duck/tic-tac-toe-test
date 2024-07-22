using System;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Match
{
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
			var delay = Random.Range(200, 1000); // bot thinking time (ms)
			await UniTask.Delay(TimeSpan.FromMilliseconds(delay), ignoreTimeScale: false);
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