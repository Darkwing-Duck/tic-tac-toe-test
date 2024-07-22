using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Board helpful methods
	/// </summary>
	public static class BoardUtils
	{
		/// <summary>
		/// Converts board position to linear collection index
		/// </summary>
		public static int PositionToIndex(Vector2Int position, int boardWidth) => 
			position.y * boardWidth + position.x;

		/// <summary>
		/// Converts linear collection index to board position
		/// </summary>
		public static Vector2Int IndexToPosition(int index, int boardWidth)
		{
			var x = index % boardWidth;
			var y = (index - x) / boardWidth;
			return new Vector2Int(x, y);
		}
	}
}