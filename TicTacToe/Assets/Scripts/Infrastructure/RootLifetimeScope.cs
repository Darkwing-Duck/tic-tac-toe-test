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
			builder.Register<GameplayInstaller>(Lifetime.Scoped).As<IGameplayInstaller>();
			
			// entry point
			builder.RegisterEntryPoint<Startup>();
			
			// factories
			builder.Register<PresenterFactory>(Lifetime.Scoped).As<IPresenterFactory>();
			
			// presenters
			builder.Register<AppNavigationPresenter>(Lifetime.Scoped).As<IAppNavigator>().AsSelf();
			builder.Register<LoadingScreenPresenter>(Lifetime.Transient);
			builder.Register<GameScreenPresenter>(Lifetime.Transient);
			
			// view providers
			builder.Register<ResourcesViewProvider<GameScreenView>>(Lifetime.Transient).As<IModuleViewProvider<GameScreenView>>();
			builder.Register<ResourcesViewProvider<LoadingScreenView>>(Lifetime.Transient).As<IModuleViewProvider<LoadingScreenView>>();
			builder.Register<ResourcesViewProvider<AppNavigationView>>(Lifetime.Transient).As<IModuleViewProvider<AppNavigationView>>();
			
			// router
			builder.RegisterVitalRouter(routing => {
				// routing.Map<GameScreenPresenter>();
			});
		}
	}
}