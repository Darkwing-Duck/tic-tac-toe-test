using App.Services;
using App.States;
using App.States.Gameplay;
using Core;
using Input;
using Mutators;
using Presentation;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;

namespace Infrastructure
{
	public class RootLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			// entry point
			builder.RegisterEntryPoint<Startup>();
			
			// Services
			builder.Register<AppNavigatorService>(Lifetime.Scoped).As<IAppNavigatorService>();
			builder.RegisterEntryPoint<PlayerService>().As<IPlayerService>();
			
			// App States
			builder.Register<LoadingState>(Lifetime.Transient);
			builder.Register<GameState>(Lifetime.Transient);
			
			// factories
			builder.Register<PresenterFactory>(Lifetime.Scoped).As<IPresenterFactory>();
			builder.Register<AppStateFactory>(Lifetime.Scoped).As<IAppStateFactory>();
			
			// presenters
			builder.Register<AppNavigationPresenter>(Lifetime.Scoped);
			builder.Register<LoadingScreenPresenter>(Lifetime.Transient);
			builder.Register<GameScreenPresenter>(Lifetime.Transient);
			
			// view providers
			builder.Register<ResourcesViewProvider<GameScreenView>>(Lifetime.Transient).As<IModuleViewProvider<GameScreenView>>();
			builder.Register<ResourcesViewProvider<LoadingScreenView>>(Lifetime.Transient).As<IModuleViewProvider<LoadingScreenView>>();
			builder.Register<ResourcesViewProvider<AppNavigationView>>(Lifetime.Transient).As<IModuleViewProvider<AppNavigationView>>();
			
			// Engine
			builder.Register<GameResultCalculator>(Lifetime.Scoped).As<IGameResultCalculator>();
			builder.Register<GameEngine>(Lifetime.Scoped);
			
			// router
			builder.RegisterVitalRouter(routing => {
				routing.Filters.Add<PlayerTurnMutator>();
			});
			
			// input
			builder.RegisterEntryPoint<MousePlayerInput>();
		}
	}
}