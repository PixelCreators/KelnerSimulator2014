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
