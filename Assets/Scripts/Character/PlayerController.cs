using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController> {
	
	[SerializeField] private int initialHealth;
	[SerializeField] private float neutralSpeed;
	[SerializeField] private float slowSpeed;

	[HideInInspector] public int health;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void OnTriggerEnter2D(Collider2D other){

		Debug.Log("teteetetete");
	}

    //#######################################################################################

    /// <summary>
    /// 自機の移動
    /// </summary>
	private void Move()
	{
		Vector2 moveDir = new Vector2();
		if (Input.GetKey(KeyCode.W))
		{
			moveDir.y++;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			moveDir.y--;
		}
		if (Input.GetKey(KeyCode.D))
		{
			moveDir.x++;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			moveDir.x--;
		}
		if (moveDir != new Vector2(0, 0))
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				transform.Translate(slowSpeed * moveDir);
			}
			else
			{
				transform.Translate(neutralSpeed * moveDir);
			}
		}
	}
}
