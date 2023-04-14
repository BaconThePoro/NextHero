using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour
{
    int currLife = 4;
    Color ourColor;
    Vector3 initPos;
    public GameObject allWaypoints = null; 
    

    // Start is called before the first frame update
    void Start()
    {
        ourColor = GetComponent<SpriteRenderer>().color;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage()
    {
        currLife--;
        ourColor = new Color(ourColor.r, ourColor.g, ourColor.b, ourColor.a * 0.75f);
        GetComponent<SpriteRenderer>().color = ourColor;

        if (currLife == 0)
        {
            moveWaypoint();
            ourColor = new Color(ourColor.r, ourColor.g, ourColor.b, 1f);
            GetComponent<SpriteRenderer>().color = ourColor;
            currLife = 4;
        }
            
            
    }

    public void moveWaypoint()
    {
        transform.position = new Vector3(initPos.x + Random.Range(-15, 15), initPos.y + Random.Range(-15, 15), 0);
        allWaypoints.GetComponent<AllWaypoints>().updatePos();
    }
}
