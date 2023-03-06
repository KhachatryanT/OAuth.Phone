using System.Collections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OAuth.Phone.Utils;

public static class RegisterAllAssignableTypeExtension
{
	public static void RegisterAllFromAssignableInterface<T>(this IServiceCollection serviceCollection, Assembly assembly,
		ServiceLifetime lifetime = ServiceLifetime.Scoped) =>
		RegisterAllFromAssignableInterface(serviceCollection, typeof(T), assembly, lifetime);

	public static void RegisterAllFromAssignableInterface(this IServiceCollection serviceCollection,
		Type openGenericType,
		Assembly assembly,
		ServiceLifetime lifetime = ServiceLifetime.Scoped)
	{
		var types = assembly.GetTypes();
		using IEnumerator<AssemblyScanResult> enumerator = openGenericType.IsGenericType
			? new GenericScanEnumerator(types, openGenericType)
			: new NotGenericScanEnumerator(types, openGenericType);
		
		while (enumerator.MoveNext())
		{
			var scanResult = enumerator.Current;
			
			serviceCollection.Add(
				new ServiceDescriptor(
					serviceType: scanResult.ServiceType,
					implementationType: scanResult.ImplementationType,
					lifetime: lifetime));
		}
	}

	private class AssemblyScanResult
	{
		public AssemblyScanResult(Type serviceType, Type implementationType)
		{
			ServiceType = serviceType;
			ImplementationType = implementationType;
		}

		public Type ServiceType { get; }
		public Type ImplementationType { get; }
	}

	private class GenericScanEnumerator : IEnumerator<AssemblyScanResult>
	{
		private readonly IEnumerator<AssemblyScanResult> _internalEnumerator;

		public GenericScanEnumerator(IEnumerable<Type> types, Type baseType)
		{
			var query = from type in types
				where !type.IsAbstract && !type.IsGenericTypeDefinition
				let interfaces = type.GetInterfaces()
				let genericInterfaces =
					interfaces.Where(i =>
						i.IsGenericType && (i.GetGenericTypeDefinition() == baseType || i == baseType))
				let matchingInterface = genericInterfaces.FirstOrDefault()
				where matchingInterface != null
				select new AssemblyScanResult(matchingInterface, type);

			_internalEnumerator = query.GetEnumerator();
		}


		public bool MoveNext() => _internalEnumerator.MoveNext();

		public void Reset() => _internalEnumerator.Reset();

		public AssemblyScanResult Current => _internalEnumerator.Current;

		object IEnumerator.Current => Current;

		public void Dispose() => _internalEnumerator.Dispose();
	}

	private class NotGenericScanEnumerator : IEnumerator<AssemblyScanResult>
	{
		private readonly IEnumerator<AssemblyScanResult> _internalEnumerator;

		public NotGenericScanEnumerator(IEnumerable<Type> types, Type baseType)
		{
			// todo checkit
			var query = from type in types
				where !type.IsAbstract && !type.IsGenericTypeDefinition
				let interfaces = type.GetInterfaces()
				let genericInterfaces =
					interfaces.Where(baseType.IsAssignableFrom)
				let matchingInterface = genericInterfaces.FirstOrDefault()
				where matchingInterface != null
				select new AssemblyScanResult(matchingInterface, type);

			_internalEnumerator = query.GetEnumerator();
		}


		public bool MoveNext() => _internalEnumerator.MoveNext();

		public void Reset() => _internalEnumerator.Reset();

		public AssemblyScanResult Current => _internalEnumerator.Current;

		object IEnumerator.Current => Current;

		public void Dispose() => _internalEnumerator.Dispose();
	}
}