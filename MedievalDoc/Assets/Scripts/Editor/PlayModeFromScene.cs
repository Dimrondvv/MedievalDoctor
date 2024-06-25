using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class PlayModeFromScene : EditorWindow
{
    [MenuItem("Play/PlayMe _%h")]
    public static void RunMainScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GlobalScene.unity");
        EditorApplication.isPlaying = true;
    }
}
