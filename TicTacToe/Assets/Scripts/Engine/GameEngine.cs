using UnityEngine;

namespace TicTacToe.Engine
{
	public class GameEngine
	{
		private const int BoardSize = 3;
		
		public int PlayerOneId { get; private set; }
		public int PlayerTwoId { get; private set; }

		private readonly Board _board = new();

		public int TurnOwner { get; private set; }
		public int TurnNumber { get; private set; }

		public void Setup(int playerOneId, int playerTwoId)
		{
			PlayerOneId = playerOneId;
			PlayerTwoId = playerTwoId;

			TurnNumber = 0;
			TurnOwner = PlayerOneId;

			_board.Reset();
		}

		public void Turn(Vector2Int location)
		{
			_board.OccupySlot(location, TurnOwner);
			
			TurnNumber++;
			TurnOwner = TurnOwner == PlayerOneId
				? PlayerTwoId
				: PlayerOneId;
		}
	}
}