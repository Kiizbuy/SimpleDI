using SimpleDI.Providers;

namespace SimpleDI
{
    internal class ProviderWrapper<T> : IProviderWrapper where T:class
	{
		private readonly IObjectProvider<T> _provider;

		public ProviderWrapper(IObjectProvider<T> provider)
		{
			_provider = provider;
		}

		public object GetObject(DiContainer diContainer)
		{
			return _provider.GetObject(diContainer);
		}
	}
}
