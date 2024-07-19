using Core;
using Input;
using Mutators;
using Presentation;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;

namespace Infrastructure
{
	public class GameplayInstaller : IGameplayInstaller
	{
		public void Install(IContainerBuilder builder)
		{
			// input
			builder.RegisterEntryPoint<MousePlayerInput>();
			
			// model
			var engine = new GameEngine();
			engine.Setup(0, 1);
			builder.RegisterInstance(engine);
			
			// router
			builder.RegisterVitalRouter(routing => {
				// routing.Map<GameScreenPresenter>();
			
				routing.Filters.Add<PlayerTurnMutator>();
			});
		}
	}
}