using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaitressControllerScript : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotationSpeed = 2.0f;

    public List<Path> avaliblePaths;

    public TextInputFieldScript inputCommandField;
    public Transform targetObject;

    public Text lastCommandText;

    private float step;
    private float angle;

    void Start()
    {
    }

    void Update()
    {
        
        //moveTowards(); //Przesuwanie w stronę obiektu, zakomentowane. Jeśli chcesz przesunąć w stronę punktu odkomentuj.
    }

    public void invokeCommand(string command)
    {
        //TODO: Wyświetlanie komendy na szybko, zrobić osobną funkcję!
        lastCommandText.text = command;
        // TODO: dodać wykonywanie komend przez kelnerkę. 
        if (command.Equals("moveTowards"))
        {
            moveTowards();
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
			}y
			else if(commandChain[3].Equals("odbierze") && commandChain[4].Equals("zamówienie") && commandChain[5].Equals("od") && commandChain[6].Equals("stołu")) {
				//call kelner stół odbierz(iKelner, int.Parse(commandChain[7]))
				// jeśli brak id stołu to odbierz zamówienia ze wszystkich stołów
			}
		}
		/**/
    }

    public void interpretCommand()
    {
        /*Funkcja wywoływana po naciśnięciu przycisku wyślij.*/
        Debug.Log("Interpret");
        invokeCommand(inputCommandField.getCommandFromInput());
    }

    public void moveTowards()
    {
        if(rigidbody.position != targetObject.position)
        {
            step = speed * Time.deltaTime;
            angle = rotationSpeed * Time.deltaTime;
            rigidbody.position = Vector3.MoveTowards(rigidbody.position, targetObject.position, step);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, Quaternion.LookRotation(targetObject.position - rigidbody.position), angle);
        }
    }

    private void changeObjectPositionAndAngle()
    {
        /* Przesuwamy postać o krok równy $speed metrów na sekundę.
         * Obracamy ją także w stronę obiektu o $rotationSpeed metrów na sekundę. */
        
    }

}
