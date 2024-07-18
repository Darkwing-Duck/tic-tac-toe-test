using System;
using UnityEngine;

namespace Presentation.Core
{
	public class ModuleView : MonoBehaviour
	{
		public event Action OnDestroyed;
		
		private void OnDestroy()
		{
			OnDestroyed?.Invoke();
		}
	}
}