using UnityEngine;
using System.Collections;

public class TableScript : MonoBehaviour
{
    //Komponenty
    private Transform transformComponent;

    //Części stolika
    private GameObject peopleModule;
    //private GameObject dishesModule; //To be implemented


    void Awake()
    {
        transformComponent = transform;
        peopleModule = transformComponent.FindChild("People").gameObject;
    }

    void Start()
    {
        peopleModule.SetActive(false);
    }

    void Update()
    {

    }
}
