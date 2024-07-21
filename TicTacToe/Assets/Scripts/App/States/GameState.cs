using App.Services;
using Commands;
using Core;

namespace App.States.Gameplay
{
	public class GameState : AppState<GameStateActivated>
	{
		private readonly IPlayerService _playerService;
		private readonly GameEngine _gameEngine;

		public GameState(IPlayerService playerService, GameEngine gameEngine)
		{
			_playerService = playerService;
			_gameEngine = gameEngine;
		}

		protected override void OnActivate()
		{
			var playerId1 = 1;
			var playerId2 = 2;
			
			// initialize game engine with player ids 
			_gameEngine.Initialize(playerId1, playerId2);

			// define who turns first and who second 
			var startPlayerId = _gameEngine.TurnOwner;
			var secondPlayerId = startPlayerId == playerId1 
				? playerId2 
				: playerId1;
			
			// initialize which symbol should be used by which player
			_playerService.InitializePlayers(
				new PlayerInfo(startPlayerId, SymbolKey.Cross), 
				new PlayerInfo(secondPlayerId, SymbolKey.Circle));
		}
	}
}