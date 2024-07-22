using App.Match;

namespace Presentation
{
	public class HomeScreenPresenter : StatelessPresenter<HomeScreenView>
	{
		private readonly IMatchService _matchService;
		
		public HomeScreenPresenter(IViewProvider<HomeScreenView> viewProvider, IMatchService matchService) : base(viewProvider)
		{
			_matchService = matchService;
		}
		
		protected override void InitializeView(HomeScreenView view)
		{
			view.name = "HomeScreen";
		}

		protected override void OnActivate()
		{
			View.PlayerVsPlayerButton.onClick.AddListener(OnTapPlayerVsPlayerButton);
			View.PlayerVsBotButton.onClick.AddListener(OnTapPlayerVsBotButton);
			View.BotVsBotButton.onClick.AddListener(OnTapBotVsBotButton);
		}

		protected override void OnDeactivate()
		{
			View.PlayerVsPlayerButton.onClick.RemoveListener(OnTapPlayerVsPlayerButton);
			View.PlayerVsBotButton.onClick.RemoveListener(OnTapPlayerVsBotButton);
			View.BotVsBotButton.onClick.RemoveListener(OnTapBotVsBotButton);
		}
		
		private void OnTapPlayerVsPlayerButton() => _matchService.StartMatch(MatchType.PlayerVsPlayer);
		private void OnTapPlayerVsBotButton() => _matchService.StartMatch(MatchType.PlayerVsBot);
		private void OnTapBotVsBotButton() => _matchService.StartMatch(MatchType.BotVsBot);
	}
}