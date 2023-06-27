using System;

namespace SimpleDI.Providers
{
	public class ActivatorObjectProvider<T> : IObjectProvider<T> where T : class, new()
	{
		public T GetObject(DiContainer diContainer)
		{
			var obj = Activator.CreateInstance<T>();
			diContainer.BuildUp(obj);
			return obj;
		}
	}
}
