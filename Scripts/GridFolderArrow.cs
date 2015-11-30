using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GridFolderArrow : MonoBehaviour, IPointerClickHandler {

	// This is the path to the folder directly above whichever folder spawned this object
	public string upFolder;

	private bool oneClick;
	private float delay;
	private float timerForDoubleClick;

	private void Awake()
	{
		delay = 0.25f;
		upFolder = null;
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
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
			if ( upFolder == null )
			{
				Debug.Log("GridFolderArrow's upFolder was never defined and cannot spawn the correct folder's information.");
				return;
			}

			//			Debug.Log("Go Up In the Folder Hierarchy");

			// TEST
			BoxTab.instance.ClearBox();
			BoxTab.instance.PopulateBox( upFolder );
			BoxTab.instance.AddPlusButton( BoxTab.instance.modifier );

			// If these two values are equal, there's no need to add an upbutton
			if ( upFolder != BoxTab.instance.modifier )
			{
				string oneLessFolder = this.RemoveFolderLevel( upFolder );

				if ( oneLessFolder == null )
					GEFolderClick.instance.AddUpArrow( "" );
				else
					GEFolderClick.instance.AddUpArrow( upFolder );
			}
//			GEFolderClick.instance.AddUpArrow(  );

			// Check to see if upFolder is the same as the modifier hidden under BoxTab. If so, you can just re-spawn the base folder again.
//			if ( upFolder == BoxTab.instance.modifier )
//			{
//				// Setting it to "" so that it doesn't change anything via concatenation
//				BoxTab.instance.lastFolder = "";
//
//				BoxTab.instance.ClearBox();
//				BoxTab.instance.PopulateBox( BoxTab.instance.modifier );
//				BoxTab.instance.AddPlusButton( BoxTab.instance.modifier );
//			}
//			// Otherwise a more specific folder will need to be spawned
//			else
//			{
//				string oneLessFolder = this.RemoveFolderLevel( GEFolderClick.instance.subFolderList );
//				// Clear everything in the box
//				BoxTab.instance.ClearBox();
//				// Populate the box with the correct folder's information
//				BoxTab.instance.PopulateBox( BoxTab.instance.modifier + oneLessFolder + "/" + upFolder );
//				// Add the necessary Plus button
//				BoxTab.instance.AddPlusButton( BoxTab.instance.modifier );
//				// I need to add an Up Arrow here as well because the folder is nested, and therefore needs another way back up
//				GEFolderClick.instance.AddUpArrow( oneLessFolder );
//			}

//			BoxTab.instance.ClearBox();
//			BoxTab.instance.PopulateBox( BoxTab.instance.modifier + subFolderList + "/" + folderName );
//			BoxTab.instance.AddPlusButton( BoxTab.instance.modifier );

			// This object has served it's purpose, and can die with dignity
			Destroy(this);	
		}
	}

	#endregion

	private string RemoveFolderLevel( string folderPath )
	{
//		string temp = folderPath.Substring( 0, folderPath.LastIndexOf('/') );
		int temp = folderPath.LastIndexOf('/');
		if ( temp == -1 )
		{
			return null;
		}
		return folderPath.Substring( 0, folderPath.LastIndexOf('/') );
	}
	
}


























