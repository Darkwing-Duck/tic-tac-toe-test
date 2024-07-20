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

		public override async UniTask InvokeAsync(PlayerTurnCommand command, PublishContext context, PublishContinuation<PlayerTurnCommand> next)
		{
			_engine.Turn(command.Position);
			await next(command, context);
		}
	}
}