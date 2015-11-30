using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridElementClick : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GridElementClick instance;
//	public Canvas rightClickCanvas;

//	const string rightClickPath = "UI Prefabs/GridEle Right Click";
//	const string rightClickPath = "UI Prefabs/RightClickTest";
	const string rightClickPath = "UI Prefabs/Temp";

	// When this grid element is clicked on, update the information window with the information pertaining to the current prefab
	#region IPointerClickHandler implementation
	public void OnPointerClick ( PointerEventData eventData )
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log("Left click");

			UpdateInformationWindow();
		}
//		else if (eventData.button == PointerEventData.InputButton.Middle)
//			Debug.Log("Middle click");
		else if (eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log("Right click");

			instance = this;

			// Spawn a right click menu for the Grid Element
			SpawnRightClickMenu();
		}
	}
	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		Debug.Log("Drag of GridElement started");
//		throw new System.NotImplementedException ();
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		Debug.Log("Dragging GridElement");
//		throw new System.NotImplementedException ();
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		Debug.Log("Drag of GridElement has ended");
//		throw new System.NotImplementedException ();
	}

	#endregion

	public void UpdateInformationWindow()
	{
		// If no information window is open, create a new one
		if ( InformationWindow.instance == null )
		{
			Object prefab;
			string resourcePath = "UI Prefabs/Information Window";

			prefab = Resources.Load( resourcePath );

			Instantiate( prefab );
		}

		Player testPlayer = this.gameObject.transform.parent.gameObject.GetComponent<Player>();

		if ( testPlayer != null )
		{
			UpdatePlayerInformation( testPlayer );

			return;
		}

		Enemy testEnemy = this.gameObject.transform.parent.gameObject.GetComponent<Enemy>();

		if ( testEnemy != null )
		{
			UpdateEnemyInformation( testEnemy );

			return;
		}

		//TODO: When the NPC class is updated fill this out

		else {
			Debug.Log( "UpdateInformationWindow() was given a grid element with an attached script it didn't understand" );
			Debug.Log( "Or, there was an error in the attachment of a recognizable script" );
		}


	}

	public void SpawnRightClickMenu()
	{
		// Create a prefab GameObject and load the necessary data in
		GameObject rightClickPrefab;
		rightClickPrefab = (GameObject) Resources.Load( rightClickPath );
		// Get the RectTransform
		RectTransform test = rightClickPrefab.GetComponent<RectTransform>();
		// Spawn the prefab where the mouse button is, but adding half the width and subtracting half the height
		// This makes sure the upper left tip of the game object is right under the mouse position
		GameObject newObj = (GameObject)Instantiate( rightClickPrefab, new Vector3( Input.mousePosition.x + test.rect.width / 2, Input.mousePosition.y - test.rect.height / 2, Input.mousePosition.z ), Quaternion.identity );
		// Give this transform a default value
		Transform last = null;
		// Find the last child in the list of objects that are filling up the box
		// This ensures that the right click menu will be spawned OVER every other Grid Element in the Box. Otherwise, subsequent grid elements would be on top of this right click prefab.
		foreach( Transform child in this.transform.parent.parent )
		{
			last = child;
		}
		// Set the correct parent
		newObj.transform.SetParent( last );

	}

	private void UpdatePlayerInformation( Player player )
	{
		InformationWindow.instance.SetSpriteImage( player.playerSprite );
		InformationWindow.instance.SetClassText( "Player" );
		InformationWindow.instance.SetNameText( player.playerName );
		InformationWindow.instance.SetHealthText( player.health.ToString() );
		InformationWindow.instance.SetMoveText( player.move.ToString() );
	}

	private void UpdateEnemyInformation( Enemy enemy )
	{
		InformationWindow.instance.SetSpriteImage( enemy.enemySprite );
		InformationWindow.instance.SetClassText( "Enemy" );
		InformationWindow.instance.SetNameText( enemy.enemyName );
		InformationWindow.instance.SetHealthText( enemy.health.ToString() );
		InformationWindow.instance.SetMoveText( enemy.movement.ToString() );
	}

}













































