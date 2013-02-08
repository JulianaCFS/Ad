using Gtk;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
 

using Serpis.Ad;
using System;
using System.Collections;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		//Visualiza el nombre completo de la clase
		/*Categoria categoria = new Categoria();
		Console.WriteLine("Categoria", categoria);
		if(true)
			return;*/
			
		
		
		Configuration configuration = new Configuration();
		configuration.Configure ();
		configuration.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
		configuration.AddAssembly(typeof(Categoria).Assembly);
		new SchemaExport(configuration).Execute(true, false, false);
		
		//para acceder a la base de datos
		ISessionFactory sessionFactory = configuration.BuildSessionFactory();
		
		//updateCategoria (sessionFactory);
		
		//insertCategoria (sessionFactory);
		
		//loadArticulo(sessionFactory);
		
		
			ISession session = sessionFactory.OpenSession();
			ICriteria criteria = session.CreateCriteria(typeof(Articulo));
			criteria.SetFetchMode("Categoria",FetchMode.Join);
			IList list = criteria.List();
			foreach (Articulo articulo in list)
				Console.WriteLine("Articulo Id={0} Nombre={1}",articulo.Id, articulo.Nombre);
		
		session.Close();
		
		sessionFactory.Close ();
		
	}
	private void loadArticulo(ISessionFactory sessionFactory){
		using (ISession session = sessionFactory.OpenSession()){
			Articulo articulo = (Articulo)session.Load (typeof(Articulo),2L);
			Console.WriteLine ("Articulo Id={0} Nombre={1} Precio={2} Categoria={3}",articulo.Id, articulo.Nombre,articulo.Precio,articulo.Categoria.Nombre);
			
			if(articulo.Categoria == null)
				Console.WriteLine("Categoria==null");
			else
				//Console.WriteLine("Categoria.id={0}",articulo.Categoria.Id);
				Console.WriteLine("Categoria.Nombre={0}",articulo.Categoria.Nombre);
		}
		
	}
	/*private void updateCategoria(ISessionFactory sessionFactory){
		//inicia la session
		ISession session = sessionFactory.OpenSession();
		try{
			Categoria categoria = (Categoria)session.Load(typeof(Categoria),2L);
			Console.WriteLine("Categoria Id={0} Nombre={1}",categoria.Id,categoria.Nombre);
			categoria.Nombre = DateTime.Now.ToString ();
			session.SaveOrUpdate(categoria);
			session.Flush ();
		}finally{
		   session.Close ();
		}
	}
	private void insertCategoria(ISessionFactory sessionFactory){
		ISession session = sessionFactory.OpenSession ();
		try{
			Categoria categoria = new Categoria ();
			categoria.Nombre = "Nueva "+ DateTime.Now.ToString ();
			session.SaveOrUpdate(categoria);
			session.Flush ();
		}finally{
		    session.Close ();
		}
	}*/
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
