using System;
using System.Linq;
using App.Common;
using App.States;
using App.States.Gameplay;
using Commands;
using Core;
using UnityEngine;
using VitalRouter;

namespace App.Match
{
	public enum SymbolKey
	{
		Cross,
		Circle
	}
	
	public interface IMatchService : IUpdatable
	{
		void StartMatch(MatchType type);
		void ActivateInput();
		SymbolKey GetPlayerSymbol(int playerId);
	}
	
	public class MatchService : IMatchService, IMatchPlayerOutput
	{
		private readonly MatchPlayerInput[] _inputs = new MatchPlayerInput[2];
		private readonly LockedPlayerInput _lockedPlayerInput = new();
		
		private readonly GameEngine _gameEngine;
		private readonly ICommandPublisher _commandPublisher;
		private readonly IAppNavigatorService _appNavigator;

		private MatchPlayerInput _activeInput;
		
		public MatchService(GameEngine gameEngine, IAppNavigatorService appNavigator, ICommandPublisher commandPublisher)
		{
			_gameEngine = gameEngine;
			_commandPublisher = commandPublisher;
			_appNavigator = appNavigator;
			_activeInput = _lockedPlayerInput;
		}
		
		public void StartMatch(MatchType type)
		{
			var playerId1 = 1;
			var playerId2 = 2;
			
			_appNavigator.GoToState<GameState>();
			
			switch (type) {
				case MatchType.PlayerVsPlayer:
					StartMatchInternal<LocalPlayerInput, LocalPlayerInput>(playerId1, playerId2);
					break;
				case MatchType.PlayerVsBot:
					StartMatchInternal<LocalPlayerInput, BotPlayerInput>(playerId1, playerId2);
					break;
				case MatchType.BotVsBot:
					StartMatchInternal<BotPlayerInput, BotPlayerInput>(playerId1, playerId2);
					break;
				default:
					throw new ArgumentException($"Unsupported match type '{type}'");
			}

			NotifyNextTurn();
		}

		public void ActivateInput()
		{
			_activeInput = GetPlayerInputFor(_gameEngine.TurnOwner);
		}

		public SymbolKey GetPlayerSymbol(int playerId)
		{
			return GetPlayerInputFor(playerId).SymbolKey;
		}

		public void MakeTurn(int playerId, Vector2Int position)
		{
			_activeInput = _lockedPlayerInput;
			_commandPublisher.Enqueue(new PlayerTurnCommand(playerId, position));

			if (_gameEngine.TryGetGameResult(out var result)) {
				_commandPublisher.Enqueue(new GameFinishedCommand(result));
			} else {
				NotifyNextTurn();
			}
		}

		private void NotifyNextTurn()
		{
			_commandPublisher.Enqueue(new NextTurnCommand(_gameEngine.TurnNumber, _gameEngine.TurnOwner));
			_commandPublisher.Enqueue(new ActivatePlayerInputCommand());
		}

		public void Update()
		{
			if (_activeInput is IUpdatable casted) {
				casted.Update();
			}
		}

		private void StartMatchInternal<TInput1, TInput2>(int playerId1, int playerId2)
			where TInput1 : MatchPlayerInput
			where TInput2 : MatchPlayerInput
		{
			// initialize game engine with player ids 
			_gameEngine.Initialize(playerId1, playerId2);
			
			InitializeInputs<TInput1, TInput2>(playerId1, playerId2);
		}

		private void InitializeInputs<TInput1, TInput2>(int playerId1, int playerId2)
			where TInput1 : MatchPlayerInput
			where TInput2 : MatchPlayerInput
		{
			// define who turns first and who second 
			var startPlayerId = _gameEngine.TurnOwner;
			var secondPlayerId = startPlayerId == playerId1 
				? playerId2 
				: playerId1;
			
			_inputs[0] = CreatePlayerInput<TInput1>(startPlayerId, SymbolKey.Cross);
			_inputs[1] = CreatePlayerInput<TInput2>(secondPlayerId, SymbolKey.Circle);
		}

		private MatchPlayerInput CreatePlayerInput<T>(int playerId, SymbolKey symbolKey) where T : MatchPlayerInput
		{
			return (MatchPlayerInput)Activator.CreateInstance(typeof(T), new object[] { playerId, symbolKey, this });
		}

		private MatchPlayerInput GetPlayerInputFor(int playerId)
		{
			return _inputs.First(i => i.PlayerId == playerId);
		}
	}
}