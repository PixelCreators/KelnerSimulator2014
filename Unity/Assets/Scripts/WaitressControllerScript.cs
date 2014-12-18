using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaitressControllerScript : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;


    public float speed = 1.5f;
    public float rotationSpeed = 2.0f;

    public List<Path> avaliblePaths;

    private float step;
    private float angle;
    private string waitressName;

    public Transform currentTargetObject;

    void Awake()
    {
        rigidbodyComponent = rigidbody;
    }

    void Start()
    {
        
    }

    void Update()
    {
        moveTowards();
    }

    
    public bool moveTowards()
    {

        //Zwraca true gdy kelnerka dotrze do docelowego obiektu.
        if (!isPositionEquals())
        {
            changeObjectRotation();
            changeObjectPosition();
            return false;
        }
        return true;
    }

    private void changeObjectPosition()
    {
        step = speed * Time.deltaTime;
        rigidbodyComponent.position = Vector3.MoveTowards(rigidbodyComponent.position, currentTargetObject.position, step);
    }

    private void changeObjectRotation()
    {
        angle = rotationSpeed * Time.deltaTime;
        rigidbodyComponent.rotation = Quaternion.Slerp(rigidbodyComponent.rotation, Quaternion.LookRotation(currentTargetObject.position - rigidbodyComponent.position), angle);
    }

    bool isPositionEquals()
    {
        return Vector3.Distance(rigidbodyComponent.position, currentTargetObject.position) <= 1;
    }

    void setCurrentTarget(Transform newTarget)
    {
        currentTargetObject = newTarget;
    }


}

