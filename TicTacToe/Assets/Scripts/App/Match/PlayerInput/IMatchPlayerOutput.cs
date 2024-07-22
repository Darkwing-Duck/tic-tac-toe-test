using UnityEngine;

namespace App.Match
{
	public interface IMatchPlayerOutput
	{
		void MakeTurn(int playerId, Vector2Int position);
	}
}