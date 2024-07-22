using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core
{
	/// <summary>
	/// Board engine state
	/// </summary>
	public class Board
	{
		public readonly int Size = 3;
		private readonly int[] _slots;

		public Board()
		{
			_slots = new int[Size * Size];
		}

		/// <summary>
		/// Indicates whether a slot in specified location is free or not
		/// </summary>
		public bool IsSlotFree(Vector2Int location)
		{
			var slotIndex = BoardUtils.PositionToIndex(location, Size);
			return IsSlotFree(slotIndex);
		}
		
		/// <summary>
		/// Returns value in specified slot position
		/// </summary>
		public int GetValueAt(Vector2Int position)
		{
			var slotIndex = BoardUtils.PositionToIndex(position, Size);
			return _slots[slotIndex];
		}
		
		/// <summary>
		/// Returns all free cell in the board
		/// </summary>
		public IReadOnlyList<Vector2Int> GetFreeCells()
		{
			var result = new List<Vector2Int>();
			
			for (var i = 0; i < _slots.Length; i++) {
				if (IsSlotFree(i)) {
					result.Add(BoardUtils.IndexToPosition(i, Size));
				}
			}

			return result;
		}
		
		/// <summary>
		/// Occupies a slot in specified location with a passed value
		/// </summary>
		internal bool OccupySlot(Vector2Int location, int value)
		{
			if (!IsSlotFree(location)) {
				return false;
			}
			
			var slotIndex = BoardUtils.PositionToIndex(location, Size);
			_slots[slotIndex] = value;
			return true;
		}

		/// <summary>
		/// Resets the board
		/// </summary>
		internal void Reset()
		{
			for (var i = 0; i < _slots.Length; i++) {
				_slots[i] = 0;
			}
		}
		
		private bool IsSlotFree(int index) => _slots[index] == 0;
	}
}