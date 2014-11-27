using UnityEngine;
using System.Collections;

public class TextInputFieldScript : MonoBehaviour
{

	void Start ()
    {
	
	}
	
	void Update () 
    {
	
	}

    public void sendTextToGlobal()
    {
        GlobalScript.lastCommandFromPlayer; 
    }



}
