using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    ObjectSelector selector;
    ObjectEditor editor;
    Camera cam;
    GameObject placeholderObj;
    GameObject currentPlaceholderObj;
    bool moveObj = false;
    public List<GameObject> placedObjects = new List<GameObject>();

    void Awake()
    {
        editor = FindObjectOfType<ObjectEditor>();
        selector = FindObjectOfType<ObjectSelector>();
        cam = Camera.main;
        selector.selectionChange += PlaceholderChange;
    }

    void Update()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(2) && hit.transform.CompareTag("Object"))
            {
                //Edit mode for placed object
                editor.ChangeEditableObject(hit.transform.gameObject);
                selector.Deselect();
                Destroy(currentPlaceholderObj);
            }
            else if (Input.GetMouseButtonDown(1) && hit.transform.CompareTag("Object"))
            {
                //select and move object
                editor.ChangeEditableObject(hit.transform.gameObject);
                selector.Deselect();
                Destroy(currentPlaceholderObj);
                moveObj = true;
                editor.EditableObject.layer = 2;
            }
            if(moveObj)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    moveObj = false;
                    editor.EditableObject.layer = 0;
                }
                editor.EditableObject.transform.position = hit.point + (Vector3.up * editor.EditableObject.transform.localScale.y / 2);
            }
            if (selector.SelectedObject != null)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    //if the mouse is pointing at an object and isn't over the UI
                    GameObject obj = Instantiate(selector.SelectedObject.prefab, hit.point + (Vector3.up * selector.SelectedObject.prefab.transform.localScale.y / 2), Quaternion.identity);
                    obj.name = selector.SelectedObject.name;
                    placedObjects.Add(obj);
                }
                else
                {
                    //the end part makes sure that it doesn't stick through the ground (apart from the cylinder because Unity sized it wrong :D)
                    currentPlaceholderObj.transform.position = hit.point + (Vector3.up * selector.SelectedObject.prefab.transform.localScale.y / 2);
                }
            }
        }
    }

    void PlaceholderChange()
    {
        placeholderObj = selector.SelectedObject.placeholderPrefab;
        Destroy(currentPlaceholderObj);
        currentPlaceholderObj = Instantiate(placeholderObj);
        if (editor.EditableObject)
        {
            editor.ChangeEditableObject(null);
        }
    }

    public void RemoveObject(GameObject objectToRemove)
    {
        placedObjects.Remove(objectToRemove);
    }
}
