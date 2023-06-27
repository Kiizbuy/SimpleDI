using System;

namespace SimpleDI
{
	public interface IInjectDependent : IDisposable
	{
		void OnInjected();
	}
}
