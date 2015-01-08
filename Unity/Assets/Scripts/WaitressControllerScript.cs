﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaitressControllerScript : MonoBehaviour
    {
        private Rigidbody rigidbodyComponent;
        private Animation animationComponent;
        public float speed;
        public float rotationSpeed;

        public List<Path> avaliblePaths;

        private float step;
        private float angle;
        private string waitressName;
        private float currentXRotation = 0;

        public Transform currentTargetObject;

        void Awake()
        {
            rigidbodyComponent = rigidbody;
            animationComponent = animation;
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
            rigidbodyComponent.position = Vector3.MoveTowards(rigidbodyComponent.position, currentTargetObject.position, speed * Time.deltaTime);
        }

        private void changeObjectRotation()
        {
            angle = rotationSpeed * Time.deltaTime;
            rigidbodyComponent.rotation = Quaternion.Slerp(rigidbodyComponent.rotation, Quaternion.LookRotation(currentTargetObject.position - rigidbodyComponent.position), angle);
            Vector3 rotationEuler = rigidbodyComponent.rotation.eulerAngles;
            rigidbodyComponent.rotation = Quaternion.Euler(currentXRotation, rotationEuler.y, rotationEuler.z);
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
}

