namespace App.Match
{
	/// <summary>
	/// Describes a type of match 
	/// </summary>
	public enum MatchType
	{
		/// <summary>
		/// Two local players are playing on the same device with the same local input
		/// </summary>
		PlayerVsPlayer,
		
		/// <summary>
		/// One local player versus AI 
		/// </summary>
		PlayerVsBot,
		
		/// <summary>
		/// The battle of two AIs
		/// </summary>
		BotVsBot
	}
}