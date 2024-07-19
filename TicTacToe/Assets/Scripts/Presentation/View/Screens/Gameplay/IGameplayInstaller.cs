using VContainer;
using VContainer.Unity;

namespace Presentation
{
	public interface IGameplayInstaller : IInstaller
	{
		void Install(IContainerBuilder builder);
	}
}