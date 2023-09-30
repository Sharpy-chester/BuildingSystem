using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEditor : MonoBehaviour
{
    [SerializeField] Slider slider;
    Animator sliderAnim;
    [SerializeField] float rotSpeed = 45f;
    public GameObject EditableObject { get; private set; }

    void Awake()
    {
        sliderAnim = slider.GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Input.GetAxis("YRot") != 0 && EditableObject)
        {
            EditableObject.transform.eulerAngles +=  new Vector3(0, Input.GetAxis("YRot") * Time.deltaTime * rotSpeed, 0);
        }
    }

    public void UpdateScaleSlider(float amt)
    {
        if (EditableObject)
        {
            if(EditableObject.transform.localScale.z / EditableObject.transform.localScale.x != 1)
            {
                EditableObject.transform.localScale = new Vector3(amt, amt, amt * (EditableObject.transform.localScale.z / EditableObject.transform.localScale.x));
            }
            else
            {
                EditableObject.transform.localScale = new Vector3(amt, amt, amt);
            }
            
        }
    }

    public void ChangeEditableObject(GameObject obj)
    {
        if (!obj && EditableObject)
        {
            //selecting primative
            sliderAnim.SetTrigger("Switch");
            if (EditableObject.GetComponent<MeshRenderer>())
            {
                EditableObject.GetComponent<MeshRenderer>().material.color *= new Color(1, 1, 1, 2);
            }
            else
            {
                EditableObject.GetComponentInChildren<MeshRenderer>().material.color *= new Color(1, 1, 1, 2);
            }
            EditableObject = obj;
        }
        else if (obj && obj != EditableObject)
        {
            //going into editing mode
            if (!EditableObject)
            {
                sliderAnim.SetTrigger("Switch");
            }
            else
            {
                if (EditableObject.GetComponent<MeshRenderer>())
                {
                    EditableObject.GetComponent<MeshRenderer>().material.color *= new Color(1, 1, 1, 2);
                }
                else
                {
                    EditableObject.GetComponentInChildren<MeshRenderer>().material.color *= new Color(1, 1, 1, 2);
                }
            }
            EditableObject = obj;
            if (EditableObject.TryGetComponent(out MeshRenderer mr))
            {
                mr.material.color *= new Color(1, 1, 1, 0.5f);
            }
            else
            {
                EditableObject.GetComponentInChildren<MeshRenderer>().material.color *= new Color(1, 1, 1, 0.5f);
            }
            slider.value = obj.transform.localScale.x;
        }
        
        
    }
}
