using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collision : MonoBehaviour {

    private string letter;
    private static string word;
    public static bool gameOver;
    public static bool gameWon;
    public Text wordNew;
	// Use this for initialization
	void Start () {

        word = background.rword;
        Debug.Log(word);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (word.Length == 0)
        {
            gameWon = true;
            background.gameWon(); //if length of word becomes zero means user has hit all spheres correctly, hence game is won

        }
    }

    //Every time a sphere and our gun collide

    void OnCollisionEnter(Collision col)
    {

        TextMesh textObject = col.gameObject.GetComponentInChildren<TextMesh>();
        letter = textObject.text;
        if(word.Contains(letter))
        {
            int n = word.IndexOf(letter);
            word = word.Remove(n, 1); //removing the hit letter
            wordNew.text = word;
            Debug.Log(word);
        }
        else
        {
            gameOver = true; //user hit one letter incorrectly
            background.gameOver();
            Debug.Log("Game Lost");

        }

        Destroy(col.gameObject); //destory the sphere once the sphere and gun collide to avoid physical ruptures
    }
}
