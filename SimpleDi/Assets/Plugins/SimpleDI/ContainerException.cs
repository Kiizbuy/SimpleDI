using System;

namespace SimpleDI
{
	public class ContainerException : Exception
	{
		public ContainerException() {}
		public ContainerException(string message) : base(message) {}
		public ContainerException(string message, Exception innerException) : base(message, innerException) {}
	}
}
