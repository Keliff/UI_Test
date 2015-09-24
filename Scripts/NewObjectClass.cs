using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using UnityEngine.EventSystems;

public class NewObjectClass : MonoBehaviour {

	public Image imageProp;
	public Text spriteText;

	protected string folder;

	protected void WritePrefabToDisk( string refName, GameObject obj )
	{
		obj.name = refName;
		PrefabUtility.CreatePrefab( "Assets" + "/Resources/" + folder + "/" + refName + ".prefab", obj );
	}
		
	public void CancelButtonClick()
	{
		Destroy( this.gameObject );
	}

	/**
	 * This method is attached to the nameProp InputField.
	 * 
	 * Whenever the user is done entering a name into the field, it needs to be checked against the particular prefabs that are already stored of this class.
	 * 
	 * That means that it is necessary to look inside the prefabList and check if that string is inside of it
	 * */
	protected void CheckNameProp( InputField nameProp )
	{
		// Get a directory to load in
		string path = Application.dataPath + "/Resources/" + folder;
		// load files in of directory
		DirectoryInfo directory = new DirectoryInfo( path );
		// list of files
		FileInfo[] info = directory.GetFiles();
		// Iterate through the files of that directory
		foreach ( FileInfo file in info )
		{
			if ( Path.GetExtension( file.Name ) == ".prefab" )
			{
				if ( Path.GetFileNameWithoutExtension( file.Name ) == nameProp.text )
				{
					// error message
					EditorUtility.DisplayDialog( "Name conflict", "The name you chose for your new" + folder + "class already exists! Please choose another one.", "Okay" );
					// reset input
					nameProp.text = "";
				}
			}
		}
	}

	/**
	 * Checks to see whether or not the sprite chosen by the user already exists in the assets folder.
	 * 
	 * It checks by name, so the sprites aren't actually compared.
	 * */
	protected bool CheckSpriteProp( string fileName )
	{
		string path = Application.dataPath + "/Resources/Sprites";
		DirectoryInfo directory = new DirectoryInfo( path );
		FileInfo[] info = directory.GetFiles();
		
		foreach( FileInfo file in info )
		{
			if ( Path.GetFileNameWithoutExtension( file.Name ) == fileName )
				return true;
		}
		
		return false;
	}

	/**
	 * This method is attached to the new Sprite button, and allows a sprite for the player to be loaded in
	 * 
	 * Also checks whether or not the sprite is already stored in the assets folder
	 * 
	 * If so, it uses the one in the asset folder instead
	 * 
	 * Otherwise it saves the selected sprite INTO the asset folder and then uses it
	 * */
	public void SelectSprite()
	{
		string path = EditorUtility.OpenFilePanel( "New" + folder + "Sprite", "", "png" );
		
		//		string fileName = Path.GetFileName( path );
		string fileName = Path.GetFileNameWithoutExtension( path );
		
		if ( CheckSpriteProp( fileName ) )
		{
			// If the user says it's okay to use the sprite already stored there
			if (EditorUtility.DisplayDialog( "Sprite conflict!", "The sprite you selected shares a name of a sprite already stored in the Assets folder." +
			                                "Do you wish to use the sprite already stored in the folder?", "Yes", "No" ))
			{
				LoadSpriteClearText( fileName );
				return;
			} else // Then the user would need to re-name the image
			{
				EditorUtility.DisplayDialog( "Re-name", "Then please re-name your sprite and choose it again.", "Okay" );
				
				return;
			}
		}
		
		if ( path.Length != 0 )
		{
			// make byte array
			byte[] bytes = File.ReadAllBytes( path );
			// make texture of the correct size
			// TODO: THIS IS WHERE THE SPRITE WIDTH AND HEIGHT ARE MEASURED. CHANGE IN THE FUTURE
			Texture2D texture = new Texture2D( 16, 16 );
			// load texture in
			texture.LoadImage( bytes );
			// Convert texture to png
			byte[]png = texture.EncodeToPNG();
			// Save png to the assets folder
			File.WriteAllBytes( Application.dataPath + "/Resources/Sprites/" + fileName + ".png", png );
			// Reset the Asset database so it can be loaded into the UI
			AssetDatabase.Refresh();
			
			LoadSpriteClearText( fileName );

		}
	}

	/**
	 * I used this piece of code at two separate points of the script, so I put it in a little private method
	 * */
	protected void LoadSpriteClearText( string fileName )
	{
		imageProp.sprite = (Sprite)Resources.Load( "Sprites/" + fileName, typeof(Sprite) );
		spriteText.text = "";
	}

}































