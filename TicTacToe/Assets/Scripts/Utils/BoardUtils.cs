using UnityEngine;

namespace Utils
{
	public static class BoardUtils
	{
		public static int PositionToIndex(Vector2Int position, int boardWidth) => 
			position.y * boardWidth + position.x;
	}
}