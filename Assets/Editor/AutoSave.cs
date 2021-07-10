using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;

public class AutoSave : EditorWindow
{

    private bool autoSaveScene = true;
    private bool showMessage = true;
    private bool isStarted = false;
    private int intervalScene;
    private DateTime lastSaveTimeScene;
    private string projectPath;
    private string scenePath;

    [MenuItem("Tools/AutoSave")]

    public void OnEnable()
    {
        lastSaveTimeScene = DateTime.Now;
        projectPath = Application.dataPath;
    }
    static void Init()
    {
        AutoSave saveWindow = (AutoSave)EditorWindow.GetWindow(typeof(AutoSave));
        saveWindow.Show();
    }

    float time = 0;
    void Update()
    {      
        if ( autoSaveScene )
        {       
            time += Time.fixedDeltaTime;
           // Debug.Log(time);
            if ( time > 120.0f ) {              
                time = 0;
                saveScene();              
            }           
        }
        else
        {
            isStarted = false;
        }

    }

    void saveScene()
    {
        scenePath = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), scenePath);
        lastSaveTimeScene = DateTime.Now;
        isStarted = true;
        if ( showMessage )
        {
            Debug.Log("自动保存路径: " + scenePath + " 保存时间： " + lastSaveTimeScene);
        }
        AutoSave repaintSaveWindow = (AutoSave)EditorWindow.GetWindow(typeof(AutoSave));
        repaintSaveWindow.Repaint();
    }
}