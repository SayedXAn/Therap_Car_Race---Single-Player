using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;


public class UIManager : MonoBehaviour
{
    [Header("Scripts")]
    public ScoreManager scoreManager;
    public LeapPointerController LPCon;
    public LBManager lb;
    public Optimization opt;

    [Header("GameObjects")]
    public GameObject[] cars;
    public GameObject selectCar;    
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject gameOverUI;
    public GameObject gameWinBG;
    public GameObject gameLoseBG;
    public GameObject camUI;
    public GameObject countDownUI;    
    public GameObject joraTali;
    public GameObject raceTrack;
    public GameObject[] trafficLight;
    public GameObject LPointer;

    [Header("Texts")]
    public TMP_Text countDownText;
    public TMP_Text gameTimerText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverPosText;
    public TMP_InputField nameIF;

    [Header("Variables")]
    public bool isMenuOn = true;      
    public int current_car;
    public float countDown = 5f;
    public int gameTimer = 90;
    private int totalCar;    
    private float countTime = 0f;    
    private int playerPos = -2;




    void Start()
    {
        totalCar = cars.Length;
        current_car = 0;
        selectCar.SetActive(true);
        isMenuOn = true;
        nameIF.ActivateInputField();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!isMenuOn && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Therap_Car_Race");
        }

    }


    public void NextCar()
    {
        if (current_car == totalCar - 1)
        {
            current_car = 0;
        }
        else
        {
            current_car++;
        }
        for (int i = 0; i < totalCar; i++)
        {
            if (i == current_car)
            {
                cars[i].SetActive(true);
            }
            else
            {
                cars[i].SetActive(false);
            }
        }

    }

    public void PreviousCar()
    {
        if (current_car == 0)
        {
            current_car = totalCar - 1;
        }
        else
        {
            current_car--;
        }
        for (int i = totalCar - 1; i >=0; i--)
        {
            if (i == current_car)
            {
                cars[i].SetActive(true);
            }
            else
            {
                cars[i].SetActive(false);
            }
        }

    }

    public void LeaderboardButton()
    {
        lb.GenerateLeaderboard();   
        isMenuOn = false;
        menuUI.SetActive(false);        
    }
    public void HomeButton()
    {        
        /*isMenuOn = true;
        menuUI.SetActive(true);
        lb.HideLeaderboard();*/
        SceneManager.LoadScene("Therap_Car_Race");
    }

    public void StartGameButton()
    {
        if(isMenuOn && nameIF.text != "")
        {
            isMenuOn = false;
            Quaternion target = Quaternion.Euler(0, 90, 0);
            raceTrack.transform.rotation = target;
            raceTrack.GetComponent<Spin>().enabled = false;
            //selectCar.SetActive(false);
            Destroy(selectCar);
            menuUI.SetActive(false);
            camUI.SetActive(false);
            LPCon.GetComponent<LeapPointerController>().enabled = false;
            LPointer.SetActive(false);
            joraTali.SetActive(true);
            joraTali.GetComponent<Ultimate>().SelectBody(current_car);
            countDownUI.SetActive(true);
            StartCoroutine(CountDown());
        }
        else
        {
            nameIF.placeholder.GetComponent<TMP_Text>().text = "Enter your name here\nbefore proceeding";
        }
    }

    IEnumerator CountDown()
    {
        countDownText.text = (5 - countTime) + " Second(s)";
        yield return new WaitForSeconds(1f);
        countTime += 1f;
        if(countTime == countDown)
        {
            countDownUI.SetActive(false);
            joraTali.GetComponent<Ultimate>().TurnGameOn();
            trafficLight[0].SetActive(false);
            trafficLight[1].SetActive(true);
            gameUI.SetActive(true);
            StartCoroutine(GameTimerCountDown());
        }
        else
        {            
            StartCoroutine(CountDown());
        }
        
    }
    IEnumerator GameTimerCountDown()
    {
        gameTimerText.text = gameTimer.ToString();
        yield return new WaitForSeconds(1f);
        if(gameTimer > 1)
        {
            gameTimer--;
            StartCoroutine(GameTimerCountDown());
        }
        else
        {
            StopCoroutine(GameTimerCountDown());
        }

    }
    public void GameWin()
    {
        StartCoroutine(GameOver(true));
    }    
    public void HitByObstacle()
    {
        StartCoroutine(GameOver(false));
    }

    IEnumerator GameOver(bool isWinner)
    {
        lb.SetEntry(nameIF.text, scoreManager.GetScore());
        gameOverScoreText.text = scoreManager.GetScore().ToString();
        StartCoroutine(ShowPosition());

        joraTali.GetComponent<Ultimate>().gameOn = false;

        gameUI.SetActive(false);

        gameOverUI.SetActive(true);
        gameLoseBG.SetActive(!isWinner);
        gameWinBG.SetActive(isWinner);     
        
        yield return new WaitForSeconds(4);

        opt.TurnOnThings(opt.t10);
        gameOverUI.SetActive(false);
        camUI.SetActive(true);
        joraTali.SetActive(false);
        LPCon.GetComponent<LeapPointerController>().enabled = true;
        LPointer.SetActive(true);        
        scoreManager.ResetScore();
        GoToLeaderboard();
        StartCoroutine(RestartScene());
    }

    
    IEnumerator ShowPosition()
    {
        yield return new WaitForSeconds(0.1f);
        if(playerPos == -2)
        {
            StartCoroutine(ShowPosition());
        }
        else
        {
            gameOverPosText.text = "Your position: " + playerPos.ToString();
            StopCoroutine(ShowPosition());
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Therap_Car_Race");
    }

    private void GoToLeaderboard()
    {
        lb.GenerateLeaderboard();
        StartCoroutine(RestartScene());
    }
    public void SetPlayerPos(int pos)
    {
        playerPos = pos;
    }
}
