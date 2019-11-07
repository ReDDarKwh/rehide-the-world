
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SceneManager myScript = (SceneManager)target;
        if(GUILayout.Button("LAUNCH"))
        {
            myScript.LaunchThatMissileBaby(myScript.lands.Where(x=> !x.isDestroyed));
        }
    }
}
