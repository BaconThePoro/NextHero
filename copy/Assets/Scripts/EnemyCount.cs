using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{
    public int enemyCount = 0;
    Text ourText = null;

    // Start is called before the first frame update
    void Start()
    {
        ourText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText()
    {
        ourText.text = "Enemies: " + enemyCount;
    }
}
