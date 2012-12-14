using System;

namespace PArticulo
{
	public partial class ArticuloAdd : Gtk.Window
	{
		public ArticuloAdd (long id) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			dbConnection = ApplicationContext.Instance.DbConnection;

			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = string.Format ("select * from articulo where id={0}",id);
			
			IDataReader dataReader = dbCommand.ExecuteReader ();
			dataReader.Read ();
			
			entryNombre.Text = (string)dataReader["nombre"];
			spinButtonPrecio.Value = Convert.ToDouble( (decimal)dataReader["precio"] );
			
			dataReader.Close ();
			
			saveAction.Activated += delegate {
				Console.WriteLine("saveAction.Activated");
				
				IDbCommand dbInsertCommand = dbConnection.CreateCommand ();
				dbInsertCommand.CommandText = "insert into articulo (nombre, precio) values (nombre=:nombre, precio=:precio)";
				
				DbCommandExtensions.AddParameter (dbInsertCommand, "nombre", entryNombre.Text);
				DbCommandExtensions.AddParameter (dbInsertCommand, "precio", Convert.ToDecimal (spinButtonPrecio.Value ));
				
	
				dbInsertCommand.ExecuteNonQuery ();
				
				Destroy ();
			};
		}
	}
}

