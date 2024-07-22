using App.Match;
using UnityEngine;

namespace Presentation
{
	public class BoardSlotView : MonoBehaviour, IPlayerInputSource
	{
		[SerializeField]
		private Vector2Int _slotPosition;
		
		[SerializeField]
		private Transform _elementContainer;
		
		public Vector2Int SlotPosition => _slotPosition;
		public Transform ElementContainer => _elementContainer;
	}
}