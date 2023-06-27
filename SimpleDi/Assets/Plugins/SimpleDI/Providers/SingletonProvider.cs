using System;

namespace SimpleDI.Providers
{
	public class SingletonProvider<T> : IObjectProvider<T> where T : class, new()
	{
		private bool _inited;
		private T _instance;
		
		public T GetObject(DiContainer diContainer)
		{
			if (!_inited)
			{
				_instance  = Activator.CreateInstance<T>();
				_inited = true;
				diContainer.BuildUp(_instance);
			}
			return _instance;
		}
	}
}
