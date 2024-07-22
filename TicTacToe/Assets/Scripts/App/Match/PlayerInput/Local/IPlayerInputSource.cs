using UnityEngine;

namespace App.Match
{
	/// <summary>
	/// Tag component just to mark all the game board slots MonoBehaviours to help LocalPlayerInput handle input
	/// without any dependency on concrete monoBehaviour
	/// </summary>
	public interface IPlayerInputSource
	{
		Vector2Int SlotPosition { get; }
	}
}