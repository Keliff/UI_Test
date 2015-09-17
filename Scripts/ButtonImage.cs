using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour {

	private Image thisImage;
	public Sprite defaultSprite;
	public Sprite clickSprite;

	private void Awake()
	{
		thisImage = this.GetComponent<Image>();
		//defaultSprite = thisImage.sprite;
		//clickSprite = (Sprite)Resources.Load( Application.dataPath + "Right" );
	}

	public void SwitchImage()
	{
		if ( thisImage.sprite == defaultSprite )
			thisImage.sprite = clickSprite;
		else
			thisImage.sprite = defaultSprite;
	}
}
