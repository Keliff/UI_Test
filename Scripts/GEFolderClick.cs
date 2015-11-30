using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GEFolderClick : MonoBehaviour, IPointerClickHandler {

	public static GEFolderClick instance;

	public string folderPath;
	public string folderName;

	//TEST
	public string subFolderList;

	private bool oneClick;
	private float delay;
	private float timerForDoubleClick;
	private string upArrowPath;

	private void Awake()
	{
		delay = 0.25f;
		upArrowPath = "UI Prefabs/UpArrowFolder";
	}

	// When this grid element is clicked on, update the information window with the information pertaining to the current prefab
	#region IPointerClickHandler implementation
	public void OnPointerClick ( PointerEventData eventData )
	{
		if ( oneClick )
		{
			if ( (Time.time - timerForDoubleClick) > delay )
				oneClick = false;
		}

		if (eventData.button == PointerEventData.InputButton.Left && !oneClick)
		{
			oneClick = true;
			timerForDoubleClick = Time.time;
//			Debug.Log("Left click");
		} else
		{
//			Debug.Log("Open folder");

			// I need to save the current directory, to be able to re-fill the box and go back to it, by hitting an up arrow
			instance = this;
			
			subFolderList = this.GetSubFolderList( subFolderList );

//			string test = BoxTab.instance.modifier + subFolderList;

			BoxTab.instance.ClearBox();
			BoxTab.instance.PopulateBox( BoxTab.instance.modifier + subFolderList );
			BoxTab.instance.AddPlusButton( BoxTab.instance.modifier );

			this.AddUpArrow();

			oneClick = false;

		}
	}
	#endregion

	private string GetSubFolderList( string path )
	{
		// We're going to analyze BoxTab.instance.modifier, and we're going to search the folderPath for this string
		path = folderPath.Substring( folderPath.IndexOf( BoxTab.instance.modifier ) );
		// We're going to get the substring of that location, and search for the first / after it.
		int firstSlash = path.IndexOf('/');
		// Get the substring from there to the end
		path = path.Substring( firstSlash );
		// Delete anything after the '.'
		return path.Remove( path.IndexOf('.') );
		// And then we get a substring of everything after the last '/'
//		return path.Remove( path.LastIndexOf('/') );
		// This leaves a blank string for a first level folder and a full list for any subfolder
	}

	private void AddUpArrow()
	{
		// We're going to instantiate a new gameobject using the up arrow prefab
		GameObject upArrow = (GameObject)Instantiate( Resources.Load(  upArrowPath) );
		// Set the parent of this new object
	// DOUBLE CHECK THAT THIS PARENT IS CORRECT
		upArrow.transform.SetParent( BoxTab.instance.theBox.transform );
		// Get the script, which is attached to the first child
		GridFolderArrow script = upArrow.transform.GetChild( 0 ).GetComponent<GridFolderArrow>();
		// Set the above folder
		if ( subFolderList == "" )
		{
			script.upFolder = BoxTab.instance.modifier;
		} else
		{
//			string test = BoxTab.instance.modifier + this.RemoveFolderLevel( subFolderList );
			script.upFolder = BoxTab.instance.modifier + this.RemoveFolderLevel( subFolderList );
		}

		// Set the name of the UpArrow
		Text scriptText = upArrow.GetComponentInChildren<Text>();
		scriptText.text = "Previous";

		return;
	}

	public void AddUpArrow( string path )
	{
		// We're going to instantiate a new gameobject using the up arrow prefab
		GameObject upArrow = (GameObject)Instantiate( Resources.Load(  upArrowPath) );
		// Set the parent of this new object
	// DOUBLE CHECK THAT THIS PARENT IS CORRECT
		upArrow.transform.SetParent( BoxTab.instance.theBox.transform );
		// Get the script, which is attached to the first child
		GridFolderArrow script = upArrow.transform.GetChild( 0 ).GetComponent<GridFolderArrow>();
		// Set the above folder
		if ( path == "" )
		{
			script.upFolder = BoxTab.instance.modifier;
		} else
		{
//			string test = this.RemoveFolderLevel( path );
			script.upFolder = this.RemoveFolderLevel( path );
		}
		
		// Set the name of the UpArrow
		Text scriptText = upArrow.GetComponentInChildren<Text>();
		scriptText.text = "Previous";
		
		return;
	}

	private string ModifierFolderList( string path )
	{
		int lastSlash = path.LastIndexOf('/');
		// If there is no '/' in the path, that means we should just return BoxTab.instance.modifier
		if ( lastSlash == -1 )
			return BoxTab.instance.modifier;
		string temp = path.Substring( 0, lastSlash );
		lastSlash = temp.LastIndexOf('/');
		return temp.Substring( lastSlash + 1 );
	}

	private string RemoveFolderLevel( string folderPath )
	{
//		string temp = folderPath.Substring( 0, folderPath.LastIndexOf('/') );
		return folderPath.Substring( 0, folderPath.LastIndexOf('/') );
	}
}

































