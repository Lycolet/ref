using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//LinerBase:Barrage<>
//
//
//
//
//

[System.Serializable]
public class BaseBarrage : MonoBehaviour {

	[SerializeField] public int time;
	[SerializeField] public Sprite bulletImage;
	[SerializeField] protected bool startFromEnemy;
	[SerializeField] protected Vector2 startPoint;
	[SerializeField] protected bool aimForPlayer;
	[SerializeField] protected float shotAngle;
	[SerializeField] protected float speed;

	//呼び出し用
	public virtual IEnumerator Shot()
	{

		yield break;
	}
}
