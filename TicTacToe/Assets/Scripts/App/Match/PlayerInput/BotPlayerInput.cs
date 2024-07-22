using System;
using App.Common;

namespace App.Match
{
	public class BotPlayerInput : MatchPlayerInput, IUpdatable
	{
		public BotPlayerInput(int playerId, SymbolKey symbolKey, IMatchPlayerOutput output) : base(playerId, symbolKey, output)
		{ }

		public void Update()
		{
			throw new NotImplementedException();
		}
	}
}