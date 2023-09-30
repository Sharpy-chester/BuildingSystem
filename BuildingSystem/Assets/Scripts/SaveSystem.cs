using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    ObjectPlacer placer;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject cylinderPrefab;

    private void Awake()
    {
        placer = FindObjectOfType<ObjectPlacer>();
    }

    public void SaveLevel()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        Save data;
        if (!File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat"))
        {
            //if no maps have been saved before
            file = File.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat");
            data = new Save();
            print("New Save");
        }
        else
        {
            file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat", FileMode.Open);
            data = (Save)bf.Deserialize(file);
            print("Open Save");
        }
        print(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat");

        //create new map
        Map newMap = new Map
        {
            name = System.DateTime.Now.ToString(),
            position = new Vec3[placer.placedObjects.Count],
            rotation = new Vec3[placer.placedObjects.Count],
            scale = new Vec3[placer.placedObjects.Count],
            type = new string[placer.placedObjects.Count]
        };
        for (int i = 0; i < placer.placedObjects.Count; i++)
        {
            newMap.position[i] = new Vec3(placer.placedObjects[i].transform.position.x, placer.placedObjects[i].transform.position.y, placer.placedObjects[i].transform.position.z);
            newMap.rotation[i] = new Vec3(placer.placedObjects[i].transform.eulerAngles.x, placer.placedObjects[i].transform.eulerAngles.y, placer.placedObjects[i].transform.eulerAngles.z);
            newMap.scale[i] = new Vec3(placer.placedObjects[i].transform.localScale.x, placer.placedObjects[i].transform.localScale.y, placer.placedObjects[i].transform.localScale.z);
            newMap.type[i] = placer.placedObjects[i].name;
        }
        //save newly created map
        data.maps.Add(newMap);
        file.Close();
        //for some reason if i dont delete and then remake the file it doesnt work :D Will fix later
        File.Delete(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat");
        file = File.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat");
        bf.Serialize(file, data);
        print("Saved");
        file.Close();
    }

    public void LoadLevel(int id)
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat", FileMode.OpenOrCreate);
            Save data = (Save)bf.Deserialize(file);
            file.Close();
            //for each object in selected map
            for(int i = 0; i < data.maps[id].type.Length; i++)
            {
                Vector3 pos = new Vector3(data.maps[id].position[i].x, data.maps[id].position[i].y, data.maps[id].position[i].z);
                Vector3 rot = new Vector3(data.maps[id].rotation[i].x, data.maps[id].rotation[i].y, data.maps[id].rotation[i].z);
                Vector3 scale = new Vector3(data.maps[id].scale[i].x, data.maps[id].scale[i].y, data.maps[id].scale[i].z);
                GameObject newObj;
                switch (data.maps[id].type[i])
                {
                    case "Cube":
                        newObj = Instantiate(cubePrefab, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
                        newObj.transform.localScale = scale;
                        newObj.name = "Cube";
                        placer.placedObjects.Add(newObj);
                        break;

                    case "Sphere":
                        newObj = Instantiate(spherePrefab, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
                        newObj.transform.localScale = scale;
                        newObj.name = "Sphere";
                        placer.placedObjects.Add(newObj);
                        break;

                    case "Wall":
                        newObj = Instantiate(wallPrefab, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
                        newObj.transform.localScale = scale;
                        newObj.name = "Wall";
                        placer.placedObjects.Add(newObj);
                        break;

                    case "Cylinder":
                        newObj = Instantiate(cylinderPrefab, pos, Quaternion.Euler(rot.x, rot.y, rot.z));
                        newObj.transform.localScale = scale;
                        newObj.name = "Cylinder";
                        placer.placedObjects.Add(newObj);
                        break;

                }
            }
        }
    }

    public Save GetSaves()
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "maps.dat", FileMode.Open);
            return (Save)bf.Deserialize(file);
        }
        else
        {
            return null;
        }
    }
}


[Serializable]
public class Save
{
    public List<Map> maps = new List<Map>();
}

[Serializable]
public class Map
{
    public string name;
    public Vec3[] position;
    public Vec3[] rotation;
    public Vec3[] scale;
    public string[] type;
}

//have to make my own vector3 as the normal one isn't serialized :/
[Serializable]
public class Vec3
{
    public Vec3(float a, float b, float c)
    {
        x = a;
        y = b;
        z = c;
    }
    public float x;
    public float y;
    public float z;
}