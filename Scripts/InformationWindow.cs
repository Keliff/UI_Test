using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InformationWindow : MonoBehaviour {

	public static InformationWindow instance;

	private Text  classText;
	private Text  nameText;
	private Text  healthText;
	private Text  moveText;
	private Image spriteImage;

	private void Awake()
	{
		if ( instance == null )
			instance = this;

		FindAndSetChildren();
	}

	private void FindAndSetChildren()
	{
		foreach( Transform child in this.transform )
		{
			if ( child.name == "Sprite" )
				spriteImage = child.GetComponent<Image>();
			else if ( child.name == "Class" )
			{
				classText = child.FindChild("Value").GetComponent<Text>();
			} else if ( child.name == "Name" )
			{
				nameText = child.FindChild("Value").GetComponent<Text>();
			} else if ( child.name == "Health" )
			{
				healthText = child.FindChild("Value").GetComponent<Text>();
			} else if ( child.name == "Movement" )
			{
				moveText = child.FindChild("Value").GetComponent<Text>();
			}
		}
	}

	public void CloseClick()
	{
		instance = null;

		Destroy( this.gameObject.transform.parent.parent.gameObject );
	}

	public void SetClassText( string text )
	{
		this.classText.text = text;
	}

	public void SetNameText( string text )
	{
		this.nameText.text = text;
	}

	public void SetHealthText( string text )
	{
		this.healthText.text = text;
	}

	public void SetMoveText( string text )
	{
		this.moveText.text = text;
	}

	public void SetSpriteImage( Sprite sprite )
	{
		this.spriteImage.overrideSprite = sprite;
	}

}





















