using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	[SerializeField] private Vector2 mapRange = new Vector2(5.625f, 10f);
	[SerializeField] private float padding = 1f;
	[SerializeField] private List<Roll> roll = new List<Roll>();
	[SerializeField] public Roll ro { get; set; }
	[SerializeField] public GameObject gf { get; set; }
	[SerializeField] public Roll ss;
	
	[SerializeField]
	public List<Roll> GameRoll { get; set; }

	[System.Serializable] public class Roll
	{
		public float time;
		public GameObject enemy;
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
