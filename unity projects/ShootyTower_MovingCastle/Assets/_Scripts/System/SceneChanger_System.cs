using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger_System : MonoBehaviour
{

    [ContextMenu("Go To Menu")]
    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
