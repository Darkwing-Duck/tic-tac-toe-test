using System.Collections.Generic;

namespace App.Services
{
	public enum SymbolKey
	{
		Cross,
		Circle
	}
	
	public class PlayerInfo
	{
		public readonly int Id;
		public readonly SymbolKey SymbolKey;

		public PlayerInfo(int id, SymbolKey symbolKey)
		{
			Id = id;
			SymbolKey = symbolKey;
		}
	}
	
	public interface IPlayerService
	{
		void InitializePlayers(PlayerInfo player1, PlayerInfo player2);
		PlayerInfo GetPlayer(int playerId);
	}
	
	public class PlayerService : IPlayerService
	{
		private Dictionary<int, PlayerInfo> _playersMap = new();
		
		public void InitializePlayers(PlayerInfo player1, PlayerInfo player2)
		{
			_playersMap.Clear();
			_playersMap.Add(player1.Id, player1);
			_playersMap.Add(player2.Id, player2);
		}

		public PlayerInfo GetPlayer(int playerId) => _playersMap[playerId];
	}
}