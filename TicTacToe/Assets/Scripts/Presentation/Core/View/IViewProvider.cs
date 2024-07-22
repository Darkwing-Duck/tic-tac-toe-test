using UnityEngine;

namespace Presentation
{
	public interface IViewProvider<out TView> where TView : MonoBehaviour
	{
		TView Get(Transform parent = null);
		void Release();
	}
}