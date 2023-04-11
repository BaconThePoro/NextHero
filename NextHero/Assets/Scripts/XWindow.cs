using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XWindow : MonoBehaviour
{
    public GameObject window = null;
    public GameObject outline = null; 
    public void Clicked()
    {
        window.SetActive(false);
        outline.SetActive(false);
    }
}
