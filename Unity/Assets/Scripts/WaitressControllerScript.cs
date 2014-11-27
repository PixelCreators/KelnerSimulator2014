using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaitressControllerScript : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotationSpeed = 2.0f;

    //TODO: to poniżej do zmiany. Powinno być  dynamiczne wskazywanie obiektów, na razie jest stałe.
    public Transform targetObject;


    public TextInputFieldScript inputCommandField;

    public Text lastCommandText;

    private float step;
    private float angle;


    void Start()
    {
    }

    void Update()
    {
        //moveTowards(targetObject); //Przesuwanie w stronę obiektu, zakomentowane. Jeśli chcesz przesunąć w stronę punktu odkomentuj.
    }

    public void invokeCommand(string command)
    {
        //TODO: Wyświetlanie komendy na szybko, zrobić osobną funkcję!
        lastCommandText.text = command;
        // TODO: dodać wykonywanie komend przez kelnerkę. 
        if (command.Equals("moveTowards"))
        {
            moveTowards(targetObject);
            Debug.Log("Wykonano polecenie moveTowards!");
        }
    }

    public void interpretCommand()
    {
        /*Funkcja wywoływana po naciśnięciu przycisku wyślij.*/

        invokeCommand(inputCommandField.getCommandFromInput());
    }

    public void moveTowards(Transform targetObject)
    {
        /* Przesuwamy postać o krok równy $speed metrów na sekundę.
         * Obracamy ją także w stronę obiektu o $rotationSpeed metrów na sekundę. */
     
        //Sprawdzanie równości wektorów działa, ale jest nieoptymalne. TODO: zamienić na sprawdzanie float x,y,z.
        if (rigidbody.position != targetObject.position)
        {
            step = speed * Time.deltaTime;
            angle = rotationSpeed * Time.deltaTime;
            rigidbody.position = Vector3.MoveTowards(rigidbody.position, targetObject.position, step);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, Quaternion.LookRotation(targetObject.position - rigidbody.position), angle);
        }
    }
}
