using System;
using UnityEngine;

namespace Core
{
	public interface IGameResultCalculator
	{
		GameResult Calculate(Vector2Int fromPosition, ICalculatorDataProvider dataProvider);
	}

	public interface ICalculatorDataProvider
	{
		Board Board { get; }
		int TurnNumber { get; }
	}
	
	public class GameResultCalculator : IGameResultCalculator
	{
		/// <summary>
		/// Calculates the winner if the game finished else returns '-1'
		/// </summary>
		/// <returns>
		/// * 0 - Draw<br />
		/// * more than 0 - playerId of winner<br />
		/// * less than 0 - game is still not finished
		/// </returns>
		public GameResult Calculate(Vector2Int fromPosition, ICalculatorDataProvider dataProvider)
		{
			var board = dataProvider.Board;
			var winSlots = new Vector2Int[3];
			var targetValue = board.GetValueAt(fromPosition);
			var nextPos = new Vector2Int();
			
			// check column
			for (var i = 0; i < board.Size; i++) {
				nextPos.Set(fromPosition.x, i);
				var nextValue = board.GetValueAt(nextPos);
				
				if (nextValue != targetValue) {
					break;
				}
				
				winSlots[i] = nextPos;
				
				if (i == board.Size - 1) {
					return new GameResult(targetValue, winSlots);
				}
			}
        
			// check row
			for (var i = 0; i < board.Size; i++) {
				nextPos.Set(i, fromPosition.y);
				var nextValue = board.GetValueAt(nextPos);
				
				if (nextValue != targetValue) {
					break;
				}
				
				winSlots[i] = nextPos;
				
				if (i == board.Size - 1) {
					return new GameResult(targetValue, winSlots);
				}
			}
        
			// check diagonal
			if (fromPosition.x == fromPosition.y) {
				for (var i = 0; i < board.Size; i++) {
					nextPos.Set(i, i);
					var nextValue = board.GetValueAt(nextPos);
				
					if (nextValue != targetValue) {
						break;
					}
				
					winSlots[i] = nextPos;
				
					if (i == board.Size - 1) {
						return new GameResult(targetValue, winSlots);
					}
				}
			}
            
			// check reversed diagonal
			if (fromPosition.x + fromPosition.y == board.Size - 1) {
				for(var i = 0; i < board.Size; i++) {
					nextPos.Set(i, board.Size - i - 1);
					var nextValue = board.GetValueAt(nextPos);
				
					if (nextValue != targetValue) {
						break;
					}
				
					winSlots[i] = nextPos;
				
					if (i == board.Size - 1) {
						return new GameResult(targetValue, winSlots);
					}
				}
			}

			//check draw
			// if the number of turn is 9, it means that we already made 8 moves
			// so, one free slot left but it doesn't matter, there are no winners
			if (dataProvider.TurnNumber >= board.Size * board.Size){
				return new GameResult(0, Array.Empty<Vector2Int>());
			}
			
			return null;
		}
	}
}