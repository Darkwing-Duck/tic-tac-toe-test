using UnityEngine;

namespace Core
{
	public class Board
	{
		public readonly int Size = 3;
		private readonly int[] _slots;

		public Board()
		{
			_slots = new int[Size * Size];
		}

		public bool IsSlotFree(Vector2Int location)
		{
			var slotIndex = BoardUtils.PositionToIndex(location, Size);
			return IsSlotFree(slotIndex);
		}
		
		private bool IsSlotFree(int index) => _slots[index] == 0;

		public int GetValueAt(Vector2Int position)
		{
			var slotIndex = BoardUtils.PositionToIndex(position, Size);
			return _slots[slotIndex];
		}
		
		internal bool OccupySlot(Vector2Int location, int value)
		{
			if (!IsSlotFree(location)) {
				return false;
			}
			
			var slotIndex = BoardUtils.PositionToIndex(location, Size);
			_slots[slotIndex] = value;
			return true;
		}

		internal void Reset()
		{
			for (var i = 0; i < _slots.Length; i++) {
				_slots[i] = 0;
			}
		}
	}
}