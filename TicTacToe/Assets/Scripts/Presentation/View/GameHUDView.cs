using TMPro;
using UnityEngine;

namespace Presentation
{
	public class GameHUDView : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _turnNumberField;
		
		[SerializeField]
		private TMP_Text _turnOwnerField;

		public void SetTurnNumber(int value)
		{
			_turnNumberField.text = value.ToString();
		}
		
		public void SetTurnOwner(int playerId)
		{
			_turnOwnerField.text = $"Player {playerId}";
		}
	}
}