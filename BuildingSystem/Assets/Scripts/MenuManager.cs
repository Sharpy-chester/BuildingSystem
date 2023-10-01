using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadMenu;
    [SerializeField] GameObject buttonsGO;
    [SerializeField] GameObject buttonPrefab;
    List<GameObject> buttons = new List<GameObject>();
    SaveSystem saveSys;
    Save save;

    private void Awake()
    {
        saveSys = FindObjectOfType<SaveSystem>();
    }

    public void Play()
    {
        Destroy(LevelLoader.Instance.gameObject);
        SceneManager.LoadScene("SampleScene");
    }

    public void Load()
    {
        loadMenu.SetActive(true);
        mainMenu.SetActive(false);
        save = saveSys.GetSaves();
        
        if (save != null)
        {
            for (int i = 0; i < save.maps.Count; i++)
            {
                GameObject btn = Instantiate(buttonPrefab);
                btn.transform.SetParent(buttonsGO.transform);
                buttons.Add(btn);
                btn.name = save.maps[i].name;
                btn.GetComponentInChildren<TextMeshProUGUI>().text = save.maps[i].name;
                btn.GetComponent<Button>().onClick.AddListener(LoadLevel);
            }
        }
    }

    public void LoadLevel()
    {
        LevelLoader.Instance.saveName = EventSystem.current.currentSelectedGameObject.name;
        LevelLoader.Instance.save = save;
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        loadMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}