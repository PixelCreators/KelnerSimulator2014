using UnityEngine;
using System.Collections;

public class WaypointScript : MonoBehaviour
{
    public enum State
    {
        OccupiedByWaitress,
        OccupiedByGuest,
        OccupiedByItem,
        Free
    }

    public State currentState = State.Free;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
}
