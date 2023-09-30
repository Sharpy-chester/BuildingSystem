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
            if (Input.GetMouseButtonDown(2))
            {
                //Edit mode for placed object
                editor.ChangeEditableObject(hit.transform.gameObject);
                selector.Deselect();
                Destroy(currentPlaceholderObj);
            }
            if (selector.SelectedObject != null)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    //if the mouse is pointing at an object and isn't over the UI
                    Instantiate(selector.SelectedObject.prefab, hit.point + (Vector3.up * selector.SelectedObject.prefab.transform.localScale.y / 2), Quaternion.identity);

                }
                else
                {
                    //the end part makes sure that it doesn't stick through the ground
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
}
