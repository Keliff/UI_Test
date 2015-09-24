using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class NewEnemyClass : NewObjectClass {

	public InputField nameProp;
	public InputField healthProp;
	public InputField moveProp;
	
	private Enemy enemyProp;
	private GameObject enemyPropObj;

	private void Awake()
	{
		enemyPropObj = new GameObject();
		enemyProp = enemyPropObj.AddComponent<Enemy>();
		
		nameProp.onEndEdit.AddListener(
			delegate{ CheckNameProp( nameProp ); }
		);
		
		this.folder = "Enemies";
	}

	private void OnDestroy()
	{
		Destroy( enemyPropObj );
	}

	/**
	 * Take all the information from the public UI fields above, and give the necessary details to enemyProp
	 * */
	private void SaveNewEnemyClass()
	{
		enemyProp.enemyName = nameProp.text;
		enemyProp.health = int.Parse( healthProp.text );
		enemyProp.movement = int.Parse( moveProp.text );
		enemyProp.enemySprite = this.imageProp.sprite;
	}

	public void SaveButtonClick()
	{
		SaveNewEnemyClass();
		this.WritePrefabToDisk( enemyProp.enemyName, enemyPropObj );
		BoxTab.instance.CategoryClicked( this.folder );
		Destroy(this.gameObject);
	}

}



































