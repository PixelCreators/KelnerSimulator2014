using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
		List<List<string>> dictionary = new List<List<string>>(interpreter.readFile("słownik.txt"));
		List<List<string>> cookbook = new List<List<string>>(interpreter.readFile("potrawy.txt"));

		Tuple<int, int, int, int> commandTranslation = new Tuple<int, int, int, int>(interpreter.parseInput(lastCommand, dictionary, cookbook));

		// dodam jeszcze obsługę błędów
		// do menedżera kelnerów przesyłamy 4krotkę numerKelnera, numerZadania, numerOpcji, numerStolika
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
