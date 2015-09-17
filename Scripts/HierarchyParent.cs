using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HierarchyParent : MonoBehaviour {

	public bool listState;
	public List<GameObject> hierChildren;
	public List<HierarchyChild> hierChildrenScripts;
	public Image arrow;

	private void Awake()
	{
		listState = true;
	}

	/**
	 * This is for attaching to the button that this script is attached to.
	 * 
	 * It flips the state of listState, and then either reveals or hides the children depending on the current
	 * value of the state.
	 * 
	 * It changes the image depending on the value of the list state as well.
	 * 
	 * And then it highlights all the children.
	 * */
	public void ClickOnParent()
	{
		listState = !listState;

		SwitchChildrenState( listState );
		SwitchImage( listState );
		if ( listState )
			HighlightAllChildren();
	}
// Begin Helper methods for ClickOnParent()
	/**
	 * This method either reveals or hides the children depening on the value of state.
	 * */
	private void SwitchChildrenState( bool state )
	{
		foreach( GameObject obj in hierChildren )
			obj.SetActive( state );
	}

	/**
	 * This method switches the Image associated with the button depening on the value of state.
	 * */
	private void SwitchImage( bool state )
	{
		if ( state )
		{
			arrow.sprite = Resources.Load<Sprite>( "Sprites/Down" );
		}
		else
			// Set to the default sprite
			arrow.sprite = Resources.Load<Sprite>( "Sprites/Right" );
	}
	/**
	 * Highlights all the children on the map, iterating through the list of scripts and running the approparite method.
	 * */
	private void HighlightAllChildren()
	{
		foreach( HierarchyChild child in hierChildrenScripts )
			child.HighlightChild();
	}
// End Helper methods for ClickOnParent()

	/**	
	 * This method exists as a helper to populate the list of 'Children' associated with this HierarchyParnet
	 * They are stored as GameObjects and not as any type of UI object because I want to be able to call the
	 * 	SetActive method on them. The actual children themselves will handle all the necessary non GameObject
	 * 	related funtions.
	 * */
	public void AddObjectToChildren( GameObject child, HierarchyChild script )
	{
		hierChildren.Add( child );
		hierChildrenScripts.Add( script );
	}

	/**
	 * This method exists to remove a particular set of elements from the two Lists of this script.
	 * */
	public void RemoveObjectFromChildren( GameObject child, HierarchyChild script )
	{
		if ( hierChildren.Contains( child ) )
			hierChildren.Remove(child);
		else if ( hierChildrenScripts.Contains( script ) )
			hierChildrenScripts.Remove( script );
	}

}






























