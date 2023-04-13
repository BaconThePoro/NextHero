using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggBehaviour : MonoBehaviour
{
    public const float kEggSpeed = 40f;
    CameraSupport s = null;
    Rigidbody2D ourRb = null;
    public GameObject TextObject = null;
    EggCount eggCounter; 

    private int mLifeCount = 0; 
    // Start is called before the first frame update
    void Start()
    {
        s = Camera.main.GetComponent<CameraSupport>();
        ourRb = GetComponent<Rigidbody2D>();
        TextObject = GameObject.Find("EggNumber"); 
        eggCounter = TextObject.GetComponent<EggCount>();
    }

    // Update is called once per frame
    void Update()
    {
        /*      transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);

                ourRb.MovePosition(transform.position + transform.up * (kEggSpeed * Time.smoothDeltaTime));
                if (!s.isInside(transform.GetComponent<Collider2D>().bounds))
                {
                    Debug.Log("Egg has hit world boundary"); 
                    Destroy(gameObject);
                }*/
    }

    private void FixedUpdate()
    {
        ourRb.MovePosition(transform.position + (transform.up * (kEggSpeed * Time.smoothDeltaTime)));
        if (!s.isInside(transform.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("Egg has hit world boundary");
            Destroy(gameObject);
            eggCounter.eggCount--; 
            eggCounter.updateText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Egg has hit something");
        if (collision.gameObject.name == "Enemy(Clone)")
        {
            Debug.Log("Egg has hit an enemy");
            collision.gameObject.GetComponent<EnemyBehavior>().reduceLife(); 
            Destroy(gameObject);
            eggCounter.eggCount--;
            eggCounter.updateText();
        }
    }
}
