﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public GameObject boidPrefab;

    public int spawnCount = 100;

    public float spawnRadius = 4.0f;

    [Range(0.1f, 20.0f)]
    public float velocity = 6.0f;

    [Range(0.0f, 0.9f)]
    public float velocityVariation = 0.5f;

    [Range(0.1f, 20.0f)]
    public float rotationCoeff = 4.0f;

    [Range(0.1f, 10.0f)]
    public float neighborDist = 2.0f;

    public LayerMask searchLayer;

    void Start()
    {

        for (var i = 0; i < spawnCount; i++) Agent();


    }

    public GameObject Agent()
    {
        return Spawn(transform.position + Random.insideUnitSphere * spawnRadius);
    }

    public GameObject Spawn(Vector3 position)
    {
        //position = transform.position + Random.insideUnitSphere * spawnRadius;
        var rotation = Quaternion.Slerp(transform.rotation, Random.rotation, 0.3f);
        var boid = Instantiate(boidPrefab, position, rotation) as GameObject;
        boid.GetComponent<BoidMovement>().controller = this;
        return boid;
    }
}
