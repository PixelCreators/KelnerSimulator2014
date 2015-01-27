using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartButton()
    {
        Application.LoadLevel("TestLevel");
    }

    public void AuthorsButton()
    {
        Application.LoadLevel("Authors");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        Application.LoadLevel("Menu");
    }
}
