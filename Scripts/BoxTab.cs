using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class BoxTab : MonoBehaviour {

	// Used to determine if this current tab is active
	public bool tabState;
	// This is private because I'm only going to utilize it inside methods
	private string baseResourcePath;
	// Path to this particular prefab
	private string gridElementPath;
	// Path to the folder prefab
	private string gridFolderPath;
	// This is only not a static value right now for the sake of testing. This refers to the Box itself.
	public GameObject theBox;

	public List<GameObject> boxChildren;

	public static BoxTab instance;

	// TEST
	private Button thisButton;
	private Color defaultNormalColor;

	public string modifier;

	private void Awake()
	{
		baseResourcePath = Application.dataPath + "/Resources/";
		gridElementPath = "UI Prefabs/GridElement";
		gridFolderPath = "UI Prefabs/GridFolder";
		thisButton = this.GetComponent<Button>();
		defaultNormalColor = thisButton.colors.normalColor;
	}

	public void CategoryClicked( string pathModifier )
	{
		instance = this;
		modifier = pathModifier;

		HighlightCategory();
		ClearBox();
		PopulateBox( pathModifier );
		AddPlusButton( pathModifier );
	}

	// Highlights the current selected box category tab
	private void HighlightCategory()
	{
		thisButton.image.color = thisButton.colors.pressedColor;
	}

	// Dulls the current category.
	// This is called from other categories to ensure the user knows which tab is currently active
	public void DullCategory()
	{
		thisButton.image.color = defaultNormalColor;
	}

	private bool DirectoryContainsFolder( FileInfo[] directoryFiles )
	{

		foreach( FileInfo file in directoryFiles )
		{
			if ( ExtensionIsOnlyMeta( file.Name ) )
				return true;
		}

		// REMOVE THIS WHEN YOU'RE FINISHED WRITING
		return false;
	}

	private bool ExtensionIsOnlyMeta( string fileName )
	{
		// This folder contains .prefab, .prefab.meta, and .meta files
		if ( fileName.Substring( fileName.IndexOf('.') ) == ".meta" )
			return true;
		else
			return false;
	}

	public void PopulateBox( string pathModifier )
	{
		// Making the path for the Directory to load
		string path = baseResourcePath + pathModifier;
		// Load in directory
		DirectoryInfo directory = new DirectoryInfo( path );
		// Get a list of files in the directory
		FileInfo[] info = directory.GetFiles();
		// Iterate through every file in the directory

	// New stuff

		// Determine if this folder contains a folder.
		if ( DirectoryContainsFolder( info ) )
		{
			Dictionary< string, GameObject > gridFolders = new Dictionary< string, GameObject >();
			// This variable exists as a fail-safe in the case there is only one folder, to know which string to access in the dictionary
			string single = null;

			// Gets a list of the folder paths and the folder names
			foreach( FileInfo file in info )
			{
				// Only look at the folders
				if ( ExtensionIsOnlyMeta( file.Name ) )
				{
					// Get the folderName
					string folderName = Path.GetFileNameWithoutExtension( file.Name );
					single = folderName;
					GameObject folderPrefab = Instantiate( (GameObject)Resources.Load( gridFolderPath ) );

					// Get the script, which is attached to the first child of the GameObjct
					GEFolderClick script = folderPrefab.transform.GetChild( 0 ).GetComponent<GEFolderClick>();
					// Set the folder script values
					script.folderName = folderName;
					script.folderPath = file.FullName;
					// Set the Text value
					folderPrefab.transform.GetChild(1).GetComponent<Text>().text = folderName;

					// Add the folder to the dictionary
					gridFolders.Add( folderName, folderPrefab );
				}
			}

			// If there is more than one folder within this folder
			if ( gridFolders.Count > 1 )
			{
				// Get a list of the folderNames
				List<string> folderNames = new List<string>();
				foreach( string gridFolderName in gridFolders.Keys )
				{
					folderNames.Add( gridFolderName );
				}
				// Sort the folder names, if there is more than one
				// Since they are strings this is automatically handled alphabetically
				folderNames.Sort();
				// Put the folders in the correct order
				foreach( string folderName in folderNames )
				{
					gridFolders[ folderName ].transform.SetParent( theBox.transform );
				}
			}
			else if ( gridFolders.Count == 1 ) {
				gridFolders[ single ].transform.SetParent( theBox.transform );
			}
			// We want to make sure NOT to take any action if the count of the dictionary is 0

			// And then populate the box like normal
			PopulateBoxWithPrefabs( info, pathModifier );
		} else
		{
			PopulateBoxWithPrefabs( info, pathModifier );
		}



	}

	private void PopulateBoxWithPrefabs( FileInfo[] directoryFiles, string pathModifier )
	{
		foreach ( FileInfo file in directoryFiles )
		{
			// There are .meta files when dealing with prefabs. It's important to ignore them as they are not the actual objects that I am looking for.
			if ( Path.GetExtension( file.Name ) == ".prefab" )
			{
				// Get the name of the file, no extension
				string fileName = Path.GetFileNameWithoutExtension( file.Name );
				// Make a gridElement to modify
				GameObject gridElement = Instantiate( (GameObject)Resources.Load(gridElementPath) );
				// Give it the correct parent
				gridElement.transform.SetParent( theBox.transform );
				// Get an instance of the prefab at that particular moment
				GameObject fileObj = (GameObject)Resources.Load( pathModifier + "/" + fileName );

				switch( modifier )
				{
				case "Players":
					Player playerPreab = fileObj.GetComponent<Player>();
					SetupGridElement( gridElement, playerPreab.playerSprite, playerPreab.playerName );
					Player gridPlayer =  gridElement.AddComponent<Player>();
					gridPlayer.SetPlayerValues( playerPreab );
					break;
				case "Enemies":
					Enemy enemyPrefab = fileObj.GetComponent<Enemy>();
					SetupGridElement( gridElement, enemyPrefab.enemySprite, enemyPrefab.enemyName );
					Enemy gridEnemy =  gridElement.AddComponent<Enemy>();					
					gridEnemy.SetEnemyValues( enemyPrefab );
					break;
				case "NPCs":
					NPC NPCPrefab = fileObj.GetComponent<NPC>();
					SetupGridElement( gridElement, NPCPrefab.NPCSprite, NPCPrefab.NPCName );
					// TODO: When NPC is designed, make sure to update this to attach the correct script
					break;
				case "Tiles":
					Tile tilePrefab = fileObj.GetComponent<Tile>();
					SetupGridElement( gridElement, tilePrefab.tileSprite, null );
					break;
				default:
					break;
				}
				
				//				if ( pathModifier == "Players" )
				//				{
				//					Player playerPreab = fileObj.GetComponent<Player>();
				//					SetupGridElement( gridElement, playerPreab.playerSprite, playerPreab.playerName );
				//				}
				// TODO: Stop the user from spamming clicking on the button to keep spawning more and more of the children. Add a check to stop that.
				
			}
		}// end foreach
	}

	public void AddPlusButton( string pathModifier )
	{
		// based on the pathModifier, make the resource path to the particular plus button I want
		string resourcePath = "UI Prefabs/Plus";
		resourcePath += pathModifier;
		resourcePath += "Button"; 

		// Load that plus button in to the same hierarchy that the gridElements were just added to moments ago
		GameObject plusButton = Instantiate( (GameObject)Resources.Load( resourcePath ) );
		plusButton.transform.SetParent( theBox.transform );
	}

	private void SetupGridElement( GameObject gridElement, Sprite image, string text )
	{
		// I go through an array of the Images because the first Image returned otherwise is the one corresponding to GridElement itself, as opposed to it's child.
		Image[] gridImages = gridElement.GetComponentsInChildren<Image>();
		// There's a hard-coded 1 here because of how GridElement is designed, the second Image element is the one I'm looking for.
		gridImages[1].sprite = image;
		
		Text gridText = gridElement.GetComponentInChildren<Text>();
		gridText.text = text;
	}

	public void ClearBox()
	{
		foreach( Transform child in theBox.transform )
		{
			Destroy( child.gameObject );
		}
	}

	/**
	 *	This method is designed to add a plus button at the end of all the grid elements. It's designed to always be the last child in the list.
	 *
	 *	This plus button, when the user clicks on it, allows them to create a new version of the class that pathModifier determines.
	 *
	 *	It opens up a new UI window and displays all the necessary, editable fields related to the class at hand.
	 * */
	

}

























