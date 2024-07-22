using Commands;
using Core;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace Mutators
{
	public class PlayerTurnMutator : TypedCommandInterceptro<PlayerTurnCommand>
	{
		private readonly GameEngine _engine;

		public PlayerTurnMutator(GameEngine engine)
		{
			_engine = engine;
		}

		override public async UniTask InvokeAsync(PlayerTurnCommand command, PublishContext context, PublishContinuation<PlayerTurnCommand> next)
		{
			_engine.Turn(command.Owner, command.Position);
			await next(command, context);
		}
	}
}