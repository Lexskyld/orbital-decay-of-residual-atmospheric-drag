using UnityEngine;
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class orbitalvisualization : MonoBehaviour
{
    [Header("Simulation Settings")]
    public string csvFileName = "satellite_trajectory.csv";
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
        if (Camera.main != null)
        {
            Camera.main.transform.position = new Vector3(0f, 40f, -150f);
            Camera.main.transform.LookAt(Vector3.zero);
        }
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


        if (currentPointIndex % 100 == 0 && timer == 0f)
        {
            Debug.Log($"Satellite Live Coordinates -> X: {transform.position.x:F2}, Y: {transform.position.y:F2}, Z: {transform.position.z:F2}");
        }
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
                try
                {

                    float x = Convert.ToSingle(values[1], CultureInfo.InvariantCulture) / spaceScale;
                    float y = Convert.ToSingle(values[2], CultureInfo.InvariantCulture) / spaceScale;
                    float z = Convert.ToSingle(values[3], CultureInfo.InvariantCulture) / spaceScale;

 
                    trajectoryPoints.Add(new Vector3(x, z, y));
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Skipping line {i} due to parsing error: {e.Message}");
                }
            }
        }

        Debug.Log($"Successfully loaded {trajectoryPoints.Count} high-precision trajectory coordinates.");
        isLoaded = true;
    }
}
