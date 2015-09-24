using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class NewNPCClass : NewObjectClass {

	public InputField nameProp;
	public InputField healthProp;
	public InputField moveProp;
	
	private NPC NPCProp;
	private GameObject NPCPropObj;

	private void Awake()
	{
		NPCPropObj = new GameObject();
		NPCProp = NPCPropObj.AddComponent<NPC>();
		
		nameProp.onEndEdit.AddListener(
			delegate{ CheckNameProp( nameProp ); }
		);
		
		this.folder = "NPCs";
	}

	private void OnDestroy()
	{
		Destroy( NPCPropObj );
	}
	
	/**
	 * Take all the information from the public UI fields above, and give the necessary details to enemyProp
	 * */
	private void SaveNewNPCClass()
	{
		NPCProp.NPCName = nameProp.text;
		NPCProp.health = int.Parse( healthProp.text );
		NPCProp.movement = int.Parse( moveProp.text );
		NPCProp.NPCSprite = this.imageProp.sprite;
	}
	
	public void SaveButtonClick()
	{
		SaveNewNPCClass();
		this.WritePrefabToDisk( NPCProp.NPCName, NPCPropObj );
		BoxTab.instance.CategoryClicked( this.folder );
		Destroy(this.gameObject);
	}

}
