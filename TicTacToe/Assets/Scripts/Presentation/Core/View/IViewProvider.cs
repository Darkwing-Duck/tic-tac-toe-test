using UnityEngine;

namespace Presentation
{
	
	/// <summary>
	/// Indicates that provider marked by this interface can release view 
	/// </summary>
	public interface ICanRelease<in TView> where TView : MonoBehaviour
	{
		void Release(TView view);
	}
	
	/// <summary>
	/// Describes common view provider
	/// </summary>
	/// <typeparam name="TView">view of concrete type</typeparam>
	public interface IViewProvider<out TView> where TView : MonoBehaviour
	{
		TView Get(Transform parent = null);
	}
}