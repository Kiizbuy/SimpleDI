using System;

namespace SimpleDI
{
    internal class ContainerKey
	{
		public ContainerKey(Type type) : this(type, null)
		{
		}

		public ContainerKey(Type type, string name)
		{
			Type = type;
			Name = name;
		}

		public Type Type { get; private set; }

		public string Name { get; private set; }

		protected bool Equals(ContainerKey other)
		{
			return (Type == other.Type) && string.Equals(Name, other.Name);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ContainerKey) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			}
		}

		public static bool operator ==(ContainerKey left, ContainerKey right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ContainerKey left, ContainerKey right)
		{
			return !Equals(left, right);
		}
	}
}
