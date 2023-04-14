using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    int currLife = 0;
    SpriteRenderer ourSpriteRenderer;

    Rigidbody2D ourRB; 
   
    float speed = 20f;
    CameraSupport s = null;
    Bounds scaledBound;
    //private float waitAmount = 1.0f;
    //private float waitTime = 0.0f;
    GameObject ControllerObject;
    GameController gameController; 
    //Vector3 stationary = new Vector3(0f, 1f, 0f);

    GameObject Hero = null;
    HeroMovement heroMovement;

    GameObject lifebar = null; 
    Vector3 offset = new Vector3(0f, 10f, 0f);

    bool pathingToggle = false; // false = sequential, true = random
    AllWaypoints allWaypoints = null;
    int currWaypoint = 0;
    float turnSpeed = 1.5f;

    void Start()
    {
        currWaypoint = 0;
        currLife = 4; 
        ourSpriteRenderer = GetComponent<SpriteRenderer>();
        ourRB = GetComponent<Rigidbody2D>();
        s = Camera.main.GetComponent<CameraSupport>();
        scaledBound = s.GetScaledBound(0.95f);

        ControllerObject = GameObject.Find("GameController");
        gameController = ControllerObject.GetComponent<GameController>();

        Hero = GameObject.Find("Hero");
        heroMovement = Hero.GetComponent<HeroMovement>();

        allWaypoints = GameObject.Find("AllWaypoints").GetComponent<AllWaypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currLife == 0)
        {
            gameController.EnemyDestroyed();
            Destroy(gameObject);
            heroMovement.enemiesShot++;
            heroMovement.updateDestroyed();
        }

        pathingToggle = allWaypoints.waypointBool;     
    }

    private void FixedUpdate()
    {
        float singleStep = turnSpeed * Time.smoothDeltaTime;
        Vector3 desiredUp = (allWaypoints.getPos(currWaypoint) - transform.position).normalized; 
        transform.up = Vector3.RotateTowards(transform.up, desiredUp, singleStep, 100f);
        transform.position = transform.position + transform.up * speed * Time.smoothDeltaTime;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.position == allWaypoints.getPos(currWaypoint))
        {
            // sequential
            if (!pathingToggle)
            {
                if (currWaypoint == 5)
                    currWaypoint = 0;
                else currWaypoint++; 
            }
            else
            {
                currWaypoint = Random.Range(0, 5);
            }
        }

    }

    public void reduceLife()
    {
        currLife--;
        Color newColor = new Color(ourSpriteRenderer.color.r, ourSpriteRenderer.color.g, ourSpriteRenderer.color.b, GetComponent<SpriteRenderer>().color.a * 0.8f);
        ourSpriteRenderer.color = newColor;

        if (lifebar != null)
            Destroy(lifebar);
    }
}
