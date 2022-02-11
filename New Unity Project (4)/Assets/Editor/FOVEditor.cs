using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRad);
        Handles.color = Color.blue;
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false) * 2;
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false) * 2;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRad * 2);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRad);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRad);

        Handles.color = Color.green;
        if (fow.visibleTargets != null)
            Handles.DrawLine(fow.transform.position, fow.visibleTargets.position);
    }
}
