using System;
using System.Collections;
using System.Collections.Generic;
using Runners;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private RunnersGroup runnersGroup;
    private LineRenderer _lineRenderer;
    private Coroutine _drawingCoroutine;
    private readonly List<Vector3> _drawPoints = new();

    private void Awake()
    {
        InitLineRenderer();
    }

    private void InitLineRenderer()
    {
        _lineRenderer = Instantiate(lineRendererPrefab);
        _lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GroupRunners();
            StopDrawing();
        }
    }

    private void StartDrawing()
    {
        if (_drawingCoroutine != null)
        {
            StopDrawing();
        }

        _drawingCoroutine = StartCoroutine(DrawLine());
    }

    private IEnumerator DrawLine()
    {
        while (true)
        {
            Vector3 inputPosition = Input.mousePosition;
            inputPosition.z = 10;
            inputPosition.x = Mathf.Clamp(inputPosition.x, 0, Screen.width);
            inputPosition.y = Mathf.Clamp(inputPosition.y, 0, Screen.height / 3);
            _drawPoints.Add(inputPosition);
            RedrawRenderer();
            yield return null;
        }
    }

    private void RedrawRenderer()
    {
        _lineRenderer.positionCount = _drawPoints.Count;
        for (int i = 0; i < _drawPoints.Count; i++)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(_drawPoints[i]);
            _lineRenderer.SetPosition(i, position);
        }
    }

    private void StopDrawing()
    {
        StopCoroutine(_drawingCoroutine);
        _lineRenderer.positionCount = 0;
        _drawPoints.Clear();
    }

    private void GroupRunners()
    {
        for (var i = 0; i < _drawPoints.Count; i++)
        {
            _drawPoints[i] = new Vector2(
                _drawPoints[i].x / Screen.width,
                _drawPoints[i].y / (Screen.height / 3f));
        }

        runnersGroup.GroupRunners(_drawPoints);
    }
}