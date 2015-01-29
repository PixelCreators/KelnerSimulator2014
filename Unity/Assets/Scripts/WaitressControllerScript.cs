using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;           //Werka

namespace Assets.Scripts
{
    public class WaitressControllerScript : MonoBehaviour
    {
        private enum States
        {
            Waiting,
            Walking,
            GettingOrder,
            Serving,
            Cleaning,
            Washing,
        }

        [SerializeField]
        private States CurrentState = States.Waiting;

        //Zmienne publiczne
        public float speed;
        public float rotationSpeed;
        public List<Path> avaliblePaths;
        public Text StateText;              //Werka

        //Komponenty
        private Rigidbody rigidbodyComponent;
        private Animation animationComponent;
        private Transform currentTargetObject;
        private CommandInterpreter commandInterpreter;

        //Zmienne prywatne
        private float angle;
        [SerializeField]
        private string waitressName;
        private int currentPath;
        private float currentXRotation;
        private bool moveBack;
        private int currentPathPoint;
        [SerializeField]
        private List<TableScript> tables;
        private bool onPosition;
        private bool commandProceed;

        [SerializeField] 
        public int currentTable;
        public int carryingMeal;
        public int invokeFunction;
        public bool doingSomething;

        private void Awake()
        {
            commandInterpreter = GameObject.Find("Gameplay").GetComponent<CommandInterpreter>(); 
            tables.Add(null);       //Brzydkie, ale nie chcialem juz zmieniac kolejnosci stolikow. TODO: Poprawic! 
            for(int i = 1; i <= 9; i++)
                tables.Add(GameObject.Find(i.ToString()).GetComponent<TableScript>());


            rigidbodyComponent = rigidbody;
            animationComponent = animation;
            
            currentPath = 0;
            currentXRotation = 0;
            currentPathPoint = 0;
            moveBack = false;
            onPosition = false;
            commandProceed = false;
            waitressName = gameObject.name;
        }

        private void Start()
        {
            StartCoroutine(UpdateStatus());
        }

        private void Update()
        {
            if(CurrentState == States.Walking)
                walkAlongPath(currentTable);
        }

        IEnumerator UpdateStatus()
        {
            for (;;)
            {
                switch (CurrentState)
                {
                        case States.Waiting:
                        StateText.text = "Oczekuje na polecenie";       //Werka
                            getCommand();
                            break;
                        case States.Walking:
                            if (onPosition && !commandProceed)
                            {
                                StateText.text = "Idę do stolika.";
                                switch (invokeFunction)
                                {
                                    /*
                                     * Funkcje, na razie tak działają, nie mogłem się dobrać do interpretera.
                                     * 
                                     *  1 - odbierze
                                     *  2 - poda
                                     *  3 - sprzątnie
                                     */
                                    case 0:
                                        CurrentState = States.GettingOrder;
                                        StateText.text = "Przyjmuje zamówienie od stolika";        //Werka
                                        aquireOrder();
                                        yield return new WaitForSeconds(3f);
                                        CurrentState = States.Walking;
                                        break;
                                    case 1:
                                        CurrentState = States.GettingOrder;
                                        StateText.text = "Podaje zamówienie do stolika";        //Werka
                                        serveOrder();
                                        yield return new WaitForSeconds(3f);
                                        CurrentState = States.Walking;
                                        break;
                                    case 2:
                                        CurrentState = States.GettingOrder;
                                        StateText.text = "Sprząta stolik";        //Werka
                                        cleanTable();
                                        yield return new WaitForSeconds(3f);
                                        CurrentState = States.Walking;
                                        break;
                                }
                            }
                        break;
                }
                yield return new WaitForSeconds(.1f);
            }
        }

        private void getCommand()
        {
            if (doingSomething)
            {
                CurrentState = States.Walking;
            }
        }

        void aquireOrder()
        {
            commandInterpreter.setOutput(waitressName + " odbiera zamówienie ze stolika " + currentTable);
            tables[currentTable].AquireOrder();
            commandProceed = true;
        }

        void serveOrder()
        {
            commandInterpreter.setOutput(waitressName + " podaje zamówienie do stolika " + currentTable);
            tables[currentTable].ServeOrder(carryingMeal);
            commandProceed = true;
        }

        void cleanTable()
        {

            commandInterpreter.setOutput(waitressName + " czyści stolik " + currentTable);
            tables[currentTable].CleanTable();
            commandProceed = true;
        }

        public bool moveTowards()
        {
            //Zwraca true gdy kelnerka dotrze do docelowego obiektu.
            if (!isPositionEquals())
            {
                animationComponent.Play("Default Take");
                currentXRotation = -90.0f;
                changeObjectRotation();
                changeObjectPosition();
                return false;
            }
            animationComponent.Stop();
            currentXRotation = 0f;
            return true;
        }

        private void changeObjectPosition()
        {
            rigidbodyComponent.position = Vector3.MoveTowards(rigidbodyComponent.position, currentTargetObject.position,
                speed*Time.deltaTime);
        }

        private void changeObjectRotation()
        {
            angle = rotationSpeed*Time.deltaTime;

            rigidbodyComponent.rotation = Quaternion.Slerp(
                rigidbodyComponent.rotation,
                Quaternion.LookRotation(currentTargetObject.position - rigidbodyComponent.position), 
                angle);

            Vector3 rotationEuler = rigidbodyComponent.rotation.eulerAngles;
            rigidbodyComponent.rotation = Quaternion.Euler(currentXRotation, rotationEuler.y, rotationEuler.z);
        }

        private bool isPositionEquals()
        {
            return Vector3.Distance(rigidbodyComponent.position, currentTargetObject.position) <= 1;
        }

        private void setCurrentTarget(Transform newTarget)
        {
            currentTargetObject = newTarget;
        }


        //TODO: Dodać wybieranie ścieżki po jej nazwie. 
        private bool walkAlongPath(int pathNumber)
        {
            setCurrentTarget(avaliblePaths[pathNumber].pathPoints[currentPathPoint]);
            if (moveTowards())
            {
                if (!moveBack && currentPathPoint < avaliblePaths[pathNumber].pathPoints.Count - 1)
                {
                    currentPathPoint++;
                }
                else if (moveBack && currentPathPoint > 0)
                {
                    currentPathPoint--;
                }
                else
                {
                    if (moveBack && currentPathPoint == 0)
                    {
                        CurrentState = States.Waiting;
                        doingSomething = false;
                        commandProceed = false;
                        moveBack = false;
                        onPosition = false;
                        return true;
                    }



                    if(!moveBack)
                        moveBack = true;
                }
            }
            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger");
            if (currentTable.ToString().Equals((other.gameObject.name)))
            {
                onPosition = true;
                Debug.Log("On Position");
            }
        }

    }
}
