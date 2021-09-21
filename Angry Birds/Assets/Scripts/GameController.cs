using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public List<Bird> birds;
    public List<Enemy> enemies;
    public TrailController trailController;
    public BoxCollider2D tapCollider;
    public GameObject WinPanel, LosePanel;

    private Bird shotBird;
    private bool isGameEnded = false;

    private void Start()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdShot += AssignTrail;
            birds[i].OnBirdDestroyed += ChangeBird;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        slingShooter.InitiateBird(birds[0]);
        shotBird = birds[0];
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;

        if (isGameEnded) return;

        birds.RemoveAt(0);

        if(birds.Count > 0) //kalau masih ada bird yang bisa dilontarkan
        {
            slingShooter.InitiateBird(birds[0]);
            shotBird = birds[0];
        }
        else //kalau bird udah habis berarti kalah
        {
            Debug.Log("kalah woi");
            StartCoroutine(ShowPanel(LosePanel));
            isGameEnded = true;
        }
    }

    public void AssignTrail(Bird selectedBird)
    {
        trailController.SetBird(selectedBird);
        StartCoroutine(trailController.SpawnTrails());
        tapCollider.enabled = true;
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if(enemies.Count == 0)
        {
            Debug.Log("menang woi");
            StartCoroutine(ShowPanel(WinPanel));
            isGameEnded = true;
        }
    }

    private IEnumerator ShowPanel(GameObject panel)
    {
        yield return new WaitForSeconds(2f);
        panel.SetActive(true);
    }

    private void OnMouseUp()
    {
        if(shotBird != null)
        {
            shotBird.OnTap();
        }
    }
}
