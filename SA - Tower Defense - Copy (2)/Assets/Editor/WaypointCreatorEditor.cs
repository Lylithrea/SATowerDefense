using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointCreator))]
public class WaypointCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WaypointCreator waypoint = (WaypointCreator)target;
        if(GUILayout.Button("Create Waypoints"))
        {
            waypoint.createWaypoints();
        }
    }
}
