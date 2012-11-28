using Gtk;
using System;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		
		public string Nombre
		{
			get {entryNombre.Text = value;}
		}
		
		public double Precio{
			get {spinbuttonPrecio.Value = value;}
		}
		
		public long Categoria{
			set{ //Todo implementar 
				
			}
		}
	}
}

