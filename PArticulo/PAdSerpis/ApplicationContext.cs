using System;
using System.Data;

namespace Serpis.Ad
{
	public class ApplicationContext
	{
		private ApplicationContext ()
		{
			
		}
		
		private static ApplicationContext instance = new ApplicationContext();//patron singleton,variable global
		
		public static ApplicationContext Instance{
			get {return instance;}
		}
		
		private IDbConnection dbConnection;
		public IDbConnection DbConnection{//instancia
			get {return dbConnection;}
			set {dbConnection = value;}
			
		}
	}
}

