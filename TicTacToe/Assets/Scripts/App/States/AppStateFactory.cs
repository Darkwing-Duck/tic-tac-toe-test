using VContainer;

namespace App.States
{
	public interface IAppStateFactory
	{
		TState Create<TState>() where TState : IAppState;
	}
	
	public class AppStateFactory : IAppStateFactory
	{
		private IObjectResolver _container;
		
		public AppStateFactory(IObjectResolver container)
		{
			_container = container;
		}
		
		public TState Create<TState>() where TState : IAppState
		{
			return _container.Resolve<TState>();
		}
	}
}