using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class NewTileClass : NewObjectClass {

	private Tile tileProp;
	private GameObject tilePropObj;
	
	private void Awake()
	{
		tilePropObj = new GameObject();
		tileProp = tilePropObj.AddComponent<Tile>();
		
		this.folder = "Tiles";
	}
	
	private void OnDestroy()
	{
		Destroy( tilePropObj );
	}
	
	/**
	 * Take all the information from the public UI fields above, and give the necessary details to enemyProp
	 * */
	private void SaveNewTileClass()
	{
		tileProp.tileSprite = this.imageProp.sprite;
	}
	
	public void SaveButtonClick()
	{
		SaveNewTileClass();
		this.WritePrefabToDisk( tileProp.tileSprite.name, tilePropObj );
		BoxTab.instance.CategoryClicked( this.folder );
		Destroy( this.gameObject );
	}

}
