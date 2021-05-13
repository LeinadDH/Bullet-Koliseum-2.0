using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(GunData))]
//public class GunData_Editor : Editor
//{
//    SerializedProperty _gunIcon;
//    SerializedProperty _gunSprite;
//    //SerializedProperty _bulletCapacity;
//    //SerializedProperty _bulletShotTime;
//    //SerializedProperty _bulletReloadTime;
//    //SerializedProperty _bulletInitialSpeed;
//    //SerializedProperty _constantSpeed;
//    //SerializedProperty _useGravity;
//    //SerializedProperty _damage;
//    //SerializedProperty _despawnDelay;

//    private void OnEnable()
//    {
//        _gunIcon = serializedObject.FindProperty("gunIcon");
//        _gunSprite = serializedObject.FindProperty("gunSprite");
//        //_bulletCapacity = serializedObject.FindProperty("bulletCapacity");
//        //_bulletShotTime = serializedObject.FindProperty("bulletShotTime");
//        //_bulletReloadTime = serializedObject.FindProperty("bulletReloadTime");
//        //_bulletInitialSpeed = serializedObject.FindProperty("bulletInitialSpeed");
//        //_constantSpeed = serializedObject.FindProperty("constantSpeed");
//        //_useGravity = serializedObject.FindProperty("useGravity");
//        //_damage = serializedObject.FindProperty("damage");
//        //_despawnDelay = serializedObject.FindProperty("despawnDelay");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        _gunIcon.objectReferenceValue = EditorGUILayout.ObjectField("Gun Icon", _gunIcon.objectReferenceValue, typeof(Texture2D), false);
//        _gunSprite.objectReferenceValue = EditorGUILayout.ObjectField("Gun Sprite", _gunSprite.objectReferenceValue, typeof(Texture2D), false);
        
//        serializedObject.ApplyModifiedProperties();
//    }

//    public override void OnPreviewGUI(Rect r, GUIStyle background)
//    {
//        base.OnPreviewGUI(r, background);
//    }
//}
