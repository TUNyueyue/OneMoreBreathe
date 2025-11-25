using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;
    float minDistance = 0.1f;
    int maxPoints = 1000;
    List<Vector3> points = new List<Vector3>();
    Vector3 lastAddPoint;
    event Action theLineRunOut;
    bool isRunOut;

    Vector3 startP;
    Vector3 endP;
    Rigidbody2D rb;
    void Start()
    {
        isRunOut = false;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, this.transform.position);
        lastAddPoint = this.transform.position;
        startP = this.transform.position;
        points.Add(this.transform.position);
    }


    void Update()
    {
        if (isRunOut) return;
        if (Vector3.Distance(lastAddPoint, this.transform.position) > minDistance)
        {
            if (points.Count > maxPoints)
            {
                //points.RemoveAt(0);
                isRunOut = true;
                endP = this.transform.position;
                theLineRunOut?.Invoke();
            }
            lastAddPoint = this.transform.position;
            points.Add(this.transform.position);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }

    }

    void PullBackTheLine()
    {

        StartCoroutine("MoveToStart");



    }

    IEnumerator MoveToStart()
    {
        while (true)
        {


            yield return null;
        }
    }
}
