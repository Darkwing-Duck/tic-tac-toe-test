using UnityEngine;

namespace Presentation
{

	public interface IPresenter
	{
		void Show();
		void ShowUnder(Transform parent);
		void Hide();
	}
	
	/// <summary>
	/// Stateful presenter implementation.
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TModel"></typeparam>
	public abstract class Presenter<TView, TModel> : IPresenter
		where TView : PresentationView
	{
		protected TView View;
		protected readonly TModel Model;

		private IViewProvider<TView> _viewProvider;

		public Presenter(IViewProvider<TView> viewProvider, TModel model)
		{
			_viewProvider = viewProvider;
			Model = model;
			Initialize();
		}
		
		private void Initialize()
		{
			InitializeModel(Model);
		}

		protected virtual void InitializeView(TView view)
		{ }
		
		protected virtual void InitializeModel(TModel model)
		{ }

		public void Show() => ShowUnder(null);
		
		public void ShowUnder(Transform parent)
		{
			View = _viewProvider.Get(parent);
			InitializeView(View);
			
			View.OnDestroyed += OnViewWasDestroyedImplicitly;
			
			OnActivate();
		}

		private void OnViewWasDestroyedImplicitly()
		{
			View.OnDestroyed -= OnViewWasDestroyedImplicitly;
			OnDeactivate();
		}

		public void Hide()
		{
			View.OnDestroyed -= OnViewWasDestroyedImplicitly;
			_viewProvider.Release();
			OnDeactivate();
		}
		
		protected virtual void OnActivate()
		{ }
		
		protected virtual void OnDeactivate()
		{ }
	}

	/// <summary>
	/// Presenter that doesn't need any state so it will use 'EmptyModel' model.
	/// </summary>
	public abstract class StatelessPresenter<TView> : Presenter<TView, EmptyModel>
		where TView : PresentationView
	{
		protected StatelessPresenter(IViewProvider<TView> viewProvider) : base(viewProvider, new EmptyModel())
		{ }
	}
	
	/// <summary>
	/// Empty model object to be used in StatelessPresenter.
	/// </summary>
	public class EmptyModel {}
}