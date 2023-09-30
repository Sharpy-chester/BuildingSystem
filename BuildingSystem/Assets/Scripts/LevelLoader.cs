using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    SaveSystem saveSystem;
    internal Save save;
    internal string saveName;

    private static LevelLoader _instance;
    public static LevelLoader Instance { get { return _instance; } }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += ManageLoad;
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ManageLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene" && save != null)
        {
            saveSystem = FindObjectOfType<SaveSystem>();
            saveSystem.LoadLevel(save.maps.FindIndex(x => x.name.Equals(saveName)));
        }
    }
}
