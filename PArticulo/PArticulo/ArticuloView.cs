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
			get {return entryNombre.Text;}
			set {EntryNombre.Text = value;}
		}
		
		public decimal Precio{
			get {return Convert.ToDecimal(spinbuttonPrecio.Value);}
			set{spinbuttonPrecio.Value = Convert.ToDouble(value);}
		}
		
		public long Categoria
		{
			set{ //Todo implementar 
				
			}
		}
		public Gtk.Action SaveAction{
			get{}
		}
	}
}

