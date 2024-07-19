using System.Collections.Generic;
using Commands;
using Presentation;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;
using VitalRouter;

namespace Input
{
	public interface IPlayerInput
	{
		
	}
	
	public class MousePlayerInput : IPlayerInput, ITickable
	{
		private Vector2 _positionCache = Vector2.zero;
		private readonly List<RaycastResult> _raycastResults = new();
		private ICommandPublisher _commandPublisher;

		public MousePlayerInput(ICommandPublisher commandPublisher)
		{
			_commandPublisher = commandPublisher;
		}

		public void Tick()
		{
			if (!UnityEngine.Input.GetMouseButtonUp(0)) {
				return;
			}
			
			if (!TryGetClickedSlotPosition(out var slotPosition)) {
				return;
			}
			
			_commandPublisher.Enqueue(new PlayerTurnCommand(slotPosition));
		}

		private bool TryGetClickedSlotPosition(out Vector2Int result)
		{
			var mousePosition = UnityEngine.Input.mousePosition;
			_positionCache.Set(mousePosition.x, mousePosition.y);
				
			var eventData = new PointerEventData(EventSystem.current) { position = _positionCache };
			EventSystem.current.RaycastAll(eventData, _raycastResults);
		
			if (_raycastResults.Count <= 0) {
				result = default;
				return false;
			}

			var firstUiElement = _raycastResults[0];

			if (!firstUiElement.gameObject.TryGetComponent<BoardSlotView>(out var slotView)) {
				result = default;
				return false;
			}
			
			result = slotView.SlotPosition;
			return true;
		}
	}
}