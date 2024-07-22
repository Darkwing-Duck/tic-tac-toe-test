using App.Common;
using App.Match;
using Commands;

namespace App.States.Gameplay
{
	/// <summary>
	/// Describes a Game application state in Application logic layer
	/// </summary>
	public class GameState : AppState<GameStateActivated>, IUpdatable
	{
		private readonly IMatchService _matchService;

		public GameState(IMatchService matchService)
		{
			_matchService = matchService;
		}

		public void Update()
		{
			_matchService.Update();
		}
	}
}