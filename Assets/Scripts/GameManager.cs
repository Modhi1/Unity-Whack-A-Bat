using UnityEngine;
using TMPro;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Variables

    public event Action GameEnded;

    public static GameManager instance;
    
    [SerializeField] private Mole[] moles;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;

    // time of the game is set to 1 min 
    private float timeRemaining = 60f;
    private int score;


    #endregion

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {

        StartCoroutine(RandomMoleAppear());
        //  InvokeRepeating("RandomMoleAppear", 1f, 1f);

        // subscribe to event 
        Mole.MoleClicked += ScoreIncrement;
    }

    private void Update()
    {

        UpdateTexts();

        timeRemaining -= Time.deltaTime;

        // check remaining time for the game to end 
        if (timeRemaining <= 0)
        {
            ResetMoles();

            // Show Game obver text
            GameEnded?.Invoke();
           // I can instead cancel the coroutine 
            gameObject.SetActive(false);



        }



    }

    private void UpdateTexts()
    {
        remainingTimeText.text = "Time: " + timeRemaining.ToString("00s");
        scoreText.text = "Score: " + score.ToString();
    }

    private void ScoreIncrement()
    {
        // each hit increment score by 1 
        score++;

    }

    private IEnumerator RandomMoleAppear()
    {
        int size = moles.Length;

        
        while(true)
        {
            int index = Random.Range(0, size);

            if (moles[index].IsMovingUp == false)
            {
                moles[index].IsMovingUp = true;
                break;
            }

        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomMoleAppear());



    }

    // loop all the moles to Move them down if they're up
    private void ResetMoles()
    {

        for (int i=0; i < moles.Length; i++)
        {
            if (moles[i].IsMovingUp == true)
            {
                moles[i].IsMovingUp = false;
            }

        }

    }

    private void OnDestroy()
    {
        // unsubscribe to event
        Mole.MoleClicked -= ScoreIncrement;
    }

}
