using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private bool isWin;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (isWin)
                sceneIndex++;

            SceneManager.LoadScene(sceneIndex);
        }
    }
}
