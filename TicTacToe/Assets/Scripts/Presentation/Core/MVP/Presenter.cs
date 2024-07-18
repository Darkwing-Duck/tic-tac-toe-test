using UnityEngine;

namespace Presentation.Core
{
	
	/// <summary>
	/// Stateful presenter implementation.
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TModel"></typeparam>
	public abstract class Presenter<TView, TModel> 
		where TView : ModuleView
	{
		protected TView View;
		protected readonly TModel Model;

		private IModuleViewProvider<TView> _viewProvider;

		public Presenter(IModuleViewProvider<TView> viewProvider, TModel model)
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
		where TView : ModuleView
	{
		protected StatelessPresenter(IModuleViewProvider<TView> viewProvider) : base(viewProvider, new EmptyModel())
		{ }
	}
	
	/// <summary>
	/// Empty model object to be used in StatelessPresenter.
	/// </summary>
	public class EmptyModel {}
}