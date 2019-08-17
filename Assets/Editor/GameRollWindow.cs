using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions.Comparers;

public class GameRollWindow : EditorWindow
{
    private int size;

    private GameObject template;
    private GameObject enemyParent;
    private Vector2 scroll_pos;
    
    private GameObject enemy;//一時
    private Vector2 pos;
    private float time;

    private Enemy clipboard;

    /// <summary>
    /// 敵を管理するためのクラス
    /// </summary>
    public class Enemy
    {
        public Enemy(GameObject _obj, float _time)
        {
            obj = _obj;
            time = _time;
        }
        public GameObject obj;
        public float time;
    }

    /// <summary>
    /// 敵を管理するためのリスト
    /// </summary>
    public List<Enemy> enemies = new List<Enemy>();
    
    /// <summary>
    /// グループのリスト
    /// </summary>
    public Dictionary<string, Transform> group = new Dictionary<string, Transform>();
    
    ///メニューのツールにウィンドウを追加
    [MenuItem("Tools/GameRoll")]
    private static void Open()
    {
        GetWindow<GameRollWindow>("GameRoll");
    }
    
    /// <summary>
    /// ボタンが押されたら呼び出し,敵の追加
    /// </summary>
    /// <param name="initTime">召喚される時間</param>
    /// <param name="initPos">召喚される座標</param>
    /// <param name="initGroup">属するグループ名</param>
    private void Add_enemy(float initTime, Vector2 initPos, string initGroup = "")
    {
        //グループ名が入力されなかった場合自動的にungrouped
        initGroup = initGroup == "" ? "ungrouped" : initGroup;
        
        //親がなければ追加。
        //enemyParent ->全ての敵の親
        //┗groupParent ->複数の敵をまとめる
        //  ┗enemy ->敵
        if (!group.Keys.Contains(initGroup))
        {
            Transform groupParent = new GameObject(initGroup).GetComponent<Transform>();
            //そもそもグループの親がなければ追加
            if ((enemyParent = GameObject.Find("Enemies")) == null)
            {
                enemyParent = new GameObject();
            }
            groupParent.parent = enemyParent.GetComponent<Transform>();
            //グループ親の辞書に登録
            group.Add(initGroup, groupParent);
        }

        //敵を生成、リストに追加
        var addEnemy = new Enemy(Instantiate(template, initPos, Quaternion.identity, group[initGroup]), initTime);
        enemies.Add(addEnemy);
        //timeで昇順ソート
        enemies.Sort((a,b) => a.time > b.time ? 1 : -1);
    }

    private void OnGUI()
    {
//        
//        EditorGUILayout.BeginHorizontal();
//        pos = EditorGUILayout.Vector2Field("position", pos);
//        EditorGUILayout.EndHorizontal();
//        
        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.LabelField("time", GUILayout.Width(40));
//        time = EditorGUILayout.FloatField(time, GUILayout.Width(40));
        if (GUILayout.Button("Add", GUILayout.Width(80)))
        {
            enemies.Add(new Enemy(null, 0));
            size++;
        }
        if (GUILayout.Button("Paste", GUILayout.Width(80)))
        {
            
        }
        EditorGUILayout.EndHorizontal();
        
        scroll_pos = EditorGUILayout.BeginScrollView(scroll_pos);
        for (int i = 0; i < size; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("time", GUILayout.Width(40));
            enemies[i].time = EditorGUILayout.FloatField(enemies[i].time, GUILayout.Width(40));
            EditorGUILayout.LabelField("enemy", GUILayout.Width(40));
            enemies[i].obj = (GameObject) EditorGUILayout.ObjectField(enemies[i].obj, typeof(GameObject), true);
            if (GUILayout.Button("削除", GUILayout.Width(30)))
            {
                size--;
                enemies.Remove(enemies[i]);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }
}