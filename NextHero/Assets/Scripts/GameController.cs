using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int maxPlanes = 10;
    private int numberOfPlanes = 0;
    public Text ourText = null;
    EnemyCount enemyCount;
    public bool enemyMove = false;
    public bool waypointActive = true;
    public GameObject allWaypoints = null; 

    private float waitAmount = 25.0f;
    private float waitTime = 0.0f;
    GameObject DogSphere = null;
    Vector3 newV = new Vector3(-60f, 20f, 0f);
    Vector3 newR = new Vector3(0.6f, 0.9f, 1f);
    Vector3 newP = new Vector3(300, -100, 80);
    

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = ourText.GetComponent<EnemyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyMove && DogSphere != null)
            Destroy(DogSphere);

        if (enemyMove == true && Time.time > waitTime)
        {
            Destroy(DogSphere);
            waitTime = Time.time + waitAmount;
            DogSphere = Instantiate(Resources.Load("Prefabs/DogSphere") as GameObject);
            DogSphere.GetComponent<Rigidbody>().velocity = newV;
            DogSphere.GetComponent <Rigidbody>().angularVelocity = newR;
            DogSphere.transform.position = newP;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            waypointActive = !waypointActive;
            if (allWaypoints != null)
                allWaypoints.SetActive(waypointActive);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            flipMove();
        }

            if (Input.GetKey(KeyCode.Q))
        {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (numberOfPlanes < maxPlanes)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();
            Assert.IsTrue(s != null);

            GameObject e = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject); // Prefab MUST BE locaed in Resources/Prefab folder!
            Vector3 pos;
            pos.x = s.GetScaledBound(0.9f).min.x + Random.value * s.GetScaledBound(0.9f).size.x;
            pos.y = s.GetScaledBound(0.9f).min.y + Random.value * s.GetScaledBound(0.9f).size.y;
            pos.z = 0;
            e.transform.localPosition = pos;
            ++numberOfPlanes;
            enemyCount.enemyCount = numberOfPlanes;
            enemyCount.updateText();
        }
    }

    public void EnemyDestroyed()
    {
        --numberOfPlanes;
        enemyCount.enemyCount = numberOfPlanes;
        enemyCount.updateText();
    }

    public void flipMove()
    {
        enemyMove = !enemyMove; 
    }
}