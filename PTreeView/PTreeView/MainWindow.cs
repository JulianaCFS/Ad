using System;
using Gtk;

/**public partial class MainWindow: Gtk.Window
{	
		
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//Columna del treeView
		treeView.AppendColumn("Column uno",new CellRendererText(),"text",0);
		treeView.AppendColumn("Column dos",new CellRendererText(),"text",1);
		
		//definición de las columnas de ListStore,en este caso hay dos columnas
		ListStore listStore=new ListStore(typeof(string),typeof(string));
		                                  
		//modelo treeView(el model es para decir al treeView,donde está los elementos)
		treeView.Model=listStore;
		
		//añade valores con AppenValues(valores de la columna)
		listStore.AppendValues("clave uno","valor uno");
		listStore.AppendValues("clave dos","valor dos");
		
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}**/

public partial class MainWindow: Gtk.Window
{
	/*public MainWindows();base(Gtk.windowsType,topLevel1)
	 * {
	 * 
	 * 
	 * 
	 * 
	 * */
	
 
	public MainWindow(): base(Gtk.WindowType.Toplevel)
	{
		Build();
		//seleccion multiple
		treeView.Selection.Mode = SelectionMode.Multiple;
		
		
		treeView.AppendColumn("Columna uno",new CellRendererText (),"text", 0);
		treeView.AppendColumn("Columna dos",new CellRendererText (),"text", 1);
		
		ListStore listStore = new ListStore(typeof(string),typeof(string ));
		
		treeView.Model = listStore;
		
		listStore.AppendValues ("Clave uno", "valor uno");
		listStore.AppendValues ("Clave dos", "valor dos");
		listStore.AppendValues ("Clave tres", "valor tres");
		listStore.AppendValues ("Clave cuatro", "valor cuatro");
		
		treeView.Selection.Changed += delegate
		{
			int count = TreeView.Selection.CountSelectedRows ();
			Console.WriteLine ("treeView.Selection.Changed CountSelectedRows={0}",count);
			
			treeView.Selection.SelectedForeanch(delegate(TreeModel model, TreeParth parth, TreeIter iter)
			{
				object value =ListStore.GetValue(TreeIter, 0);
				Console.WriteLine ("value={0}",value);
			});
			//TreeIter treeIter;
			
			/*if (treeView.Selection.GetSelected(out treeIter)){//item seleccionado
				object value =ListStore.GetValue(TreeIter, 0);
				Console.WriteLine ("value={0}",value);*/
		};
	}
		                                                        
		/*// Create a Window
		Gtk.Window window = new Gtk.Window ("TreeView Example");
		window.SetSizeRequest (500,200);
 
		// Create our TreeView
		Gtk.TreeView tree = new Gtk.TreeView ();
 
		// Add our tree to the window
		window.Add (tree);
 
		// Create a column for the artist name
		Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
		artistColumn.Title = "Artist";
 
		// Create a column for the song title
		Gtk.TreeViewColumn songColumn = new Gtk.TreeViewColumn ();
		songColumn.Title = "Song Title";
 
		// Add the columns to the TreeView
		tree.AppendColumn (artistColumn);
		tree.AppendColumn (songColumn);
 
		// Create a model that will hold two strings - Artist Name and Song Title
		Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (string), typeof (string));
 
 
		// Assign the model to the TreeView
		tree.Model = musicListStore;
 
		// Show the window and everything on it
		window.ShowAll ();*/
			
	protected void OnDeleteEvent (object sender , DeleteEventArgs a)
	{
			Application.Quit();
			a.Retval = true;
		
	}
	

}
