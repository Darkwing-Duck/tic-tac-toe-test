using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
	public class HomeScreenView : PresentationView
	{
		[SerializeField]
		private Button _playerVsPlayerButton;
		
		[SerializeField]
		private Button _playerVsBotButton;
		
		[SerializeField]
		private Button _botVsBotButton;

		public Button PlayerVsPlayerButton => _playerVsPlayerButton;
		public Button PlayerVsBotButton => _playerVsBotButton;
		public Button BotVsBotButton => _botVsBotButton;
	}
}