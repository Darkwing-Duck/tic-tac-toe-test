using VitalRouter;

namespace App.States
{
	public interface IAppState
	{
		internal void Activate();
		internal void Deactivate();
		internal void SendNotificationTo(ICommandPublisher commandPublisher);
	}
	
	public abstract class AppState<TCommand> : IAppState where TCommand : ICommand, new()
	{
		void IAppState.Activate()
		{
			OnActivate();
		}

		void IAppState.Deactivate()
		{
			OnDeactivate();
		}
		
		void IAppState.SendNotificationTo(ICommandPublisher commandPublisher)
		{
			commandPublisher.PublishAsync(new TCommand());
		}

		protected virtual void OnActivate()
		{ }

		protected virtual void OnDeactivate()
		{ }
	}
	
	public interface IAppNavigatorService
	{
		IAppState ActiveState { get; }
		void GoToState<TState>() where TState : IAppState;
	}
	
	public class AppNavigatorService : IAppNavigatorService
	{
		public IAppState ActiveState { get; private set; }

		private IAppStateFactory _stateFactory;
		private ICommandPublisher _commandPublisher;

		public AppNavigatorService(IAppStateFactory stateFactory, ICommandPublisher commandPublisher)
		{
			_stateFactory = stateFactory;
			_commandPublisher = commandPublisher;
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