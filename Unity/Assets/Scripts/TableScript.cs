using UnityEngine;
using System.Collections;

public class TableScript : MonoBehaviour
{
    private enum TableState
    {
        Free,
        Busy
    }

    private enum TableStatus
    {
        ChoosingOrder,
        OrderReady,
        WaitingForOrder,
        ProgressingOrder,
        Dirty,
        Clean,
    }

    //Komponenty
    private Transform transformComponent;

    //Części stolika
    private GameObject peopleModule;
    //private GameObject dishesModule; //To be implemented


    //Zmienne publicze
    public float minTimeAftMealSrvd, maxTimeAftMealServ;
    
    //Zmienne prywatne
    private bool mealServed = true;
    private TableState state;
    private TableStatus status;

    void Awake()
    {
        transformComponent = transform;
        peopleModule = transformComponent.FindChild("People").gameObject;

        state = TableState.Free;
        status = TableStatus.Clean;
    }

    void Start()
    {
        
    }

    void Update()
    {
            
    }

    void setGuestsVisible(bool set)
    {
        peopleModule.SetActive(set);
    }

    IEnumerator ChangeGuestsStatus()
    {
        if (mealServed)
        {
            yield return new WaitForSeconds(getRandomTime(minTimeAftMealSrvd, maxTimeAftMealServ));
            peopleModule.SetActive(false);
        }
    }

    float getRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }


}
