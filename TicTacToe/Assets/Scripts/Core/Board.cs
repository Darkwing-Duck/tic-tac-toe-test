using UnityEngine;

namespace Core
{
	public class Board
	{
		private const int BoardSize = 3;
		private readonly int[] _slots = new int[BoardSize * BoardSize];

		public bool OccupySlot(Vector2Int location, int value)
		{
			return true;
		}
		
		public bool IsSlotFree(Vector2Int location)
		{
			return true;
		}

		public void Reset()
		{
			for (var i = 0; i < _slots.Length; i++) {
				_slots[i] = 0;
			}
		}
	}
}