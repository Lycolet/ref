using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RollWindow : EditorWindow
{
    public class Enemy // 敵のクラス
    {
        public Enemy(GameObject _obj, float _time)
        {
            obj = _obj;
            time = _time;
        }
        public GameObject obj;
        public float time;
    }

    private int size; //行の数
    private Vector2 scroll_pos; //スクロール表示の現在位置を格納
    
    public List<Enemy> enemies = new List<Enemy>(); //敵を管理するリスト


    ///メニューのツールタブにウィンドウを追加
    [MenuItem("Tools/TestWindow")]
    private static void Open()
    {
        GetWindow<RollWindow>("Test");
    }


    private void OnGUI() //基本的にこの関数内でGUIの内容を記述する
    {
        EditorGUILayout.BeginHorizontal(); //ここから水平に表示
        if (GUILayout.Button("Add", GUILayout.Width(80))) //ボタンを表示
        {
            enemies.Add(new Enemy(null, 0)); //押されると行を追加
            size++;
        }
        EditorGUILayout.EndHorizontal(); //ここまで水平表示
        
        scroll_pos = EditorGUILayout.BeginScrollView(scroll_pos); //ここからスクロール表示
        for (int i = 0; i < size; i++)
        {
            EditorGUILayout.BeginHorizontal(); //ここから水平表示

            // ここでEnemyクラスの中身を表示
            EditorGUILayout.LabelField("time", GUILayout.Width(40));
            enemies[i].time = EditorGUILayout.FloatField(enemies[i].time, GUILayout.Width(40));
            EditorGUILayout.LabelField("enemy", GUILayout.Width(40));
            enemies[i].obj = (GameObject) EditorGUILayout.ObjectField(enemies[i].obj, typeof(GameObject), true);
            
            if (GUILayout.Button("削除", GUILayout.Width(30))) //ボタンを表示
            {
                size--;
                enemies.Remove(enemies[i]); //押された行を削除
            }

            EditorGUILayout.EndHorizontal(); //ここまで水平表示
        }
        EditorGUILayout.EndScrollView(); //ここまでスクロール表示
    }

}
