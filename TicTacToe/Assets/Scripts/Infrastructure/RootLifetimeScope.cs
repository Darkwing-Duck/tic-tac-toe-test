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
		/// <summary>
		/// Installs all dependencies
		/// </summary>
		protected override void Configure(IContainerBuilder builder)
		{
			// Entry point
			builder.RegisterEntryPoint<Startup>();
			
			InstallServices(builder);
			InstallAppStates(builder);
			InstallFactories(builder);
			InstallPresenters(builder);
			InstallViewProviders(builder);
			InstallEngine(builder);
			
			// Router
			builder.RegisterVitalRouter(routing => {
				routing.Filters.Add<ActivatePlayerInputInterceptor>();
			});
		}
		
		private void InstallServices(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<AppNavigatorService>(Lifetime.Scoped).As<IAppNavigatorService>();
			builder.Register<MatchService>(Lifetime.Scoped).As<IMatchService>();
		}

		private void InstallAppStates(IContainerBuilder builder)
		{
			builder.Register<HomeState>(Lifetime.Transient);
			builder.Register<GameState>(Lifetime.Transient);
		}
		
		private void InstallFactories(IContainerBuilder builder)
		{
			builder.Register<PresenterFactory>(Lifetime.Scoped).As<IPresenterFactory>();
			builder.Register<AppStateFactory>(Lifetime.Scoped).As<IAppStateFactory>();
		}
		
		private void InstallPresenters(IContainerBuilder builder)
		{
			builder.Register<AppNavigationPresenter>(Lifetime.Scoped);
			builder.Register<PopupsLayerPresenter>(Lifetime.Scoped).As<IPopupService>().AsSelf();
			
			builder.Register<HomeScreenPresenter>(Lifetime.Transient);
			builder.Register<GameScreenPresenter>(Lifetime.Transient);
			builder.Register<GameResultPopupPresenter>(Lifetime.Transient);
		}
		
		private void InstallViewProviders(IContainerBuilder builder)
		{
			builder.Register<ResourcesViewProvider<GameScreenView>>(Lifetime.Scoped).As<IViewProvider<GameScreenView>>();
			builder.Register<ResourcesViewProvider<HomeScreenView>>(Lifetime.Scoped).As<IViewProvider<HomeScreenView>>();
			builder.Register<ResourcesViewProvider<AppNavigationView>>(Lifetime.Scoped).As<IViewProvider<AppNavigationView>>();
			builder.Register<ResourcesViewProvider<PopupsLayerView>>(Lifetime.Scoped).As<IViewProvider<PopupsLayerView>>();
			builder.Register<ResourcesViewProvider<GameResultPopupView>>(Lifetime.Scoped).As<IViewProvider<GameResultPopupView>>();
		}
		
		private void InstallEngine(IContainerBuilder builder)
		{
			builder.Register<GameResultCalculator>(Lifetime.Scoped).As<IGameResultCalculator>();
			builder.Register<GameEngine>(Lifetime.Scoped).As<IEngine, IEngineReadOnly>();
		}
	}
}