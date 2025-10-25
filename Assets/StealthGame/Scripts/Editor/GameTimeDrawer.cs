using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GameTime))]
public class GameTimeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var content = property.FindPropertyRelative("time");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var hourRect = new Rect(position.x, position.y, 30, position.height);
        var minuteRect = new Rect(position.x + 35, position.y, 30, position.height);
        var secondRect = new Rect(position.x + 70, position.y, 30, position.height);

        int hours = EditorGUI.IntField(hourRect, content.intValue / 3600);
        int minutes = EditorGUI.IntField(minuteRect, (content.intValue / 60) % 60);
        int seconds = EditorGUI.IntField(secondRect, content.intValue % 60);

        content.intValue = (hours * 3600 + minutes * 60 + seconds) % GameTime.MAX_TIME_SECONDS;

        EditorGUI.EndProperty();
    }
}
