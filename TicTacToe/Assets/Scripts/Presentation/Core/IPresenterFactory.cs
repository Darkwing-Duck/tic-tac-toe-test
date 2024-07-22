namespace Presentation
{
	/// <summary>
	/// General presenter factory interface.
	/// Should be realized in composition root layer to not have dependency to any DI container
	/// </summary>
	public interface IPresenterFactory
	{
		public TPresenter Create<TPresenter>() where TPresenter : IPresenter;
	}
}