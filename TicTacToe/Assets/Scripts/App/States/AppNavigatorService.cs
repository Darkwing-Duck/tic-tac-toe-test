using App.Common;
using VContainer.Unity;
using VitalRouter;

namespace App.States
{
	public interface IAppNavigatorService
	{
		IAppState ActiveState { get; }
		void GoToState<TState>() where TState : IAppState;
	}
	
	/// <summary>
	/// Describes application navigation service to swtch between states
	/// </summary>
	public class AppNavigatorService : IAppNavigatorService, ITickable
	{
		public IAppState ActiveState { get; private set; }

		private IAppStateFactory _stateFactory;
		private ICommandPublisher _commandPublisher;

		public AppNavigatorService(IAppStateFactory stateFactory, ICommandPublisher commandPublisher)
		{
			_stateFactory = stateFactory;
			_commandPublisher = commandPublisher;
		}

		public void Tick()
		{
			if (ActiveState is IUpdatable casted) {
				casted.Update();
			}
		}

		public void GoToState<TState>() where TState : IAppState
		{
			ActiveState?.Deactivate();
			ActiveState = _stateFactory.Create<TState>();
			ActiveState?.Activate();
			
			// notify outer world about app state changed (Navigation Presenter will handle it)
			ActiveState?.SendNotificationTo(_commandPublisher);
		}
	}
}