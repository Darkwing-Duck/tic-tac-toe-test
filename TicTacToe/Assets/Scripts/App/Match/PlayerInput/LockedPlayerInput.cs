namespace App.Match
{
	/// <summary>
	/// Describes locked input logic, using between the turns when no one user can make turn
	/// </summary>
	public class LockedPlayerInput : MatchPlayerInput
	{
		public LockedPlayerInput() : base(-1, SymbolKey.Cross, null)
		{ }
	}
}