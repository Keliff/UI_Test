using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class NewPlayerClass : NewObjectClass {

	public InputField nameProp;
	public InputField healthProp;
	public InputField moveProp;

	private Player playerProp;
	private GameObject playerPropObj;

	private void Awake()
	{
		playerPropObj = new GameObject();
		playerProp = playerPropObj.AddComponent<Player>();

		nameProp.onEndEdit.AddListener(
			delegate{ CheckNameProp( nameProp ); }
		);

		this.folder = "Players";
	}

	private void OnDestroy()
	{
		Destroy( playerPropObj );
	}

	/**
	 * Take all the information from the public UI fields above, and give the necessary details to playerProp
	 * */
	private void SaveNewPlayerClass()
	{
		playerProp.playerName = nameProp.text;
		playerProp.health = int.Parse( healthProp.text );
		playerProp.move = int.Parse( moveProp.text );
		playerProp.playerSprite = this.imageProp.sprite;
	}

	public void SaveButtonClick()
	{
		SaveNewPlayerClass();
		this.WritePrefabToDisk( playerProp.playerName, playerPropObj );
		BoxTab.instance.CategoryClicked( this.folder );
		Destroy(this.gameObject);
	}

	/**
	 * Inside of this class, I want to have all the particular information stored to be able to save a new player class
	 * 
	 * This means, on creation of the script, I wish to create a new player, and be able to store that information inside of that new player, and save a prefab of them onto the disk
	 * 
	 * 
	 * 
	 * 
	 * */


}



































