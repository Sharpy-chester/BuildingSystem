using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    public Object SelectedObject { get; private set; }

    public delegate void SelectionChange();
    public event SelectionChange selectionChange;

    public void SelectObject(Object obj)
    {
        SelectedObject = obj;
        print(SelectedObject.name);
        selectionChange?.Invoke();
    }

    public void Deselect()
    {
        SelectedObject = null;
        print("Deselect");
    }
}
