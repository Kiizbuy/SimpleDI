namespace SimpleDI.Providers
{
	public class InstanceProvider<T> : IObjectProvider<T> where T : class
	{
		private readonly T _instance;

		public InstanceProvider(T instance)
		{
			_instance = instance;
		}

		public T GetObject(DiContainer diContainer)
		{
			return _instance;
		}
	}
}
