using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ManipulatorJointControl))]
public class ManipulatorJointControlEditor : Editor
{
    private static GUIStyle buttonStyle;
    private ManipulatorJointControl manipulatorJointControl;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (buttonStyle == null)
            buttonStyle = new GUIStyle(EditorStyles.miniButtonRight) { fixedWidth = 75 };
        
        manipulatorJointControl = (ManipulatorJointControl)target;
        
        GUILayout.Label("All Urdf Joints", EditorStyles.boldLabel);
        DisplaySettingsToggle(new GUIContent
                ("Add/remove joint states", 
                    "Adds/removes a Joint State Reader on each joint."),
            manipulatorJointControl.SetSubscribeJointStates);


    }

    private delegate void SettingsHandler(bool enable);
    private static void DisplaySettingsToggle(GUIContent label, SettingsHandler handler)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(label);
        if (GUILayout.Button("Enable", buttonStyle))
            handler(true);
        if (GUILayout.Button("Disable", buttonStyle))
            handler(false);
        EditorGUILayout.EndHorizontal();
    }
}
