using Commands;
using Core;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace Mutators
{
	public class PlayerTurnMutator : TypedCommandInterceptro<PlayerTurnCommand>
	{
		private readonly GameEngine _engine;
		private readonly ICommandPublisher _commandPublisher;

		public PlayerTurnMutator(GameEngine engine, ICommandPublisher commandPublisher)
		{
			_engine = engine;
			_commandPublisher = commandPublisher;
		}

		public override async UniTask InvokeAsync(PlayerTurnCommand command, PublishContext context, PublishContinuation<PlayerTurnCommand> next)
		{
			_engine.Turn(command.Owner, command.Position);
			await next(command, context);

			if (_engine.TryGetGameResult(out var result)) {
				_commandPublisher.Enqueue(new GameFinishedCommand(result));
			} else {
				_commandPublisher.Enqueue(new NextTurnCommand(_engine.TurnNumber, _engine.TurnOwner));
			}
		}
	}
}