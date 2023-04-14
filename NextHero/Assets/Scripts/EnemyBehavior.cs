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
   
    float speed = 0.25f;
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

        if (Input.GetKeyDown(KeyCode.J))
        {
            pathingToggle = !pathingToggle;
        }
    }

    private void FixedUpdate()
    {
        transform.up = (allWaypoints.getPos(currWaypoint) - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, allWaypoints.getPos(currWaypoint), speed);

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
