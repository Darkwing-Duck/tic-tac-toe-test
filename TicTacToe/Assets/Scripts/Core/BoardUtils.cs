using UnityEngine;

namespace Core
{
	public static class BoardUtils
	{
		public static int PositionToIndex(Vector2Int position, int boardWidth) => 
			position.y * boardWidth + position.x;
	}
}