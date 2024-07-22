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
	public class MatchService : IMatchService, IMatchPlayerOutput
	{
		private readonly IMatchPlayerInput[] _inputs = new IMatchPlayerInput[2];
		
		/// <summary>
		/// Mock player input used to lock any player input
		/// </summary>
		private readonly LockedPlayerInput _lockedPlayerInput = new();
		
		private readonly IEngine _gameEngine;
		private readonly ICommandPublisher _commandPublisher;
		private readonly IAppNavigatorService _appNavigator;

		/// <summary>
		/// Currently active input.
		/// If we are not waiting for any input then it will be LockedPlayerInput
		/// </summary>
		private IMatchPlayerInput _activeInput;

		public MatchType CurrentMatchType { get; private set; }

		public MatchService(IEngine gameEngine, IAppNavigatorService appNavigator, ICommandPublisher commandPublisher)
		{
			_gameEngine = gameEngine;
			_commandPublisher = commandPublisher;
			_appNavigator = appNavigator;
			_activeInput = _lockedPlayerInput;
		}
		
		/// <summary>
		/// Starting a match by specified type
		/// </summary>
		public void StartMatch(MatchType type)
		{
			var playerId1 = 1;
			var playerId2 = 2;

			CurrentMatchType = type;
			
			_appNavigator.GoToState<GameState>();
			
			// initialize game engine with player ids 
			_gameEngine.Initialize(playerId1, playerId2);
			
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

		/// <summary>
		/// Activates actual input based on engine state
		/// </summary>
		public void ActivateInput()
		{
			_activeInput = GetPlayerInputFor(_gameEngine.TurnOwner);
			_activeInput.Activate();
		}

		/// <summary>
		/// Returns a symbol for specified player
		/// </summary>
		public SymbolKey GetPlayerSymbol(int playerId)
		{
			return GetPlayerInputFor(playerId).SymbolKey;
		}

		/// <summary>
		/// Core app logic for making a turn.
		/// Makes a turn for specified player in specified position
		/// </summary>
		public void MakeTurn(int playerId, Vector2Int position)
		{
			if (!_gameEngine.Board.IsSlotFree(position)) {
				// TODO: Notify presentation layer about wrong choice
				return;
			}

			DeactivateInput();
			
			// change engine state
			_gameEngine.Turn(playerId, position);
			
			// notify outer world that the turn have been made 
			_commandPublisher.Enqueue(new PlayerMadeTurnCommand(playerId, position));

			// if engine already has a result we send a game finish notfication
			// else notifying about next turn
			if (_gameEngine.TryGetGameResult(out var result)) {
				_commandPublisher.Enqueue(new GameFinishedCommand(result));
			} else {
				NotifyNextTurn();
			}
		}

		/// <summary>
		/// Provides access to computed result if there is
		/// </summary>
		public bool TryGetResult(out MatchResult result)
		{
			if (!_gameEngine.TryGetGameResult(out var gameResult)) {
				result = default;
				return false;
			}

			result = new MatchResult(gameResult.Winner, gameResult.WinSlots);
			return true;
		}
		
		/// <summary>
		/// Updates active player input if it's updatable
		/// </summary>
		public void Update()
		{
			if (_activeInput is IUpdatable casted) {
				casted.Update();
			}
		}

		private void DeactivateInput()
		{
			_activeInput.Deactivate();
			_activeInput = _lockedPlayerInput;
		}

		private void NotifyNextTurn()
		{
			_commandPublisher.Enqueue(new NextTurnCommand(_gameEngine.TurnNumber, _gameEngine.TurnOwner));
			_commandPublisher.Enqueue(new ActivatePlayerInputCommand());
		}

		private void StartMatchInternal<TInput1, TInput2>(int playerId1, int playerId2)
			where TInput1 : IMatchPlayerInput
			where TInput2 : IMatchPlayerInput
		{
			InitializeInputs<TInput1, TInput2>(playerId1, playerId2);
		}

		private void InitializeInputs<TInput1, TInput2>(int playerId1, int playerId2)
			where TInput1 : IMatchPlayerInput
			where TInput2 : IMatchPlayerInput
		{
			// define who turns first and who second 
			var startPlayerId = _gameEngine.TurnOwner;
			var secondPlayerId = startPlayerId == playerId1 
				? playerId2 
				: playerId1;
			
			_inputs[0] = CreatePlayerInput<TInput1>(startPlayerId, SymbolKey.Cross);
			_inputs[1] = CreatePlayerInput<TInput2>(secondPlayerId, SymbolKey.Circle);
		}

		private IMatchPlayerInput CreatePlayerInput<T>(int playerId, SymbolKey symbolKey) where T : IMatchPlayerInput
		{
			return (IMatchPlayerInput)Activator.CreateInstance(typeof(T), new object[]
			{
				playerId, symbolKey, _gameEngine, this
			});
		}

		private IMatchPlayerInput GetPlayerInputFor(int playerId)
		{
			return _inputs.First(i => i.PlayerId == playerId);
		}
	}
}