using System;

namespace Serpis.Ad
{
	public class TypeExtensiones
	{
		public static Type[]GetTypes (Type type,int Count)
		{
			List<Type>types=new List<Type>();
				for(int i=0;i<Count;i++)
					types.Add(type);
				return types.ToArray();
		}
	}
}

