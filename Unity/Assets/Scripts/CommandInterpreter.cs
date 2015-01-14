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
        private List<List<string>> dictionary;
        private List<List<string>> cookbook;
        public int CookbookSize = 0;

        private string lastCommand;

        void Awake()
        {
            inputCommandField = GameObject.Find("InputCommandFieldText").GetComponent<TextInputFieldScript>();
            lastCommandText = GameObject.Find("LastCommand").GetComponent<Text>();
        }

        void Start()
        {
            Debug.Log("debug : new interpreter");
            interpreter = new InterpreterEngine();

            Debug.Log("debug : parse dictionary");
            dictionary = new List<List<string>>(interpreter.readFile("słownik.txt"));

            Debug.Log("debug : parse cookbook");
            cookbook = new List<List<string>>(interpreter.readFile("potrawy.txt"));
            CookbookSize = cookbook.Count;
        }

        public void interpretCommand()
        {
            getCommandFromInput();
            invokeCommand();
            showLastCommand();

        }

        public void invokeCommand()
        {
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
