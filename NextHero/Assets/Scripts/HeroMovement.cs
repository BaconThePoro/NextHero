using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public Text ControlMode = null; 
    public Text mEnemyCountText = null;
    public float speed = 20f;
    public float mHeroRotateSpeed = 100f / 2f; // 90-degrees in 2 seconds
    public bool mFollowMousePosition = true;
    public int enemiesShot = 0; 
    // Start is called before the first frame update

    private int mPlanesTouched = 0;

    private GameController mGameGameController = null;

    private float rateOfFire = 0.2f;
    private float shotCooldown = 0.0f;

    public GameObject TextObject = null;
    EggCount eggCounter;

    public Text touchedText = null;
    int touched = 0;

    void Start()
    {
        mGameGameController = FindObjectOfType<GameController>();
        eggCounter = TextObject.GetComponent<EggCount>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mFollowMousePosition = !mFollowMousePosition;

            if (mFollowMousePosition)
                ControlMode.text = "Hero Control Mode: Mouse";
            else
                ControlMode.text = "Hero Control Mode: Keyboard";

        }
        Vector3 pos = transform.position;

        if (mFollowMousePosition)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log("Position is " + pos);
            pos.z = 0f;  // <-- this is VERY IMPORTANT!
            // Debug.Log("Screen Point:" + Input.mousePosition + "  World Point:" + p);
        }
        else
        {

            pos += ((speed * Time.smoothDeltaTime) * transform.up);

            if (Input.GetKey(KeyCode.W))
            {
                speed += 0.1f; 
            }

            if (Input.GetKey(KeyCode.S))
            {
                speed -= 0.1f; 
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);
            }
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > shotCooldown)
        {
            shotCooldown = Time.time + rateOfFire; 

            // Prefab MUST BE locaed in Resources/Prefab folder!
            GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);
            e.transform.localPosition = transform.localPosition;
            e.transform.rotation = transform.rotation;
            Debug.Log("Spawn Eggs:" + e.transform.localPosition);
            eggCounter.eggCount++;
            eggCounter.updateText();
        }
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collision wasnt with an enemy, leave
        if (collision.gameObject.name != "Enemy(Clone)")
            return; 

        Debug.Log("Here x Plane: OnTriggerEnter2D");
        mPlanesTouched = mPlanesTouched + 1;
        mEnemyCountText.text = "Enemies Destroyed: " + (mPlanesTouched + enemiesShot);
        Destroy(collision.gameObject);
        mGameGameController.EnemyDestroyed();
        touched++; 
        touchedText.text = "Enemies Touched: " + touched; 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Here x Plane: OnTriggerStay2D");
    }

    public void updateDestroyed()
    {
        mEnemyCountText.text = "Enemies Destroyed: " + (mPlanesTouched + enemiesShot);
    }
}