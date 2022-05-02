using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tentacle : MonoBehaviour
{
    [SerializeField] private int length;
    [SerializeField] private Vector3[] segmentPoses;
    [SerializeField] private Vector3[] segmentV;
    
    [SerializeField] private Transform targetDir;
    [SerializeField] private float targetDist;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float trailSpeed;
    
    [SerializeField] private bool isWiggle;
    [SerializeField] private float wiggleSpeed;
    [SerializeField] private float wiggleMagnitude;
    [SerializeField] private Transform wiggleDir;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void Update()
    {
        if (isWiggle)
            wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(
                segmentPoses[i], 
                segmentPoses[i - 1] + targetDir.right * targetDist, 
                ref segmentV[i], 
                smoothSpeed + i / trailSpeed);
        }
        
        _lineRenderer.SetPositions(segmentPoses);
    }
}
