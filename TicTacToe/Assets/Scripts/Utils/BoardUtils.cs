using UnityEngine;

namespace Utils
{
	public static class BoardUtils
	{
		public static int PositionToIndex(Vector2Int position, int boardWidth) => 
			position.y * boardWidth + position.x;

		public static Vector2Int IndexToPosition(int index, int boardWidth)
		{
			var x = index % boardWidth;
			var y = (index - x) / boardWidth;
			return new Vector2Int(x, y);
		}
	}
}