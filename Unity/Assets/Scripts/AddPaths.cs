using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class AddPaths : MonoBehaviour
    {

        public string fileName;
        WaitressControllerScript waitress;

        // Use this for initialization
        void Start()
        {
            waitress = gameObject.GetComponent<WaitressControllerScript>();

            int destination = 0;
            StreamReader stream = new StreamReader(Application.dataPath + "/" + fileName);
            while (!stream.EndOfStream)
            {
                string line = stream.ReadLine();
                string[] splitArray = line.Split(' ');
                foreach (string s in splitArray)
                {
                    string tmp = "Waypoint" + s;
                    Transform waypoint = GameObject.Find(tmp).GetComponent<Transform>();
                    waitress.avaliblePaths[destination].pathPoints.Add(waypoint);
                    Debug.Log("Add to destination " + destination + " waypoint: " + tmp);
                }
                destination++;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}