using VitalRouter;

namespace App.States
{
	public interface IAppState
	{
		internal void Activate();
		internal void Deactivate();
		internal void SendNotificationTo(ICommandPublisher commandPublisher);
	}
	
	/// <summary>
	/// Base application state
	/// </summary>
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
}