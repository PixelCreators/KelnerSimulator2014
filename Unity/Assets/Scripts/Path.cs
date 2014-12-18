using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Path
{
    private int currentPath;

    public List<Transform> pathPoints;
    public void setCurrentPath(int newCurrentPath)
    {
        currentPath = newCurrentPath;
    }

    public int getCurrentpath()
    {
        return currentPath;
    }

}

