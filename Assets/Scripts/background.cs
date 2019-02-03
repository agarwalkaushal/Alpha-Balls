using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class background : MonoBehaviour {

    public GameObject planeColor;
    public GameObject canvas;
    public GameObject canvasWord;
    public GameObject canvasGame;
    public GameObject canvasGameOver;
    public GameObject canvasGameWon;
    public GameObject sphere;
    public GameObject gun;

    private GameObject instantiatedObj;
    private Transform gunMovement;
    private Renderer rend;
    private string color;
    private bool gameStarted;
    private float timeLeft = 3.0f;
    private int launchCount;

    public Text word;
    public Text gameWord;
    public Text session;

    public static string rword;
    public static background b;
    
    private static bool gameOverB;
    private static bool gameWonB;
    private static background instance;

    void Awake()
    { 
        //Store the number of sessions user has played the game
        launchCount = PlayerPrefs.GetInt("TimesLaunched", 0);
        launchCount = launchCount + 1;
        PlayerPrefs.SetInt("TimesLaunched", launchCount);

        //set our static reference to our newly initialized instance
        instance = this; 
    }

    void Start () {

        session.text = "Session: " + launchCount.ToString(); //set session number text

        rend = planeColor.GetComponent<Renderer>();
        gunMovement = gun.GetComponent<Transform>();

        color = buttonClick.bc.color;
        gameOverB = collision.gameOver;
        gameWonB = collision.gameWon;
        
    }
	

	void Update () {

        //Check the color selected at the start scene and set background accordingly
        if (color == "Red")
        rend.material.color = Color.red;
        else if (color == "Green")
            rend.material.color = Color.green;
        else
            rend.material.color = Color.blue;

    }

    public static void gameOver()
    {
        //Called from collision script when user hits wrong letter
        instance.StartCoroutine("GameOver");

    }
    IEnumerator GameOver()
    {
        canvasGame.SetActive(false);
        canvasGameOver.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameRound");

    }
    public static void gameWon()
    {
        //Called from collision script when user hits all letters correctly
        instance.StartCoroutine("GameWon");
    }
    IEnumerator GameWon()
    {
        canvasGame.SetActive(false);
        gameStarted = false;
        canvasGameWon.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameRound");

    }
    public void FixedUpdate(string key)
    {
        //User input to move the gun which hits the letters
        if (Input.GetKeyDown(KeyCode.A) == true || key=="LEFT")
            gunMovement.position += new Vector3(1, 0, 0);
        if (Input.GetKeyDown(KeyCode.D) == true || key=="RIGHT" )
            gunMovement.position += new Vector3(-1,0, 0);
    }
    
    //Generate word to make a random word of 3 to 5 letters after user proceeds i.e after reading the rules of the game

    public void generateWord()
    {
        rword = null;
        const string letters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        int charAmount = Random.Range(3, 5); //set those to the minimum and maximum length of your string
        for (int i = 0; i < charAmount; i++)
        {
            rword += letters[Random.Range(0, letters.Length)];
        }

        canvas.SetActive(false);

        word.text = rword;
        gameWord.text = rword;

        StartCoroutine(StartNow()); //To wait for 3 seconds before starting the game
       
    }
    IEnumerator StartNow()
    {

        yield return new WaitForSeconds(3);
        startGame();
    }
    
    private void startGame()
    {
        canvasWord.SetActive(false);
        canvasGame.SetActive(true);
        gun.SetActive(true);
        gameStarted = true;
        if(gameStarted)
        StartCoroutine(dropSpheres());

    }

    //instantiate spheres with random letter which is assigned to their text mesh
    //after 5 seconds the sphere is destroyed to save memory
    IEnumerator dropSpheres()
    {
        while (true)
        {
            Vector3 spawnposition = new Vector3(Random.Range(-4.5f, 4.5f), 8f, 2f);
            Quaternion spawnrotation = Quaternion.Euler(0f,180f,0f);
            const string letters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            char sphereLetter;
            sphereLetter = letters[Random.Range(0, letters.Length)];
            TextMesh textObject = sphere.GetComponentInChildren<TextMesh>();
            textObject.text = sphereLetter.ToString();
                
            instantiatedObj = (GameObject)Instantiate(sphere, spawnposition, spawnrotation);
            yield return new WaitForSeconds(2f);
            Destroy(instantiatedObj, 5f);
            
        }
        
    }
}
