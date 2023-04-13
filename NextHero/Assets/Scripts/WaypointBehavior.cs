using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour
{
    int currLife = 4;
    Color ourColor;
    int distance = 15; // units
    Vector3 prevPos; 

    // Start is called before the first frame update
    void Start()
    {
        ourColor = GetComponent<SpriteRenderer>().color; 
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
        float x = Random.value;
        if (x <= 0.4)
            x = -1;
        else
            x = 1;

        float y = Random.value;
        if (y <= 0.4)
            y = -1;
        else
            y = 1;

        prevPos = transform.position;
        transform.position = new Vector3(prevPos.x + x * distance, prevPos.y + y * distance, 0);
    }
}
