using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    
    public class CommandInterpreter : MonoBehaviour 
    {
        enum WaitresesNames
        {
            None,
            Patrycja,
            Doris,
            Marlenka,
            Fiona

        }

        public TextInputFieldScript inputCommandField;

        public Text lastCommandText;

        public InterpreterEngine interpreter;
        private List<List<string>> dictionary;
        private List<List<string>> cookbook;
        public int CookbookSize = 0;

        [SerializeField] 
        public WaitressControllerScript[] waitresses; 

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
            
            switch (commandTranslation[0])
            {
                case 1:
                    sendCommandData(0, commandTranslation);
                    break;
                case 2:
                    sendCommandData(1, commandTranslation);
                    break;
                case 3:
                    sendCommandData(2, commandTranslation);
                    break;
                case 4:
                    sendCommandData(3, commandTranslation);
                    break;
            }
        }

        void sendCommandData(int waitId, List<int> commandTranslation )
        {
            waitresses[waitId].doingSomething = true;
            waitresses[waitId].invokeFunction = commandTranslation[1];
            waitresses[waitId].carryingMeal = commandTranslation[2];
            waitresses[waitId].currentTable = commandTranslation[3] + 1;
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
