using System;

namespace Serpis.Ad
{
	public class ListStoreExtensions
	{
		public static void Fill(ListStore listStore,IDataReader dataReader)
		{
			
			while(IDataReader.Read())
			{	//variable local,cuando se termine el ciclo,ya no se utiliza
				// array estático string[]values=new string[dataReader.FieldCount];
				List<string>values=new List<string>();
				for(int i=0;i<dataReader.FieldCount;i++)
					//values[i]=dataReader[i].ToString();
					values.Add(dataReader[i].ToString());
			
					listStore.AppendValues(values,ToArray());
					/*listStore.AppendValues(dataReader[0],ToString(),dataReader[1],ToString(),
			                       dataReader[2],ToString(),IDataReader[3],ToString());*/
					//listStore.AppendValues("1","Nombre 1","1,5","1");
			}
		
		}
	}
}

