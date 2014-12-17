using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{

    //Z którejś prelekcji pamiętam, że przyrównywanie jednego Vectora do drugiego działa nieoptymalnie i lepiej przyrównać wszystkie parametry.
    public static bool isVectorsEquals(Vector2 first, Vector2 second)
    {
        if (first.x == second.x && first.y == second.y)
            return true;
        return false;
    }

    public static bool isVectorsEquals(Vector3 first, Vector3 second)
    {
        if (first.x == second.x && first.y == second.y && first.z == second.z)
            return true;
        return false;
    }

    public static bool isVectorsEquals(Vector4 first, Vector4 second)
    {
        if (first.x == second.x && first.y == second.y && first.z == second.z && first.w == second.w)
            return true;
        return false;
    }




}
