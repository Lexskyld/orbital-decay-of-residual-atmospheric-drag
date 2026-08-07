using UnityEngine;
using System;
using System.IO; 
using System.Collections.Generic; 
public class orbitalvisualization
{
    [Header("Simulation Settings")] 
    public string csvFileName = "satellite_trajectory.csv";
    [Tooltip("Scale down real-world meters to Unity units (e.g., 100,000 meters = 1 Unity unit)")] 
    public float spaceScale = 100000f; 
    public float playbackSpeed = 5f;
    [Header("Scene References")] 
    public Transform earthTransform; 
    private List<Vector3> trajectoryPoints = new(); 
    private int currentPointIndex = 0; 
    private float timer = 0f; 
    private bool isLoaded = false;
    void Start() { 
        LoadTrajectoryData(); 

    void Update() { 
        if (!isLoaded || trajectoryPoints.Count == 0) return;

    timer += Time.deltaTime * playbackSpeed; 
        if (timer >= 1f) { 
            timer = 0f; 
            currentPointIndex++;

    }
}
