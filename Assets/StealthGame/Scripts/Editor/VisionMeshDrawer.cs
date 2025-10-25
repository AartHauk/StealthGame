using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// TODO Implement Vision mesh to make a serializable mesh so that the mesh is not made on runtime
[CustomPropertyDrawer(typeof(VisionMesh))]
public class VisionMeshDrawer : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);


		var mesh = property.FindPropertyRelative("mesh");
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


		EditorGUI.EndProperty();
	}
}
