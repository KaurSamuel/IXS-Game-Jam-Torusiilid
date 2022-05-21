using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cameracontroller : MonoBehaviour
{
    public GameObject player;
    public float min_x;
    public float max_x;
    public float min_y;
    public float max_y;
    // Use this for initialization
    void Start () {
     
    }
 
    // Update is called once per frame
    void Update () {
        Vector3 offset = transform.position - player.transform.position;
        bool changeInX = false;
        bool changeInY = false;
        Vector3 newCameraPosition = transform.position;
        if (offset.x > 2)
        {
            changeInX = true;
            newCameraPosition.x = ClampedCameraUpdate((player.transform.position.x + offset.x) - (offset.x - 2), "x");
        }
        else if (offset.x < -2)
        {
            changeInX = true;
            newCameraPosition.x = ClampedCameraUpdate((player.transform.position.x + offset.x) - (offset.x + 2), "x");
        }
        if (offset.y > 1)
        {
            changeInY = true;
            newCameraPosition.y = ClampedCameraUpdate((player.transform.position.y + offset.y) - (offset.y - 1), "y");
        }
        else if (offset.y < -1)
        {
            changeInY = true;
            newCameraPosition.y = ClampedCameraUpdate((player.transform.position.y + offset.y) - (offset.y + 1), "y");
        }
        if (!changeInX)
        {
            newCameraPosition.x = ClampedCameraUpdate(transform.position.x, "x");
        }
        if (!changeInY)
        {
            newCameraPosition.y = ClampedCameraUpdate(transform.position.y, "y");
        }
        transform.position = newCameraPosition;
    }

    float ClampedCameraUpdate(float value, string axis)
    {
        if(axis == "x")
        {
            return Math.Clamp(value, min_x, max_x);
        }else if(axis == "y"){
            return Math.Clamp(value, min_y, max_y);
        }else{
            return value;
        }
    }
}
