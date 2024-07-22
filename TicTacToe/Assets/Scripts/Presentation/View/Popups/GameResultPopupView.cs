using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Popups
{
	public class GameResultPopupView : PresentationView
	{
		[SerializeField]
		private TMP_Text _resultField;
		
		[SerializeField]
		private Button _exitButton;
		
		[SerializeField]
		private Button _restartButton;

		public Button ExitButton => _exitButton;
		public Button RestartButton => _restartButton;

		public void SetResult(string value)
		{
			_resultField.text = value;
		}
	}
}