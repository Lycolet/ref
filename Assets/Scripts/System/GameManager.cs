using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
    [SerializeField] private Vector2 mapRange = new Vector2(5.625f, 10f);
    [SerializeField] private float padding = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 引数の座標が画面内に存在するか確認する。画面内であればtrue。
    /// </summary>
    /// <param name="judgePos"></param>
    /// <returns></returns>
    public bool InRange (Vector2 judgePos)
    {
        if(Mathf.Abs(judgePos.y + (mapRange.y / 2)) < Mathf.Abs((mapRange.y / 2) + padding))
        {
            if(Mathf.Abs(judgePos.x) < Mathf.Abs(mapRange.x / 2) + padding)
            {
                return true;
            }
        }
        return false;
    }
}
