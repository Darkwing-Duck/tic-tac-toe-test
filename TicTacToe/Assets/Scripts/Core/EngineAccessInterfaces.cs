using UnityEngine;

namespace Core
{
	/// <summary>
	/// Provides readonly access to the engine to be able get the actual state
	/// </summary>
	public interface IEngineReadOnly
	{
		public int PlayerOneId { get; }
		public int PlayerTwoId { get; }

		public int TurnOwner { get; }
		public int TurnNumber { get; }
		
		Board Board { get; }
		bool TryGetGameResult(out GameResult result);
	}
	
	/// <summary>
	/// Provides full access to the engien
	/// </summary>
	public interface IEngine : IEngineReadOnly
	{
		void Initialize(int playerOneId, int playerTwoId);
		void Turn(int playerId, Vector2Int location);
	}
}