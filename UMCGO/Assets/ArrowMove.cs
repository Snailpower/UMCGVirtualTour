using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private int currentTargetID = 1;

    float step;

    LineDrawer parentScript;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = transform.parent.GetComponent<LineDrawer>();
        Debug.Log(parentScript.routingPoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        step = parentScript.arrowSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, parentScript.routingPoints[currentTargetID].transform.position, step);
        transform.LookAt(parentScript.routingPoints[currentTargetID].transform.position);

        if (Vector3.Distance(transform.position, parentScript.routingPoints[currentTargetID].transform.position) <= 0.1f)
        {
            currentTargetID++;
        }

        if (currentTargetID == parentScript.routingPoints.Count)
        {
            Destroy(gameObject);
            currentTargetID = 1;
        }
    }
}
