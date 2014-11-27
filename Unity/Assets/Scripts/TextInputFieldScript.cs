using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInputFieldScript : MonoBehaviour
{
    Text currentInputFieldText;

    void Start()
    {
        currentInputFieldText = gameObject.GetComponent<Text>();
    }

    public string getCommandFromInput()
    {
        return currentInputFieldText.text;
    }

}
