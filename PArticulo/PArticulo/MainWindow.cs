using Gtk;
using Npgsql;
using PArticulo;
using Serpis.Ad;
using System;
using System.Collections.Generic;
using System.Data;


public partial class MainWindow: Gtk.Window
{	private IDbConnection dbConnection;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=Juliana";
		ApplicationContext.Instance.DbConnection = new NpgsqlConnection(connectionString);
		dbConnection = ApplicationContext.Instance.DbConnection;
		dbConnection.Open ();
		
		IDbCommand dbCommand = ApplicationContext.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = 
			"select a.id, a.nombre, a.precio, c.nombre as categoria " +
			"from articulo a left join categoria c " +
			"on a.categoria = c.id";
		
		IDataReader dataReader = dbCommand.ExecuteReader ();
		
		TreeViewExtensions.Fill (treeView, dataReader);
		dataReader.Close ();
		
		dataReader = dbCommand.ExecuteReader ();
		TreeViewExtensions.Fill (treeView, dataReader);
		dataReader.Close ();
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		dbConnection.Close ();

		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnClearActionActivated (object sender, System.EventArgs e)
	{
		ListStore listStore = (ListStore)treeView.Model;
		listStore.Clear ();
	}

	protected void OnEditActionActivated (object sender, System.EventArgs e)
	{
		long id = getSelectedId();
		ArticuloView articuloView = new ArticuloView( id );
		articuloView.Show ();
	}
	
	private long getSelectedId() {
		TreeIter treeIter;
		treeView.Selection.GetSelected(out treeIter);
		
		ListStore listStore = (ListStore)treeView.Model;
		return long.Parse (listStore.GetValue (treeIter, 0).ToString ()); 
	}

	protected void OnAddActionActivated (object sender, System.EventArgs e)
	{
		
		long id = 0;
		ArticuloAdd articuloAdd = new ArticuloAdd( id );
		articuloAdd.Show ();
	}
	public void nuevo()
	{
		IDbCommand dbInsertCommand = dbConnection.CreateCommand ();
				dbInsertCommand.CommandText = "insert into articulo (nombre, precio) value (:nombre, :precio)";
				
				DbCommandExtensions.AddParameter (dbInsertCommand, "nombre", entryNombre.Text);
				DbCommandExtensions.AddParameter (dbInsertCommand, "precio", Convert.ToDecimal (spinButtonPrecio.Value ));
				
	
				dbUpdateCommand.ExecuteNonQuery ();
		
	}
	public void editar()
	{
			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = string.Format ("select * from articulo where id={0}",id);
			
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
				DbCommandExtensions.AddParameter (dbUpdateCommand, "id",id);
	
				dbUpdateCommand.ExecuteNonQuery ();
				
				Destroy ();
			};	
		
	}

	
}