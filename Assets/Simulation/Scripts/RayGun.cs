﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;
    //public Light lightLazer;
    public GameObject m_shotPrefab;

    RaycastHit hit;
    float range = 1000.0f;

    void Start()
    {
       // lightLazer.enabled = false;
    }


    void Update()
    {

        if (Input.GetKey("space"))
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }

    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);



        }

    }



}