using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int health;
	public int move;
	public int initiative;
	public Sprite playerSprite;
	public string playerName;

	public Player( string name, int health, int move, Sprite sprite )
	{
		this.playerName = name;
		this.health = health;
		this.move = move;
		this.playerSprite = sprite;
	}

	// Used to set the current Player's values equal to another Player's
	public void SetPlayerValues( Player other )
	{
		this.health = other.health;
		this.move = other.move;
		this.initiative = other.initiative;
		this.playerSprite = other.playerSprite;
		this.playerName = other.playerName;
	}

}
