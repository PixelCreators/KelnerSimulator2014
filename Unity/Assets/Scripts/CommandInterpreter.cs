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
        private List<WaitressControllerScript> waitreses; 

        private string lastCommand;

        void Awake()
        {
            inputCommandField = GameObject.Find("InputCommandFieldText").GetComponent<TextInputFieldScript>();
            lastCommandText = GameObject.Find("LastCommand").GetComponent<Text>();
        
            waitreses.Add(GameObject.Find("Patrycja").GetComponent<WaitressControllerScript>());
            waitreses.Add(GameObject.Find("Doris").GetComponent<WaitressControllerScript>());
            waitreses.Add(GameObject.Find("Marlenka").GetComponent<WaitressControllerScript>());
            waitreses.Add(GameObject.Find("Fiona").GetComponent<WaitressControllerScript>());
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
            
            switch (getNameFromInt(commandTranslation[0]))
            {
                case WaitresesNames.Patrycja:
                    sendCommandData(0, commandTranslation);
                    break;
                case WaitresesNames.Doris:
                    sendCommandData(1, commandTranslation);
                    break;
                case WaitresesNames.Marlenka:
                    sendCommandData(2, commandTranslation);
                    break;
                case WaitresesNames.Fiona:
                    sendCommandData(3, commandTranslation);
                    break;
            }
            // dodam jeszcze obsługę błędów
        }

        void sendCommandData(int waitId, List<int> commandTranslation )
        {
            waitreses[waitId].invokeFunction = commandTranslation[2];
            waitreses[waitId].carryingMeal = commandTranslation[3];
            waitreses[waitId].currentTable = commandTranslation[4];
        }

       void showLastCommand()
        {
            lastCommandText.text = lastCommand;
        }

        void getCommandFromInput()
        {
            lastCommand = inputCommandField.getCommandFromInput();
        }

        WaitresesNames getNameFromInt(int number)
        {
            switch (number)
            {
                case 1:
                    return WaitresesNames.Patrycja;
                case 2:
                    return WaitresesNames.Doris;
                case 3:
                    return WaitresesNames.Marlenka;
                case 4:
                    return WaitresesNames.Fiona;
                default:
                    Debug.Log("There's no such waitress!");
                    break;
            }
            return WaitresesNames.None;
        }
    }
}
