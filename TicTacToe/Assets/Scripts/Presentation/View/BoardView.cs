using System.Collections.Generic;
using UnityEngine;

namespace Presentation
{
	public class BoardView : PresentationView
	{
		[SerializeField]
		private List<BoardSlotView> _slots;
		
		public BoardSlotView GetSlot(int index) => _slots[index];
	}
}