using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public float arrowSpeed;
    public float arrowInterval;

    private float arrowIntervalCurr;
    //float step;
    [SerializeField]
    private GameObject animatedArrow;

    private GameObject animatedArrowObj;

    //private int currentTargetID = 1;

    [HideInInspector]
    public List<GameObject> routingPoints;

    private LineRenderer line;

    private Gradient gradient;

    private GradientColorKey[] colorkeys;
    private GradientAlphaKey[] alphakeys;

    void Start()
    {
        arrowSpeed = 5;
        arrowInterval = 3;
        arrowIntervalCurr = arrowInterval;
        int index = 0;
        foreach (Transform child in transform)
        {
            
            if (child.tag == "RoutePoint")
            {
                child.name = index.ToString();
                routingPoints.Add(child.gameObject);
            }

            index++;
        }
        animatedArrowObj = Instantiate(animatedArrow, routingPoints[0].transform.position, Quaternion.identity, transform);

        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.2f;
        line.positionCount = routingPoints.Count;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        gradient = new Gradient();

        colorkeys = new GradientColorKey[3];
        alphakeys = new GradientAlphaKey[3];

        colorkeys[0].color = c1;
        colorkeys[0].time = 0.0f;
        colorkeys[1].color = c2;
        colorkeys[1].time = 0.025f;
        colorkeys[2].color = c1;
        colorkeys[2].time = 0.05f;

        alphakeys[0].alpha = 1.0f;
        alphakeys[0].time = 0.0f;
        alphakeys[1].alpha = 1.0f;
        alphakeys[1].time = 0.025f;
        alphakeys[2].alpha = 1.0f;
        alphakeys[2].time = 0.05f;

        gradient.SetKeys(colorkeys, alphakeys);

        line.colorGradient = gradient;

        for (int i = 0; i < routingPoints.Count; i++)
        {
            line.SetPosition(i, new Vector3(routingPoints[i].transform.position.x, routingPoints[i].transform.position.y, routingPoints[i].transform.position.z));
        }
    }

    float t = 0f;

    [SerializeField]
    private float duration;

    void Update()
    {
        arrowIntervalCurr -= Time.deltaTime;
        if (arrowIntervalCurr <= 0)
        {
            SpawnArrow();
            arrowIntervalCurr = arrowInterval;
        }

        /*
        animatedArrowObj.transform.position = Vector3.MoveTowards(animatedArrowObj.transform.position, routingPoints[currentTargetID].transform.position, step);
        animatedArrowObj.transform.LookAt(routingPoints[currentTargetID].transform.position);

        if (animatedArrowObj.transform.position == routingPoints[currentTargetID].transform.position)
        {
            currentTargetID++;
        }

        if (currentTargetID == routingPoints.Count)
        {
            Destroy(animatedArrowObj);
            currentTargetID = 1;
            animatedArrowObj = Instantiate(animatedArrow, routingPoints[0].transform.position, Quaternion.identity);
        }
        */


        if (colorkeys[0].time <= 1f)
        {
            colorkeys[0].time += 0.0025f;
            colorkeys[1].time += 0.0025f;
            colorkeys[2].time += 0.0025f;
            alphakeys[0].time += 0.0025f;
            alphakeys[1].time += 0.0025f;
            alphakeys[2].time += 0.0025f;
        }
        else
        {
            colorkeys[0].time = 0f;
            colorkeys[1].time = 0.025f;
            colorkeys[2].time = 0.05f;
            alphakeys[0].time = 0f;
            alphakeys[1].time = 0.025f;
            alphakeys[2].time = 0.05f;
        }

        gradient.SetKeys(colorkeys, alphakeys);
        line.colorGradient = gradient;

        /*
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        var t = Time.time;
        for (int i = 0; i < routingPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
        }
        */
    }

    void SpawnArrow()
    {
        Instantiate(animatedArrow, routingPoints[0].transform.position, Quaternion.identity, transform);
    }
}