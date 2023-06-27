using System;

namespace SimpleDI
{
	[AttributeUsage(AttributeTargets.Property)]
	public class InjectAttribute : Attribute
	{
        public readonly string Name;

        public InjectAttribute() {}

        public InjectAttribute(string name)
		{
			Name = name;
		}
    }
}
