using UnityEngine;
using System.Collections;
using Assets.Scripts;

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
    private CommandInterpreter interpreter;
    
    //Części stolika
    private GameObject peopleModule;
    //private GameObject dishesModule; //To be implemented


    //Zmienne publicze
    public float MinTimeBtwStateChanges, MaxTimeBtwStateChanges;
    
    //Zmienne prywatne
    private bool mealServed = true;

    [SerializeField]
    private int orderName;
    [SerializeField]
    private TableState state;
    [SerializeField]
    private TableStatus status;

    
    void Awake()
    {
        transformComponent = transform;
        peopleModule = transformComponent.FindChild("People").gameObject;
        interpreter = GameObject.Find("Gameplay").GetComponent<CommandInterpreter>();

        state = TableState.Free;
        status = TableStatus.Clean;

    }

    void Start()
    {
        StartCoroutine(UpdateTable());
    }

    void Update()
    {
        /*
         * Testowanie zmiany stanu stolika. Uwaga, testowanie działa na wszystkie stoliki, czyli jeżeli dwa stoliki mają gotowe zamówienie to oba po 
         * wciśnięciu Q będą miały wywołąne AquireOrder
         * 
         * Gdy Zamownienie gotowe - Q aby odebrac
         * 
         * Gdy Czekam na zamowienie - W by podac
         * 
         * Gdy Stolik jest brudny - E aby wyczyscic
         */
        /*
        if (status == TableStatus.OrderReady)
            if (Input.GetKeyDown(KeyCode.Q))
                AquireOrder();

        if (status == TableStatus.WaitingForOrder)
            if (Input.GetKeyDown(KeyCode.W))
                ServeOrder();

        if (status == TableStatus.Dirty)
            if (Input.GetKeyDown(KeyCode.E))
                CleanTable();*/
        
    }

    void setGuestsVisible(bool set)
    {
        if(set)
            state = TableState.Busy;
        else
            state = TableState.Free;
        
        peopleModule.SetActive(set);
    }

    IEnumerator UpdateTable()
    {
        for (;;)
        {
            switch (state)
            {
                case TableState.Free:
                    if (status == TableStatus.Clean)
                    {
                        yield return new WaitForSeconds(GetRandomTime(MinTimeBtwStateChanges, MaxTimeBtwStateChanges));
                        setGuestsVisible(true);
                    }
                    break;
                case TableState.Busy:
                    //TODO: pomyśleć jak wyrzucić to do osobnej funkcji.

                    //TODO: Zmienić stałe na losowe w ostatecznej wersji. Do debbugingu lepsze stałe czasy jednak.
                    if (status == TableStatus.Clean)
                    {
                        yield return new WaitForSeconds(Random.Range(2, 10));
                        status = TableStatus.ChoosingOrder;
                    }
                    else if (status == TableStatus.ChoosingOrder)
                    {
                        yield return new WaitForSeconds(Random.Range(2, 5));
                        ChooseOrder();
                    }
                    else if (status == TableStatus.ProgressingOrder)
                    {
                        yield return new WaitForSeconds(Random.Range(10, 30));
                        setGuestsVisible(false);
                        status = TableStatus.Dirty;
                    }


                    break;
            }
            yield return new WaitForSeconds(.1f);
        }

    }

    IEnumerator ChangeGuestsStatus()
    {
        if (mealServed)
        {
            yield return new WaitForSeconds(GetRandomTime(MinTimeBtwStateChanges, MaxTimeBtwStateChanges));
            peopleModule.SetActive(false);
        }
    }

    float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    void ChooseOrder()
    {   
        //Implementacja losowości zamówienia.
        orderName = Random.Range(0, interpreter.CookbookSize + 1);
        interpreter.setOutput("Stolik " + gameObject.name + " jest gotowy do złożenia zamówienia.");
        status = TableStatus.OrderReady;
    }
    



    /*  Interakcja stolika z kelnerką  */

    public int AquireOrder()
    {
        status = TableStatus.WaitingForOrder;
        return orderName;
    }

    public void ServeOrder(int meal)
    {
        if (meal == orderName)
        {
            interpreter.setOutput("Stolik " + gameObject.name + " przyjął zamówienie i jest w trakcie spożywania.");
            status = TableStatus.ProgressingOrder;
        }
        else
        {
            interpreter.setOutput("Stolik " + gameObject.name + " nie przyjął zamówienia. Błędne zamówienie.");
        }
        
    }

    public void CleanTable()
    {
        status = TableStatus.Clean;
    }


}
