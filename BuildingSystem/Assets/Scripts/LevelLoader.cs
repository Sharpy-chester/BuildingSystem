using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelLoader : MonoBehaviour
{
    SaveSystem saveSystem;
    public Save save;
    public string saveName;

    private static LevelLoader _instance;
    public static LevelLoader Instance { get { return _instance; } }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += ManageLoad;
        if (_instance != this)
        {
            if (_instance)
            {
                Destroy(_instance.gameObject);
            }
            _instance = this;
        }
    }

    public void ManageLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene" && saveName != "")
        {
            saveSystem = FindObjectOfType<SaveSystem>();
            saveSystem.LoadLevel(save.maps.FindIndex(x => x.name.Equals(saveName)));
            saveName = "";
            save = null;
        }
    }
}
