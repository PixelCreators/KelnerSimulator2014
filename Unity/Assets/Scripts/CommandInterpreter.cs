using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommandInterpreter : MonoBehaviour 
{
    public TextInputFieldScript inputCommandField;

    public Text lastCommandText;

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
        if (lastCommand.Equals("moveTowards"))
        {
            Debug.Log("Wykonano polecenie moveTowards!");
        }


        //(opcja) podmiot (opcja) czynność (opcja + opcja)
        /**

        int iKelner;
        var commandChain:string[] = command.Split(" ");

        if (commandChain[0].Equals("niech")) {
            if(commandChain[1].Equals("każdy") && commandChain[2].Equals("kelner")) {
                iKelner = -1;
            }
            else if(!commandChain[1].Equals("kelner") || !int.TryParse(commandChain[2], out iKelner)) {
                iKelner = 0; // nie można sparsować, w innym wypadku już podstawia
            }
            if(commandChain[3].Equals("zmywaj")) {
                //call kelner zmywaj(iKelner)
            }
            else if(commandChain[3].Equals("poda") && commandChain[5].Equals("do") && commandChain[6].Equals("stołu")) {
                //call kelner danie stół podaj(iKelner, commandChain[4], int.Parse(commandChain[7])
            }
            else if(commandChain[3].Equals("sprzątnie") && commandChain[4].Equals("stół")) {
                //call kelner stół sprzątnij(iKelner, int.Parse(commandChain[5]))
                // jeśli brak id stołu to sprzątnij wszystkie stoły 
            }
            else if(commandChain[3].Equals("odbierze") && commandChain[4].Equals("zamówienie") && commandChain[5].Equals("od") && commandChain[6].Equals("stołu")) {
                //call kelner stół odbierz(iKelner, int.Parse(commandChain[7]))
                // jeśli brak id stołu to odbierz zamówienia ze wszystkich stołów
            }
        }
        /**/
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
