using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorSceneLoader : EditorWindow
{
    public static List<string> scenePaths = new List<string>();
    private Vector2 scrollPos;

    [MenuItem("Window/SceneLoader")]
    public static void ShowWindow()
    {
        var window = GetWindow<EditorSceneLoader>();
        window.titleContent = new GUIContent("SceneLoader");
        GetScenes();
        window.Show();
    }

    private void OnEnable()
    {
        GetScenes();
    }

    public static void GetScenes()
    {
        scenePaths.Clear();
        string[] guids = AssetDatabase.FindAssets("t:scene", new[] { "Assets/Scenes/" });

        foreach (string guid in guids)
        {
            scenePaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button(EditorGUIUtility.IconContent("Refresh"), GUILayout.ExpandWidth(true)))
            GetScenes();

        scrollPos = GUILayout.BeginScrollView(scrollPos, false, false, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        List<EditorBuildSettingsScene> editorBuildSettingsScenes = EditorBuildSettings.scenes.ToList();

        for (int i = 0; i < scenePaths.Count; i++)
        {
            GUILayout.BeginHorizontal();
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePaths[i]);
            var sceneItem = editorBuildSettingsScenes.Find((scene) => scene.path == scenePaths[i]);
            if(sceneItem == null)
            {
                if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Plus"), GUILayout.Width(30)))
                {
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePaths[i], true));
                    EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
                }
            }
            else
            {
                sceneItem.enabled = EditorGUILayout.Toggle(sceneItem.enabled, GUILayout.Width(30));
                EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            }
            if(SceneManager.GetActiveScene().path == scenePaths[i])
            {
                GUI.color = Color.green;
            }
            if(GUILayout.Button(sceneAsset.name))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(scenePaths[i]);
            }
            GUI.color = Color.white;
            if(GUILayout.Button(EditorGUIUtility.IconContent("ViewToolZoom"), GUILayout.Width(30)))
            {
                EditorGUIUtility.PingObject(sceneAsset);
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
}
