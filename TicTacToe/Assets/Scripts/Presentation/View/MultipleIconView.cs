using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Presentation
{
	public abstract class MultipleIconView : MonoBehaviour
	{
		[SerializeField]
		private Transform _iconsRoot;
		
		[SerializeField]
		private List<GameObject> _iconsList = new();

		private void Awake()
		{
			DisableAll();
		}

		public void SetIcon(string iconKey)
		{
			DisableAll();

			if (TryFindIcon(iconKey, out var result)) {
				result.SetActive(true);
			}
		}

		private void DisableAll()
		{
			foreach (var icon in _iconsList) {
				icon.SetActive(false);
			}
		}

		private bool TryFindIcon(string key, out GameObject result)
		{
			result = _iconsList.SingleOrDefault(i => i.name == key);
			return result is not null;
		}

		private void OnValidate()
		{
			_iconsList.Clear();
			
			foreach (Transform child in _iconsRoot) {
				_iconsList.Add(child.gameObject);
			}
		}
	}
}