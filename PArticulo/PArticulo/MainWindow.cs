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
		
		
		
		
		/*treeView.AppendColumn("Identificador",new CellRendererText(),"text",0);
		treeView.AppendColumn("Nombre",new CellRendererText(),"text",1);
		treeView.AppendColumn("Precio",new CellRendererText(),"text",2);
		treeView.AppendColumn("Categoría",new CellRendererText(),"text",3);*/
		
		//Type[]types=new Type[dataReader.FieldCount];
		/*List<Type> types=new List<Type>();
		
		for(int i=0;i<IDataReader.FieldCount;i++){
			treeView.AppendColumn.(dataReader.GetName(1),new CellRendererText(),"text",i);
			types.Add (typesof(string));
			//types[i]=typeof(string);
			                
		}*/
		
		
		
		//crear modelo
		//ListStore listStore=new ListStore(typeof(string),typeof(string),typeof(string),typeof(string));
		/*Type[]types=TypeExtensiones.GetTypes(typeof(string),dataReader.FieldCount);
		ListStrore listStore=new ListStrore(types);
		
		treeView.Model=listStore;
		ListStoreExtensions.Fill(listStore,dataReader);*/
		
		//array genérico-dinámico
		//ListStore listStore=new ListStore(types.ToArray());
		
		
		/*while(IDataReader.Read())
		{	//variable local,cuando se termine el ciclo,ya no se utiliza
			// array estático string[]values=new string[dataReader.FieldCount];
			List<string>values=new List<string>();
			for(int i=0;i<dataReader.FieldCount;i++)
				//values[i]=dataReader[i].ToString();
				values.Add(dataReader[i].ToString());
			
			listStore.AppendValues(values,ToArray());
			listStore.AppendValues(dataReader[0],ToString(),dataReader[1],ToString(),
			                       dataReader[2],ToString(),IDataReader[3],ToString());
			listStore.AppendValues("1","Nombre 1","1,5","1");
		}*/
		
		
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
		
		//leer de la base de datos los datos
		IDbCommand dbCommand = dbConnection.CreateCommand();
		//dbCommand.CommandText = "select * from articulo where id=:id";
		dbCommand.CommandText = string.Format ("select * from articulo where id=id",id);
		IDbDataParameter dbDataParameter = dbCommand.CreateParameter ();
		dbDataParameter.ParameterName = "id";
		dbCommand.Parameters.Add (dbDataParameter);
		dbDataParameter.Value = id;
		
		IDataReader dataReader = dbCommand.ExecuteReader ();//se ejecuta el parametro
		dataReader.Read ();//leo el registro, sóla una vez
		
		
		ArticuloView articuloView = new ArticuloView();
		articuloView.Nombre =(string)dataReader["nombre"];
		articuloView.Precio = double.Parse(dataReader["precio"].ToString());
		
		articuloView.Show ();
		
		dataReader.Close ();
	}
	private long getSelectedId()
	{	TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		
		ListStore listStore = (ListStore)TreeView.Model;
		object id = (listStore.GetValue(treeIter, 0).ToString());
		return long.Parse(id);
	}

	
}
