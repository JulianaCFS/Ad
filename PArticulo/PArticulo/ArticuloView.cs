using Gtk;
using Serpis.Ad;
using System.Data;
using System;
namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{	
		public ArticuloView (long id) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			dbConnection = ApplicationContext.Instance.DbConnection;
			
			if(id == 0)
			{
				nuevo();	
			}
			else
			{
				editar();
			}

			
				
			
		}
	
		
	}
}

