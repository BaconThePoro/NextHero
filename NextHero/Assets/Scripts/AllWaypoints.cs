using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllWaypoints : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Apos = Vector3.zero;
    public Vector3 Bpos = Vector3.zero;
    public Vector3 Cpos = Vector3.zero;
    public Vector3 Dpos = Vector3.zero;
    public Vector3 Epos = Vector3.zero;
    public Vector3 Fpos = Vector3.zero;
    Renderer Arend = null;
    Renderer Brend = null;
    Renderer Crend = null;
    Renderer Drend = null;
    Renderer Erend = null;
    Renderer Frend = null;

    public Text waypointTXT = null;
    public bool waypointBool = false; 

    void Start()
    {
        updatePos(); 
        Arend = transform.GetChild(0).GetComponent<Renderer>();
        Brend = transform.GetChild(1).GetComponent<Renderer>();
        Crend = transform.GetChild(2).GetComponent<Renderer>();
        Drend = transform.GetChild(3).GetComponent<Renderer>();
        Erend = transform.GetChild(4).GetComponent<Renderer>();
        Frend = transform.GetChild(5).GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            waypointBool = !waypointBool;

            if (!waypointBool)
                waypointTXT.text = "Waypoints: Sequence";
            else
                waypointTXT.text = "Waypoints: Random";
        }
    }

    public void updatePos()
    {
        Apos = transform.GetChild(0).position;
        Bpos = transform.GetChild(1).position;
        Cpos = transform.GetChild(2).position;
        Dpos = transform.GetChild(3).position;
        Epos = transform.GetChild(4).position;
        Fpos = transform.GetChild(5).position;
    }

    public Vector3 getPos(int val)
    {
        switch (val)
        {
            case 0:
                return Apos;
            case 1:
                return Bpos;
            case 2:
                return Cpos;
            case 3:
                return Dpos;
            case 4:
                return Epos;
            case 5:
                return Fpos;
            default:
                return Vector3.zero;
        }
    }

    public void toggleVisible()
    {
        Arend.enabled = !Arend.enabled;
        Brend.enabled = !Brend.enabled;
        Crend.enabled = !Crend.enabled;
        Drend.enabled = !Drend.enabled;
        Erend.enabled = !Erend.enabled;
        Frend.enabled = !Frend.enabled;
    }
}
