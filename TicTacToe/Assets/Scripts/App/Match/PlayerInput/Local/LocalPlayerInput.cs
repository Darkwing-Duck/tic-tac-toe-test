using System.Collections.Generic;
using App.Common;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Match
{
	/// <summary>
	/// Describes local player input strategy.
	/// The logic is separated from the MonoBehaviour and using UI raycast to find a clink on board slot
	/// </summary>
	public class LocalPlayerInput : MatchPlayerInput, IUpdatable
	{
		private Vector2 _positionCache = Vector2.zero;
		private readonly List<RaycastResult> _raycastResults = new();

		public LocalPlayerInput(
			int playerId, 
			SymbolKey symbolKey,
			IEngineReadOnly matchData,
			IMatchPlayerOutput output) : base(playerId, symbolKey, matchData, output)
		{
		}

		public void Update()
		{
			if (!Input.GetMouseButtonUp(0)) {
				return;
			}
			
			if (!TryGetClickedSlotPosition(out var slotPosition)) {
				return;
			}
			
			// Notify outer world about decision made by this player
			MakeTurnAt(slotPosition);
		}
		
		private bool TryGetClickedSlotPosition(out Vector2Int result)
		{
			var mousePosition = Input.mousePosition;
			_positionCache.Set(mousePosition.x, mousePosition.y);
				
			var eventData = new PointerEventData(EventSystem.current) { position = _positionCache };
			EventSystem.current.RaycastAll(eventData, _raycastResults);
		
			if (_raycastResults.Count <= 0) {
				result = default;
				return false;
			}

			var firstUiElement = _raycastResults[0];

			if (!firstUiElement.gameObject.TryGetComponent<IPlayerInputSource>(out var slotView)) {
				result = default;
				return false;
			}
			
			result = slotView.SlotPosition;
			return true;
		}
	}
}