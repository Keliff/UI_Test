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

}
