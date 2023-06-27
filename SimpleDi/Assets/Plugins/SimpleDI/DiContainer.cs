using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleDI.Providers;

namespace SimpleDI
{
    public partial class DiContainer
    {
		private readonly Dictionary<ContainerKey, IProviderWrapper> _providers = new Dictionary<ContainerKey, IProviderWrapper>();
 		
		public DiContainer RegisterType<T>(string name = null) where T : class, new()
		{
			return RegisterProvider(new ActivatorObjectProvider<T>(), name);
		}

		public DiContainer RegisterType<TBase, TDerived>(string name = null) where TDerived : class, TBase, new()
		{
			return RegisterProvider<TBase, TDerived>(new ActivatorObjectProvider<TDerived>(), name);
		}
		public DiContainer RegisterSingleton<T>(string name = null) where T : class, new()
		{
			return RegisterProvider(new SingletonProvider<T>(), name);
		}

		public DiContainer RegisterSingleton<TBase, TDerived>(string name = null) where TDerived : class, TBase, new()
		{
			return RegisterProvider<TBase, TDerived>(new SingletonProvider<TDerived>(), name);
		}

		public DiContainer RegisterInstance<T>(T obj, string name = null) where T : class
		{
			return RegisterProvider(new InstanceProvider<T>(obj), name);
		}

		public DiContainer RegisterInstance<TBase, TDerived>(TDerived obj, string name = null) where TDerived : class, TBase
		{
			return RegisterProvider<TBase, TDerived>(new InstanceProvider<TDerived>(obj), name);
		}

		public DiContainer RegisterProvider<T>(IObjectProvider<T> provider, string name = null) where T : class
		{
			var key = new ContainerKey(typeof (T), name);
			_providers[key] = new ProviderWrapper<T>(provider);
			return this;
		}

		public DiContainer RegisterProvider<TBase, TDerived>(IObjectProvider<TDerived> provider, string name = null) where TDerived : class, TBase
		{
			var key = new ContainerKey(typeof (TBase), name);
			_providers[key] = new ProviderWrapper<TDerived>(provider);
			return this;
		}
	
		public T Resolve<T>(string name = null)
		{
			return (T) Resolve(typeof(T), name);
		}
		
		public object Resolve(Type type, string name = null)
		{
            if (!_providers.TryGetValue(new ContainerKey(type, name), out var provider))
            {
                throw new ContainerException("Can't resolve type " + type.FullName + (name == null ? "" : " registered with name \"" + name + "\""));
            }

            return provider.GetObject(this);
		}

		internal void BuildUp(object obj)
		{
			var type = obj.GetType();
			var members = type.FindMembers(MemberTypes.Property | MemberTypes.Field,
				BindingFlags.FlattenHierarchy 
                | BindingFlags.SetProperty 
                | BindingFlags.Public 
                | BindingFlags.NonPublic 
                | BindingFlags.Instance,
				null, 
null);

			foreach (var member in members)
			{
				var attributes = member.GetCustomAttributes(typeof (InjectAttribute), true);
				if (!attributes.Any())
					continue;

				var firstAttribute = (InjectAttribute)attributes[0];
				var propertyInfo = (PropertyInfo)member;
				object valueObj;
				
				try
				{
					valueObj = Resolve(propertyInfo.PropertyType, firstAttribute.Name);
				}
				catch (ContainerException ex)
				{
					throw new ContainerException("Can't resolve property \"" + propertyInfo.Name + "\" of class \"" + type.FullName + "\"", ex);
				}
				
				propertyInfo.SetValue(obj, valueObj, null);
			}

			var dependent = obj as IInjectDependent;
            dependent?.OnInjected();
        }
    }
}
