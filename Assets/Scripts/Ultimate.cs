using System;
using UnityEngine;
using Leap;
using TMPro;
using DG.Tweening;
using System.Collections;
public class Ultimate : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject tailLights;
    public bool gameOn = false;
    public bool mouseOn = false;
    public Camera carCam;
    [SerializeField] GameObject[] bodies;
    public float carspeed = 1.0f;
    public float leapSens = 0.1f;
    public float mouseSens = 0.1f;
    public float lerpSens = 0.1f;
    public float divisor = 30f;
    public float maxLeftPosition = -4.75f;
    public float maxRightPosition = 5.35f;
    private float targetX = 0f;
    private Controller leapController; 
    public ScoreManager scoreManager;
    public UIManager uiManager;
    public LBManager LBManager;
    public Optimization opt;
    public int optCount = 0;
    public int coinPoint = 10;
    public int orbPoint = 50;
    public TMP_Text scoreText;
    public TMP_Text pointText;
    public Transform textSpawn;
    

    [Header("Audio")]
    public AudioSource engineAS;
    public AudioSource coinAS;
    public AudioSource orbAS;
    public AudioSource crashAS;
    void Start()
    {
        DOTween.Init();
        leapController = new Controller();
        uiManager = uiManager.GetComponent<UIManager>();
    }

    void Update()
    {
        if(mouseOn && gameOn)
        {
            SimpleSteeringWithMouse();
        }
    }
    private void FixedUpdate()
    {
        if(gameOn)
        {            
            rb.velocity = Vector3.forward * carspeed;
            SimpleSteeringWithLeap();
            
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }
    public void SimpleSteeringWithLeap()
    {
        Frame frame = leapController.Frame();

        if (frame.Hands.Count > 0)
        {
            mouseOn = false;
            Hand hand = frame.Hands[0];
            float handPositionX = hand.PalmPosition.x;
            targetX = Math.Clamp(handPositionX, -0.096f, 0.096f) * leapSens * Time.deltaTime; //Don't change the 0.096 value without testing, noyto pidamu
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetX , 0.5f), transform.position.y, transform.position.z);
        }
        else
        {
            mouseOn = true;            
        }
    }

    public void SimpleSteeringWithMouse()
    {
        float mouseAxis = Mathf.Clamp(transform.position.x + (Input.GetAxisRaw("Horizontal") * Time.deltaTime * mouseSens), maxLeftPosition, maxRightPosition);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseAxis, lerpSens), transform.position.y, transform.position.z);
    }

    public void TurnGameOn()
    {
        gameOn = true;
        engineAS.loop = true;
        engineAS.Play();
    }

    public void SelectBody(int body)
    {
        for(int i = 0; i < bodies.Length; i++)
        {
            if(i == body)
            {
                bodies[i].SetActive(true);
            }
            else
            {
                //bodies[i].SetActive(false);
                Destroy(bodies[i]);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "coin" && gameOn)
        {
            Destroy(collision.gameObject);
            //collision.gameObject.SetActive(false);
            ShowPointText(coinPoint);
            scoreManager.SetScore(coinPoint);
            scoreText.text = scoreManager.GetScore().ToString();
            coinAS.Stop();
            coinAS.Play();
        }
        else if (collision.gameObject.tag == "orb" && gameOn)
        {
            Destroy(collision.gameObject);
            //collision.gameObject.SetActive(false);
            ShowPointText(orbPoint);
            scoreManager.SetScore(orbPoint);
            scoreText.text = scoreManager.GetScore().ToString();
            orbAS.Stop();
            orbAS.Play();
        }
        else if(collision.gameObject.tag == "obstacle" && gameOn)
        {
            gameOn = false;
            crashAS.gameObject.SetActive(true);
            uiManager.HitByObstacle();
            engineAS.Stop();
            coinAS.Stop();
            orbAS.Stop();
            //Destroy(gameObject);
            //collision.gameObject.SetActive(false);     //THis line is for testing only      

        }
        else if (collision.gameObject.tag == "marker" && gameOn)
        {
            Destroy(collision.gameObject);
            CallOpt();
        }
        else if (collision.gameObject.tag == "finish_collider" && gameOn)
        {
            Destroy(collision.gameObject);
            uiManager.GameWin();
            engineAS.Stop();
            coinAS.Stop();
            orbAS.Stop();
        }

    }
    
    private void CallOpt()
    {
        opt.SelectObjectArray(optCount);
        optCount++;
    }

    public void ShowPointText(int point)
    {
        TMP_Text spawnedText = Instantiate(pointText, textSpawn);
        spawnedText.text = point.ToString();
        Color initialColor = spawnedText.color;
        initialColor.a = 0;
        spawnedText.color = initialColor;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(spawnedText.DOFade(1f, 0.5f));
        mySequence.Join(spawnedText.transform.DOMoveY(spawnedText.transform.position.y + 1f, 0.5f));
        mySequence.Append(spawnedText.DOFade(0f, 0.5f));
        mySequence.OnComplete(() => Destroy(spawnedText.gameObject));
    }

}
