using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    
    public class CommandInterpreter : MonoBehaviour 
    {
        public TextInputFieldScript inputCommandField;

        public Text lastCommandText;

        public InterpreterEngine interpreter;

        private string lastCommand;

        void Awake()
        {
            inputCommandField = GameObject.Find("InputCommandFieldText").GetComponent<TextInputFieldScript>();
            lastCommandText = GameObject.Find("LastCommand").GetComponent<Text>();
        }

        public void interpretCommand()
        {
            getCommandFromInput();
            invokeCommand();
            showLastCommand();
        }

        public void invokeCommand()
        {
			Debug.Log("debug : new interpreter");
            interpreter = new InterpreterEngine();

			Debug.Log("debug : parse dictionary");
            List<List<string>> dictionary = new List<List<string>>(interpreter.readFile("słownik.txt"));

			Debug.Log("debug : parse cookbook");
            List<List<string>> cookbook = new List<List<string>>(interpreter.readFile("potrawy.txt"));


            List<int> commandTranslation = new List<int>();
			commandTranslation = interpreter.parseInput(lastCommand, dictionary, cookbook);

            // dodam jeszcze obsługę błędów
        }

        void showLastCommand()
        {
            lastCommandText.text = lastCommand;
        }

        void getCommandFromInput()
        {
            lastCommand = inputCommandField.getCommandFromInput();
        }
    }
}
