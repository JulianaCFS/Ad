using System;
using Gtk;

using NHibernate.Cfg;
using Serpis.Ad;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Configuration configuration = new Configuration();
		configuration.Configure ();
		configuration.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
		configuration.AddAssembly(typeof(Categoria).Assembly);
		//new SchemaExport(configuration).Execute(true, false, false, true);
		
		//para acceder a la base de datos
		ISessionFactory sessionFactory = configuration.BuildSessionFactory();
		
		//updateCategoria (sessionFactory);
		
		//insertCategoria (sessionFactory);
		
		
		
		sessionFactory.Close ();
		
	}
	private void loadArticulo(ISessionFactory sessionFactory){
		using (ISession session = sessionFactory.OpenSession()){
			Articulo articulo = (Articulo)session.Load (typeof(Articulo),2L);
			Console.WriteLine ("Articulo Id={0} Nombre={1}",articulo.Id, articulo.Nombre,articulo.Precio);
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
