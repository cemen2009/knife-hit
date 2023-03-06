using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KnifeSpawn knifeSpawn;
    private GameUI gameUI;
    public bool inputAvailable;
    public static GameManager instance { get; private set; }
    private ushort knivesNumber;    // we use this variable for UI
    public ushort activeKnives;    // number of knives left
    [SerializeField] private AudioSource RestartSound;
    private bool gameOverTime = false;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        inputAvailable = true;
        instance = this;
        gameUI = GetComponent<GameUI>();
        knivesNumber = (ushort)Random.Range(4f, 10f);
        activeKnives = knivesNumber; // activeKnives - knives player should drop to win. At start that's all knives
        gameUI.ShowInitialDisplayedKnifeCount(knivesNumber);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (gameOverTime)
        {
            timer += Time.deltaTime;
            if (timer > 0.7f)
                Time.timeScale = 0;
        }
    }

    // we call this function from Knife.cs when knife hit Log
    public void KnifeHitted()
    {
        activeKnives--; // decrement number of active knives
        gameUI.DecrementDisplayedKnifeCount();

        //if are active knives
        if (activeKnives > 0)
            knifeSpawn.KnifeSpawning();
        else
        {
            GameOver();
        }
    }

    // I use this method like winning and losing because both have identical code in this practice project
    public void GameOver()
    {
        // StartCoroutine("GameOverCoroutine");
        // Time.timeScale = 0;
        gameUI.ShowRestartButton();
        gameOverTime = true;
        inputAvailable = false;
        
        // TODO: show lose UI and check record
    }
     /*private IEnumerable GameOverCoroutine()
     {
        yield return new WaitForSecondsRealtime(0.3f);

        Time.timeScale = 0;
        gameUI.ShowRestartButton();
     }*/

    public void Restart()
    {
        RestartSound.Play();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        // TODO: Check if the best record is beaten and if yeas than resave the best record
    }
}
