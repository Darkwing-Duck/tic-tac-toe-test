using UnityEngine;

namespace Core
{
	public interface IEngineReadOnly
	{
		public int PlayerOneId { get; }
		public int PlayerTwoId { get; }

		public int TurnOwner { get; }
		public int TurnNumber { get; }
		
		Board Board { get; }
		bool TryGetGameResult(out GameResult result);
	}
	
	public interface IEngine : IEngineReadOnly
	{
		void Initialize(int playerOneId, int playerTwoId);
		void Turn(int playerId, Vector2Int location);
	}
}