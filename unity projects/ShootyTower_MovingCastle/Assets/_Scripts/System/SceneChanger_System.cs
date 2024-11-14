using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_System : MonoBehaviour
{

    public static SceneChanger_System instance;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Go To Menu")]
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
