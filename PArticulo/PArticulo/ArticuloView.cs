using Gtk;
using Serpis.Ad;
using System.Data;
using System;
namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private IDbConnection dbConnection;
		
		public ArticuloView (long id) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			dbConnection = ApplicationContext.Instance.DbConnection;
			
			if (id == 0) //nuevo
				nuevo();
			else
				editar(id);
			
			categoria();
			
		}
		
		private void nuevo() {
			//inicializo los controles que quiera
			entryNombre.Text = "Pon el nombre";
			spinButtonPrecio.Value = 1;
			//valor nullable
			/*object categoriaData = dataReader["categoria"];
			long? categoria = null;
			if(!(categoriaData is DBNull))
					categoria = (long)categoriaData;
			
			comboBoxCategoria.SelectedValue();*/
			categoria();
			saveAction.Activated += delegate {
				Console.WriteLine("saveAction.Activated");
				
				IDbCommand dbCommand = dbConnection.CreateCommand ();
				dbCommand.CommandText = "insert into articulo (nombre, precio) values (:nombre, :precio)";
				
				DbCommandExtensions.AddParameter (dbCommand, "nombre", entryNombre.Text);
				DbCommandExtensions.AddParameter (dbCommand, "precio", Convert.ToDecimal (spinButtonPrecio.Value ));
				
				
				dbCommand.ExecuteNonQuery ();
				
				Destroy ();
			};
		}
		
		private void editar(long id) {
			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = string.Format ("select * from articulo where id={0}", id);
			
			IDataReader dataReader = dbCommand.ExecuteReader ();
			dataReader.Read ();
			
			entryNombre.Text = (string)dataReader["nombre"];
			spinButtonPrecio.Value = Convert.ToDouble( (decimal)dataReader["precio"] );
			
			dataReader.Close ();
			
			saveAction.Activated += delegate {
				Console.WriteLine("saveAction.Activated");
				
				IDbCommand dbUpdateCommand = dbConnection.CreateCommand ();
				dbUpdateCommand.CommandText = "update articulo set nombre=:nombre, precio=:precio where id=:id";
				
				DbCommandExtensions.AddParameter (dbUpdateCommand, "nombre", entryNombre.Text);
				DbCommandExtensions.AddParameter (dbUpdateCommand, "precio", Convert.ToDecimal (spinButtonPrecio.Value ));
				DbCommandExtensions.AddParameter (dbUpdateCommand, "id", id);
	
				dbUpdateCommand.ExecuteNonQuery ();
				
				Destroy ();
			};
		}
		private void categoria()
		{
			int index ;
			
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = 
			"select nombre from categoria";
			
			
			IDataReader dataReader = dbCommand.ExecuteReader ();
			while(dataReader.Read ()) {
				
				for ( index = 0; index < dataReader.FieldCount; index++)
				{	Console.WriteLine("index"+index);
						comboBoxCategoria.InsertText(index+1,dataReader[index].ToString ());
					Console.WriteLine("dataReader"+dataReader);
				}
				
				
			}
			//comboBoxCategoria.InsertText(index+1,"");
			comboBoxCategoria.Active=2;
			
			
		}
		
	}
}
