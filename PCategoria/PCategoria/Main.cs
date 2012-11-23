using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		fillComboBox();
	}
	private void fillComboBox()
	{	
		CellRenderer cellRenderer = new CellRendererText();
		comboBox.PackStart(cellRenderer, false);//expand=false
		comboBox.AddAttribute (cellRenderer, "text", 1);//el 1 kiere decir la columna 1 del ListStore
		
		ListStore listStore = new ListStore(typeof(string), typeof(string));
		
		comboBox.Model =listStore;
		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;password=Juliana";
		IDbConnection dbConnection = new NpgsqlConnection(connectionString);
		dbConnection.Open();
		
		IDbCommand dbCommand = dbConnection.CreateCommand();
		dbCommand.commandText = "select id, nombre from categoria";
		
		IDataReader dataReader = dbCommand.ExecuteReader();
		
		while(dataReader.Read()){
			listStore.AppendValues (dataReader["id"].ToString (), dataReader["nombre"].ToString());
			dataReader.Close();//por cada conexion, solo pode mantener un dataReader abierto
		}
		
		dataReader.
		
		dbConnection.Close();
	}	
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
