using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private int _levelNumber;
    [SerializeField] private GameObject _levelPrefab;
    
    [Header("Birds")]
    public List<Bird> Birds;
    private Bird _shotBird;
    [Header("Enemy")]
    public List<Enemy> Enemies;
    
    [Header("Tools")]
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public BoxCollider2D TapCollider;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public bool _isGameEnded = false;

    void Start()
    {
        InitLevel();
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    void InitLevel()
    {
        _levelPrefab = Resources.Load("LevelData/Level-" + _levelNumber) as GameObject;
        GameObject enemy = Instantiate(_levelPrefab);
        Enemies = new List<Enemy>(enemy.GetComponentsInChildren<Enemy>());
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }

        if (Birds.Count == 0)
        {
            _isGameEnded = true;
            GameEnded();
            Debug.Log("You Lost");
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
            GameEnded();
            Debug.Log("You Win");
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void GameEnded()
    {
        gameOverPanel.SetActive(true);
    }

    public void GameOverButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
