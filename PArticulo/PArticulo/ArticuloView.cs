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

			if (id == 0) { //nuevo
				nuevo ();
			}
			else
				editar(id);
		}
	
		private void nuevo() {
			//inicializo los controles que quiera
			categoria(0);
			entryNombre.Text = "Pon el nombre";
			spinButtonPrecio.Value = 1;

			saveAction.Activated += delegate {
				Console.WriteLine("saveAction.Activated");

				int iden_categoria = comboBoxCategoria.Active;
				
				IDbCommand dbCommand = dbConnection.CreateCommand ();
				if(iden_categoria != 0)
					dbCommand.CommandText = "insert into articulo (nombre, precio, categoria) values (:nombre, :precio, :categoria)";
				else
					dbCommand.CommandText = "insert into articulo (nombre, precio) values (:nombre, :precio)";

				DbCommandExtensions.AddParameter (dbCommand, "nombre", entryNombre.Text);
				DbCommandExtensions.AddParameter (dbCommand, "precio", Convert.ToDecimal (spinButtonPrecio.Value ));

				if(iden_categoria != 0)
				{

					DbCommandExtensions.AddParameter (dbCommand, "categoria", iden_categoria);
				}

				dbCommand.ExecuteNonQuery ();
			
				Destroy ();
			


			};
		}
		
		private void editar(long id) {
			int iden;
			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = string.Format ("select * from articulo where id={0}", id);
			
			IDataReader dataReader = dbCommand.ExecuteReader ();
			dataReader.Read ();

			entryNombre.Text = (string)dataReader["nombre"];
			spinButtonPrecio.Value = Convert.ToDouble( (decimal)dataReader["precio"] );

			bool esnull = Convert.IsDBNull(dataReader["categoria"]);

			if(esnull == true)
				iden = 0;
			else
				iden = Convert.ToInt32(dataReader["categoria"]);

			dataReader.Close ();

			categoria (iden);
	

			saveAction.Activated += delegate {
				Console.WriteLine("saveAction.Activated");

				int iden_categoria = comboBoxCategoria.Active;
				Console.WriteLine("el iden activado es: {0}", iden_categoria);

				IDbCommand dbUpdateCommand = dbConnection.CreateCommand ();
				if(iden_categoria!=0)
					dbUpdateCommand.CommandText = "update articulo set nombre=:nombre, precio=:precio, categoria=:categoria where id=:id";
				else
					dbUpdateCommand.CommandText = "update articulo set nombre=:nombre, precio=:precio where id=:id";

				DbCommandExtensions.AddParameter (dbUpdateCommand, "nombre", entryNombre.Text);
				DbCommandExtensions.AddParameter (dbUpdateCommand, "precio", Convert.ToDecimal (spinButtonPrecio.Value ));
				DbCommandExtensions.AddParameter (dbUpdateCommand, "id", id);

				if(iden_categoria!=0)
					DbCommandExtensions.AddParameter (dbUpdateCommand, "categoria", iden_categoria);


	
				dbUpdateCommand.ExecuteNonQuery ();
				
				Destroy ();

			};
		}
		private void categoria(int index_activado)
		{
			int cont=0;
			int index = 0;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = "select nombre from categoria";

			IDataReader dataReader = dbCommand.ExecuteReader ();

			comboBoxCategoria.InsertText(cont,"Seleccione una categoria");
			cont++;

			do
			{
				while (dataReader.Read())
				{
					comboBoxCategoria.InsertText(cont,dataReader[index].ToString ());
					cont++;
				}
				index++;
				
			} while (dataReader.NextResult());

			dataReader.Close ();
			
			comboBoxCategoria.Active= index_activado;
			
		}
	}
}
