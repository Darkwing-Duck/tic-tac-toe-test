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
			
			// factories
			builder.Register<PresenterFactory>(Lifetime.Singleton).As<IPresenterFactory>();
			
			// presenters
			builder.Register<AppNavigationPresenter>(Lifetime.Singleton).As<IAppNavigator>().AsSelf();
			builder.Register<LoadingScreenPresenter>(Lifetime.Transient);
			builder.Register<GameScreenPresenter>(Lifetime.Transient);
			
			// view providers
			builder.Register<ResourcesViewProvider<GameScreenView>>(Lifetime.Transient).As<IModuleViewProvider<GameScreenView>>();
			builder.Register<ResourcesViewProvider<LoadingScreenView>>(Lifetime.Transient).As<IModuleViewProvider<LoadingScreenView>>();
			builder.Register<ResourcesViewProvider<AppNavigationView>>(Lifetime.Transient).As<IModuleViewProvider<AppNavigationView>>();
			
			// input
			builder.RegisterEntryPoint<MousePlayerInput>();
			
			// router
			builder.RegisterVitalRouter(routing =>
			{
				routing.Map<GameScreenPresenter>();

				routing.Filters.Add<PlayerTurnMutator>();
			});

			var engine = new GameEngine();
			engine.Setup(0, 1);
			builder.RegisterInstance(engine);
		}
	}
}