using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TowerGM : MonoBehaviour
{

    public float towerCurrentHealth;
    public float towerMaxHealth = 10;
    public float currentTime = 0;
    public float timeHighScore;
    public bool gameRunning;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI towerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        towerCurrentHealth = towerMaxHealth;
        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        survivalTimeText.text = "Survival Time: " + currentTime;
        towerHealthText.text = "Tower Health: " + towerCurrentHealth;

        if (gameRunning)
        {
            currentTime += 1 * Time.deltaTime;
            
            if (towerCurrentHealth <= 0)
            {
                gameRunning = false;
                SceneManager.LoadScene("test");
            }
        }
    }
}
