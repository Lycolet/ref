using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	[System.Serializable] private struct Move   //キャラの移動を記述
	{
		[SerializeField] int time;
		[SerializeField] bool relative;
		[SerializeField] Vector2 destination;
		[SerializeField] int span;
	}
	[SerializeField] List<Move> moves;

	List<BaseBarrage> tasks = new List<BaseBarrage>();

	// Use this for initialization
	void Start () {
		tasks.AddRange(gameObject.GetComponents<BaseBarrage>());
		Debug.Log(tasks.Count);
		List<BaseBarrage> execution = tasks.FindAll(x => x.time == 2);
		foreach(BaseBarrage x in execution)
		{
			StartCoroutine(x.Shot());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
