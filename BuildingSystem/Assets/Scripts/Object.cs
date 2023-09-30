using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object")]
[System.Serializable]
public class Object : ScriptableObject
{
    public string name;
    public Sprite icon;
    public GameObject prefab;
    public GameObject placeholderPrefab;
}
