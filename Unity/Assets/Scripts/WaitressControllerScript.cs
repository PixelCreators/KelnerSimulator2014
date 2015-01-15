using System.Collections.Generic;
using UnityEngine;

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

        //Komponenty
        private Rigidbody rigidbodyComponent;
        private Animation animationComponent;
        private Transform currentTargetObject;

        //Zmienne prywatne
        private float step;
        private float angle;
        private string waitressName;
        private int currentPath;
        private float currentXRotation;
        private bool moveBack;
        private int currentPathPoint;

        private void Awake()
        {
            rigidbodyComponent = rigidbody;
            animationComponent = animation;

            currentPath = 0;
            currentXRotation = 0;
            currentPathPoint = 0;
            moveBack = false;
        }

        private void Start()
        {
        }

        private void Update()
        {
            //moveTowards();
            if(CurrentState == States.Walking)
                walkAlongPath(currentPath);
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
            rigidbodyComponent.rotation = Quaternion.Slerp(rigidbodyComponent.rotation,
                Quaternion.LookRotation(currentTargetObject.position - rigidbodyComponent.position), angle);
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
                    //Debug.Log(currentPathPoint);
                }
                else if (moveBack && currentPathPoint > 0)
                {
                    currentPathPoint--;
                }
                else
                {
                    //currentPathPoint = 0;
                    if (moveBack)
                    {
                        return true;
                    }

                    if(currentPathPoint == 0)
                        CurrentState = States.Waiting;

                    if(!moveBack)
                        moveBack = true;
                }
            }
            return false;
        }
    }
}
