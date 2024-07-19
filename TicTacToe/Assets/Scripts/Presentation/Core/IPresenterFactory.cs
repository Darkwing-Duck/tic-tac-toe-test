namespace Presentation
{
	public interface IPresenterFactory
	{
		public TPresenter Create<TPresenter>() where TPresenter : IPresenter;
	}
}