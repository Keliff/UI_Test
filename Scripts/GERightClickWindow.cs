using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class GERightClickWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public static GERightClickWindow instance;
	private bool over;

	private void Awake()
	{
		if ( instance == null )
			instance = this;
		else
		{
			Destroy( instance.gameObject );
			instance = this;
		}
			
	}

	private void Update()
	{
		// If the user clicks and they are not over the UI object
		// Or if the user right clicks anywhere
		// Destroy the right click
		if ( Input.GetMouseButtonDown(0) && !over ) // left click
		{
			DeleteRightClick();
		} else if ( Input.GetMouseButtonDown(1) ) // right click
		{
			DeleteRightClick();
		}
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		over = true;
//		Debug.Log("over");
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		over = false;
//		Debug.Log("exit");
	}

	#endregion
	
	public void CountInScene()
	{
//		Debug.Log("Clicked count");
	}

	public void HighlightInScene()
	{

	}

	public void DeleteInScene()
	{

	}

	public void DeleteRightClick()
	{
//		Debug.Log("Delete Right Click ran");
		instance = null;
		Destroy(this.gameObject);
	}

//	public static void RightClickClear()
//	{
//		if ( instance != null )
//		{
//			Destroy( instance.gameObject );
//			instance = null;
//		}
//	}
}
