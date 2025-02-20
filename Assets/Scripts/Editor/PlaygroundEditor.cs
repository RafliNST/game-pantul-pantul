using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(playground))]
public class PlaygroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        playground _playGround = (playground)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Create Playground"))
        {
            _playGround.DrawForGui();
        }
    }
}
