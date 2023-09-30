using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUI : MonoBehaviour
{
    ObjectSelector selector;

    private void Awake()
    {
        selector = FindObjectOfType<ObjectSelector>();
    }


}
