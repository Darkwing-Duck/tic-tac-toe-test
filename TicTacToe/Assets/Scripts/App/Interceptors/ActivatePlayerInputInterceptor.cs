using App.Match;
using Commands;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace Mutators
{
	public class ActivatePlayerInputInterceptor : TypedCommandInterceptro<ActivatePlayerInputCommand>
	{
		private readonly IMatchService _matchService;

		public ActivatePlayerInputInterceptor(IMatchService matchService)
		{
			_matchService = matchService;
		}

		override public async UniTask InvokeAsync(ActivatePlayerInputCommand command, PublishContext context, PublishContinuation<ActivatePlayerInputCommand> next)
		{
			_matchService.ActivateInput();
			await next(command, context);
		}
	}
}