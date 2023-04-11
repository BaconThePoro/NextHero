using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    int currLife = 0;
    SpriteRenderer ourSpriteRenderer;

    Rigidbody2D ourRB; 
   
    float speed = 10f;
    CameraSupport s = null;
    Bounds scaledBound;
    private float waitAmount = 1.0f;
    private float waitTime = 0.0f;
    GameObject ControllerObject;
    GameController gameController; 
    Vector3 stationary = new Vector3(0f, 1f, 0f);

    GameObject Hero = null;
    HeroMovement heroMovement;

    GameObject lifebar = null; 
    Vector3 offset = new Vector3(0f, 10f, 0f);

    void Start()
    {
        currLife = 4; 
        ourSpriteRenderer = GetComponent<SpriteRenderer>();
        ourRB = GetComponent<Rigidbody2D>();
        s = Camera.main.GetComponent<CameraSupport>();
        scaledBound = s.GetScaledBound(0.95f);

        ControllerObject = GameObject.Find("GameController");
        gameController = ControllerObject.GetComponent<GameController>();

        Hero = GameObject.Find("Hero");
        heroMovement = Hero.GetComponent<HeroMovement>();

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


        if (gameController.enemyMove == false)
        {
            transform.up = stationary;
            Destroy(lifebar);
        }

    }

    private void FixedUpdate()
    {
        if (gameController.enemyMove)
        {
            if (transform.up == stationary)
            {
                randDirection();
            }

            ourRB.MovePosition(transform.position + transform.up * speed * Time.smoothDeltaTime);
            
            if (!s.isInsideBounds(transform.GetComponent<Collider2D>().bounds, scaledBound) && Time.time > waitTime)
            {
                waitTime = Time.time + waitAmount;
                Debug.Log("Plane hit edge");
                refDirection();
            }

            if (currLife == 4)
            {
                if (lifebar == null)
                    lifebar = Instantiate(Resources.Load("Prefabs/4life") as GameObject);

                lifebar.transform.position = transform.position + offset;
            }
            else if (currLife == 3)
            {
                if (lifebar == null)
                    lifebar = Instantiate(Resources.Load("Prefabs/3life") as GameObject);

                lifebar.transform.position = transform.position + offset;
            }
            else if (currLife == 2)
            {
                if (lifebar == null)
                    lifebar = Instantiate(Resources.Load("Prefabs/2life") as GameObject);

                lifebar.transform.position = transform.position + offset;
            }
            else if (currLife == 1)
            {
                if (lifebar == null)
                    lifebar = Instantiate(Resources.Load("Prefabs/1life") as GameObject);

                lifebar.transform.position = transform.position + offset;
            }
        }
    }

    void randDirection()
    {
        transform.up = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); 
    }

    void refDirection()
    {
        Vector3 normal = transform.up;

        if (transform.position.x > scaledBound.max.x)
        {
            normal = new Vector3(1f, 0f, 0f);
        }
        else if (transform.position.x < scaledBound.min.x)
        {
            normal = new Vector3(-1f, 0f, 0f);
        }

        if (transform.position.y > scaledBound.max.y)
        {
            normal = new Vector3(0, -1f, 0f);
        }
        else if (transform.position.y < scaledBound.min.y)
        {
            normal = new Vector3(0, 1f, 0f); 
        }


        transform.up = transform.up.normalized - 2 * (Vector3.Dot(transform.up.normalized, normal)) * normal;

    }

    public void reduceLife()
    {
        currLife--;
        Color newColor = new Color(ourSpriteRenderer.color.r, ourSpriteRenderer.color.g, ourSpriteRenderer.color.b, GetComponent<SpriteRenderer>().color.a * 0.8f);
        ourSpriteRenderer.color = newColor;

        if (lifebar != null)
            Destroy(lifebar);
    }

    private void OnDestroy()
    {
        if (lifebar != null)
            Destroy(lifebar);
    }
}
