using Gtk;
using Npgsql;
using Serpis.Ad;
using System;
using System.Collections.Generic;
using System.Data;

using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		
		
		
		
		string connectionString="Server=localhost;Database=dbprueba;User Id=dbprueba;password=Juliana";
		IDbConnection dbConnection=new NpgsqlConnection();
		dbConnection.Open();
		
		IDbCommand dbCommand= dbConnection.CreateCommand();
		dbCommand.CommandText=
			"select a.id, a.nombre, a.precio, c.nombre as categoria" +
			"from articulo a left join categoria c" +
			"on a.categoria = c.id";
		
		
		
		
		
		
		
		IDataReader dataReader=dbCommand.ExecuteReader();
		
		TreeViewExtensions.Fill(treeView,dataReader);
		
		ListStore listStore=(ListStore)treeView.Model;
		
		dataReader.close();
		
		
		                                                                     
		                                        
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{	dbConnection.Close ();
		
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnClearActionActivated(object sender,System.EventArgs a)
	{
		ListStore listStore=(ListStore)TreeView.Model;
		ListStore.Clear();
	}
	//metodo que se activaraá cdo damos un clic en la accion
	protected void OnEditActionActivated (object sender, System.EventArgs e)
	{
		long id = getSelectedId();//coge el id seleccionado
		
		//para ver que ha cogido el id
		Console.WriteLine("id={0}",id);
		
		//leer de la base de datos los datos, utilizando parametro
		IDbCommand dbCommand = dbConnection.CreateCommand();
		dbUpdateCommand.CommandText = "update articulo set nombre=:nombre, precio=:precio, id=:id";
		
		dbDataParameter.Value = id;
		
		IDataReader dataReader = dbCommand.ExecuteReader ();//se ejecuta el parametro
		dataReader.Read ();//leo el registro, sóla una vez
		
		
		ArticuloView articuloView = new ArticuloView();
		articuloView.Nombre =(string)dataReader["nombre"];
		articuloView.Precio = double.Parse(dataReader["precio"].ToString());
		
		articuloView.Show ();
		
		dataReader.Close ();
		
		//accion tiene un evento Activated
		
		articuloView.SaveAction.Activated += delegate{//codigo q se ejecute cdo hagan el clic 
			Console.WriteLine("articuloView.SaveAction.Actived");
			
		IDbCommand dbCommand = dbConnection.CreateCommand();
		dbUpdateCommand.CommandText = "update articulo set nombre=:nombre, precio=:precio, id=:id";
		IDbDataParameter nombreParameter = dbUpdateCommand.CreateParameter ();
		IDbDataParameter precioParameter = dbUpdateCommand.CreateParameter ();
		IDbDataParameter idParameter = dbUpdateCommand.CreateParameter ();
		nombreParameter.ParameterName = "nombre";
		precioParameter.ParameterName = "precio";
		idParameter.ParameterName = "id";
		dbUpdateCommand.Parameters.Add (nombreParameter);
		dbUpdateCommand.Parameters.Add (precioParameter);
		dbUpdateCommand.Parameters.Add (idParameter);
			
		nombreParameter.Value = articuloView.Nombre;
		precioParameter.Value = articuloView.Precio;
		idParameter.Value = id;
			
			dbCommand.ExecuteNonQuery();//exception que no este controlada
			
			articuloView.Destroy ();
		};
	}
	private long getSelectedId()
	{	TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		
		ListStore listStore = (ListStore)TreeView.Model;
		object id = (listStore.GetValue(treeIter, 0).ToString());
		return long.Parse(id);
	}

	
}
