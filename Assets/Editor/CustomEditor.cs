using System.CodeDom;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(GameManager.Roll))]
public class CustomEditor : Editor
{
    private bool folding = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gm = target as GameManager;

        EditorGUILayout.BeginHorizontal();
        gm.ss.time = EditorGUILayout.FloatField(gm.ss.time);
        EditorGUILayout.EndHorizontal();
        
    }
}
