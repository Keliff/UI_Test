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
	// This is only not a static value right now for the sake of testing. This refers to the Box itself.
	public GameObject theBox;

	public List<GameObject> boxChildren;

	public static BoxTab instance;

	// TEST
	private Button thisButton;
	private Color defaultNormalColor;

	private void Awake()
	{
		//TODO: Ensure that this path is correct
		baseResourcePath = Application.dataPath + "/Resources/";
		gridElementPath = "UI Prefabs/GridElement";
		thisButton = this.GetComponent<Button>();
		defaultNormalColor = thisButton.colors.normalColor;
	}

	public void CategoryClicked( string pathModifier )
	{
		instance = this;
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

	private void PopulateBox( string pathModifier )
	{
		// Making the path for the Directory to load
		string path = baseResourcePath + pathModifier;
		// Load in directory
		DirectoryInfo directory = new DirectoryInfo( path );
		// Get a list of files in the directory
		FileInfo[] info = directory.GetFiles();
		// Iterate through every file in the directory
		foreach ( FileInfo file in info )
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

				switch( pathModifier )
				{
					case "Players":
						Player playerPreab = fileObj.GetComponent<Player>();
						SetupGridElement( gridElement, playerPreab.playerSprite, playerPreab.playerName );
						break;
					case "Enemies":
						Enemy enemyPrefab = fileObj.GetComponent<Enemy>();
						SetupGridElement( gridElement, enemyPrefab.enemySprite, enemyPrefab.enemyName );
						break;
					case "NPCs":
						NPC NPCPrefab = fileObj.GetComponent<NPC>();
						SetupGridElement( gridElement, NPCPrefab.NPCSprite, NPCPrefab.NPCName );
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
				// TODO: Add in the other tabCases here
				// TODO: Stop the user from spamming clicking on the button to keep spawning more and more of the children. Add a check to stop that.

			}
		}// end foreach

	}

	private void AddPlusButton( string pathModifier )
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

























