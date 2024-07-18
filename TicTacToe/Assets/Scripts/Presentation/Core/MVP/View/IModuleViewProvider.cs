using UnityEngine;

namespace Presentation.Core
{
	public interface IModuleViewProvider<out TView> where TView : MonoBehaviour
	{
		TView Get(Transform parent = null);
		void Release();
	}
}