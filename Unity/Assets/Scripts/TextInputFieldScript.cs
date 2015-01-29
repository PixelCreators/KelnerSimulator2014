using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
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
}
