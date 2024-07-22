using System;
using App.Match;
using App.States;
using Core;

namespace Presentation.Popups
{
	public class GameResultPopupPresenter : StatelessPresenter<GameResultPopupView>, IPopup
	{
		private readonly IAppNavigatorService _appNavigator;
		private readonly IPopupService _popupService;
		private readonly IMatchService _matchService;
		private readonly GameEngine _gameEngine;

		public GameResultPopupPresenter(
			IViewProvider<GameResultPopupView> viewProvider,
			IAppNavigatorService appNavigator, 
			IPopupService popupService,
			IMatchService matchService) : base(viewProvider)
		{
			_appNavigator = appNavigator;
			_popupService = popupService;
			_matchService = matchService;
		}

		protected override void InitializeView(GameResultPopupView view)
		{
			view.name = "GameResultPopup";

			if (!_matchService.TryGetResult(out var matchResult)) {
				return;
			}
			
			var resultString = matchResult.Winner switch {
				0 => "DRAW!",
				> 0 => $"PLAYER {matchResult.Winner} WON!",
				_ => throw new ArgumentException($"Unsupported match result '{matchResult.Winner}'")
			};

			view.SetResult(resultString);
		}

		protected override void OnActivate()
		{
			View.ExitButton.onClick.AddListener(OnTapExit);
			View.RestartButton.onClick.AddListener(OnTapRestart);
		}
		
		protected override void OnDeactivate()
		{
			View.ExitButton.onClick.RemoveListener(OnTapExit);
			View.RestartButton.onClick.RemoveListener(OnTapRestart);
		}

		private void OnTapExit()
		{
			_appNavigator.GoToState<HomeState>();
			ClosePopup();
		}
		
		private void OnTapRestart()
		{
			_matchService.StartMatch(_matchService.CurrentMatchType);
			ClosePopup();
		}

		private void ClosePopup()
		{
			_popupService.Hide<GameResultPopupPresenter>();
		}
	}
}