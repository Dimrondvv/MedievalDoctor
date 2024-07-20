using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ToolCreator : EditorWindow
{
    private string toolName = "";
    private GameObject tool;
    private GameObject toolModel;

    [MenuItem("Tools/ Tool Creator")]
    public static void ShowWindow() {
        GetWindow(typeof(ToolCreator));
    }

    private void OnGUI() {
        GUILayout.Label("Create New Tool", EditorStyles.boldLabel);

        toolName = EditorGUILayout.TextField("Tool name", toolName);
        toolModel = EditorGUILayout.ObjectField("Tool Model", toolModel, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Create Tool")) {
            CreateTool();
        }
    }

    private void CreateTool() {
        if (toolModel == null) { Debug.LogError("Error: Please assign a model to tool"); return; }
        if (toolName == string.Empty) { Debug.LogError("Error: Please assign a name to tool"); return; }

        tool = new GameObject();

        tool.layer = 10;
        tool.name = toolName;
        GameObject newToolModel = Instantiate(toolModel);

        newToolModel.transform.SetParent(tool.transform);

        tool.AddComponent<MeshCollider>();
        tool.AddComponent<InteractionTool>();
        tool.AddComponent<ToolPickup>();

        MeshFilter modelMesh = (MeshFilter)newToolModel.GetComponent("MeshFilter");
        tool.GetComponent<MeshCollider>().sharedMesh = modelMesh.sharedMesh;

        





        if (!Directory.Exists("Assets/Prefabs/Tools"))
            AssetDatabase.CreateFolder("Assets/Prefabs", "Tools");

        string localPath = "Assets/Prefabs/Tools/" + tool.name + ".prefab";

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        bool prefabSuccess;
        PrefabUtility.SaveAsPrefabAsset(tool, localPath, out prefabSuccess);
        if (prefabSuccess == true) {
            DestroyImmediate(tool);
            Debug.Log("Prefab was saved successfully");
        } else
            Debug.Log("Prefab failed to save" + prefabSuccess);

    }
}
