using App.Match;
using App.States;
using App.States.Gameplay;
using Core;
using Mutators;
using Presentation;
using Presentation.Popups;
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
			builder.RegisterEntryPoint<AppNavigatorService>(Lifetime.Scoped).As<IAppNavigatorService>();
			builder.Register<MatchService>(Lifetime.Scoped).As<IMatchService>();
			
			// App States
			builder.Register<HomeState>(Lifetime.Transient);
			builder.Register<GameState>(Lifetime.Transient);
			
			// factories
			builder.Register<PresenterFactory>(Lifetime.Scoped).As<IPresenterFactory>();
			builder.Register<AppStateFactory>(Lifetime.Scoped).As<IAppStateFactory>();
			
			// presenters
			builder.Register<AppNavigationPresenter>(Lifetime.Scoped);
			builder.Register<PopupsLayerPresenter>(Lifetime.Scoped).As<IPopupService>().AsSelf();
			builder.Register<HomeScreenPresenter>(Lifetime.Transient);
			builder.Register<GameScreenPresenter>(Lifetime.Transient);
			builder.Register<GameResultPopupPresenter>(Lifetime.Transient);
			
			// view providers
			builder.Register<ResourcesViewProvider<GameScreenView>>(Lifetime.Transient).As<IViewProvider<GameScreenView>>();
			builder.Register<ResourcesViewProvider<HomeScreenView>>(Lifetime.Transient).As<IViewProvider<HomeScreenView>>();
			builder.Register<ResourcesViewProvider<AppNavigationView>>(Lifetime.Transient).As<IViewProvider<AppNavigationView>>();
			builder.Register<ResourcesViewProvider<PopupsLayerView>>(Lifetime.Transient).As<IViewProvider<PopupsLayerView>>();
			builder.Register<ResourcesViewProvider<GameResultPopupView>>(Lifetime.Transient).As<IViewProvider<GameResultPopupView>>();
			
			// Engine
			builder.Register<GameResultCalculator>(Lifetime.Scoped).As<IGameResultCalculator>();
			builder.Register<GameEngine>(Lifetime.Scoped).As<IEngine, IEngineReadOnly>();
			
			// router
			builder.RegisterVitalRouter(routing => {
				routing.Filters.Add<ActivatePlayerInputInterceptor>();
			});
		}
	}
}