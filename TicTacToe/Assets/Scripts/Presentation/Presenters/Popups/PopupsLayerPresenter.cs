using System;
using System.Collections.Generic;

namespace Presentation.Popups
{
	public interface IPopup
	{ }
	
	public interface IPopupService
	{
		void Show<TPopup>() where TPopup : IPresenter, IPopup;
		void Hide<TPopup>() where TPopup : IPresenter, IPopup;
		void HideAll();
	}
	
	public class PopupsLayerPresenter : StatelessPresenter<PopupsLayerView>, IPopupService
	{
		private readonly IPresenterFactory _presenterFactory;
		private readonly Dictionary<Type, IPresenter> _activePopups = new();

		public PopupsLayerPresenter(IViewProvider<PopupsLayerView> viewProvider, IPresenterFactory presenterFactory) : base(viewProvider)
		{
			_presenterFactory = presenterFactory;
		}

		protected override void InitializeView(PopupsLayerView view)
		{
			view.name = "PopupsLayer";
		}

		public void Show<TPopup>() where TPopup : IPresenter, IPopup
		{
			var popup = _presenterFactory.Create<TPopup>();
			popup.ShowUnder(View.transform);
			_activePopups.Add(typeof(TPopup), popup);
		}

		public void Hide<TPopup>() where TPopup : IPresenter, IPopup
		{
			var popupType = typeof(TPopup);
			
			if (!_activePopups.ContainsKey(popupType)) {
				throw new NullReferenceException($"You're trying to close inactive popup '{popupType.Name}'");
			}

			HidePopup(popupType);
		}

		public void HideAll()
		{
			foreach (var pair in _activePopups) {
				HidePopup(pair.Key);
			}
		}

		private void HidePopup(Type popupType)
		{
			_activePopups[popupType].Hide();
			_activePopups.Remove(popupType);
		}
	}
}