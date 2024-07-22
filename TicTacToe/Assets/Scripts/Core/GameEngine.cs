using System;
using UnityEngine;

namespace Core
{
	/// <summary>
	/// Core business logic of the game.
	/// </summary>
	public class GameEngine : IEngine
	{
		private readonly IGameResultCalculator _resultCalculator;
		
		public int PlayerOneId { get; private set; }
		public int PlayerTwoId { get; private set; }

		public int TurnOwner { get; private set; }
		public int TurnNumber { get; private set; }
		public Board Board { get; private set; } = new();

		private GameResult _gameResult;

		public GameEngine(IGameResultCalculator resultCalculator)
		{
			_resultCalculator = resultCalculator;
		}

		public void Initialize(int playerOneId, int playerTwoId)
		{
			PlayerOneId = playerOneId;
			PlayerTwoId = playerTwoId;

			TurnNumber = 1;
			TurnOwner = PlayerOneId;
			_gameResult = null;

			Board.Reset();
		}

		public void Turn(int playerId, Vector2Int location)
		{
			if (_gameResult is not null) {
				throw new Exception("Game is already finished");
			}
			
			if (playerId != TurnOwner) {
				throw new ArgumentException($"Next turn have to be done by player '{TurnOwner}' and not by player '{playerId}'");
			}
			
			if (!Board.OccupySlot(location, TurnOwner)) {
				throw new Exception($"The board slot at position '{location}' is not free");
			}
			
			TurnNumber++;
			TurnOwner = TurnOwner == PlayerOneId
				? PlayerTwoId
				: PlayerOneId;
			
			_gameResult = _resultCalculator.Calculate(location, this);
		}

		public bool TryGetGameResult(out GameResult result)
		{
			result = _gameResult;
			return _gameResult is not null;
		}
	}
}