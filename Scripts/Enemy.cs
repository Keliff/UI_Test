using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public int health;
	public int movement;
	public Sprite enemySprite;
	public string enemyName;

	public void SetEnemyValues( Enemy other )
	{
		this.health = other.health;
		this.movement = other.movement;
		this.enemySprite = other.enemySprite;
		this.enemyName = other.enemyName;
	}
}
