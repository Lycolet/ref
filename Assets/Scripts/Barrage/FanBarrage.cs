using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBarrage : BaseBarrage {

	[SerializeField] int test;

	// Use this for initialization
	void Start () {
		
	}

	public override IEnumerator Shot()
	{
		StartCoroutine(base.Shot());
		yield break;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
