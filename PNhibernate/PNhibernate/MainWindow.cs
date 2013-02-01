using System;
using Gtk;
using NHibernate.Cfg;
using Serpis.Ad;
using NHibernate;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Configuration configuration = new Configuration();
		configuration.Configure();
		
		configuration.AddAssembly(typeof(Categoria).Assembly);
		
		new SchemaExport(configuration).Execute(true, false, false, true);
		
		//para acceder a la base de datos
		ISessionFactory sessionFactory = configuration.BuildSessionFactory();
		
		//abre la session
		ISession session = sessionFactory.OpenSession();
		Categoria categoria = (Categoria)session.Load(typeof(Categoria),2L);
		Console.WriteLine("Categoria Id={0} Nombre={1}",categoria.Id,categoria.Nombre);
		categoria.Nombre = DataTime.Now.ToString ();
		session.SaveOrUpdate(categoria);
		session.Close ();
		
		sessionFactory.Close ();
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
