using VitalRouter;

namespace Commands
{
	/// <summary>
	/// Used to notify AppNavigationPresenter to go to HomeScreenPresenter
	/// </summary>
	public struct HomeStateActivated : ICommand
	{ }
	
	/// <summary>
	/// Used to notify AppNavigationPresenter to go to GameScreenPresenter
	/// </summary>
	public struct GameStateActivated : ICommand
	{ }
}