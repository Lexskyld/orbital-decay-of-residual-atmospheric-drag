using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class OrbitVisualizer : MonoBehaviour
{
    [Header("Simulation Settings")]
    public string csvFileName="satellite_trajectory.csv";

    [Tooltip("Scale down real-world meters to Unity units (e.g., 100,000 meters = 1 Unity unit)")]
    public float spaceScale = 100000f;
    public float playbackSpeed = 5f;

    [Header("Scene References")]
    public Transform earthTransform;

    private List<Vector3> trajectoryPoints = new List<Vector3>();
    private int currentPointIndex = 0;
    private float timer = 0f;
    private bool isLoaded = false;
    void Start()
    {
        LoadTrajectoryData();
    }

    void Update()
    {
        if (!isLoaded || trajectoryPoints.Count == 0) return;

       
        timer += Time.deltaTime * playbackSpeed;
        if (timer >= 1f)
        {
            timer = 0f;
            currentPointIndex++;
            if (currentPointIndex >= trajectoryPoints.Count)
            {
                Debug.Log("Orbital Decay Simulation Finished! Satellite has re-entered atmosphere.");
                isLoaded = false;
                return;
            }
        }

        int nextIndex = Mathf.Min(currentPointIndex + 1, trajectoryPoints.Count - 1);
        Vector3 targetPosition = Vector3.Lerp(trajectoryPoints[currentPointIndex], trajectoryPoints[nextIndex], timer);

        
        if (earthTransform != null)
        {
            earthTransform.position = Vector3.zero;
        }
        transform.position = targetPosition;
    }
     void LoadTrajectoryData()
    {
       
        string filePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, csvFileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"CSV data file missing at: {filePath}. Run your Java application first!");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] values = lines[i].Split(',');
            if (values.Length >= 4)
            {
                
                float x = Convert.ToSingle(values[1]) / spaceScale;
                float y = Convert.ToSingle(values[2]) / spaceScale;
                float z = Convert.ToSingle(values[3]) / spaceScale;


}