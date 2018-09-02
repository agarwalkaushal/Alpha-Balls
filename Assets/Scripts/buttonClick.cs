using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonClick : MonoBehaviour {

    public string color;
    public static buttonClick bc;

    void Start () {
        if (bc == null)
        {
            //DontDestroyOnLoad(gameObject);
            bc = this;
        }
        else
        {
            if (bc != this)
            {
                Destroy(gameObject);
            }
        }
    }
	
    //To get the color clicked by user at the start scene
    public void onClicked()
    {
        color = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("GameRound"); //after the click the next scene is loaded
    }
}
