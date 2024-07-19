using System;
using UnityEngine;

namespace Presentation
{
	public class PresentationView : MonoBehaviour
	{
		public event Action OnDestroyed;
		
		private void OnDestroy()
		{
			OnDestroyed?.Invoke();
		}
	}
}