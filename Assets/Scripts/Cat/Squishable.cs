using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squishable : MonoBehaviour
{
    Vector3 origScale;
    float multX = 1;
    float multY = 1;
    float vX = 0;
    float vY = 0;
    public float kX = 2;
    public float kY = 1.5f;

    public float damp = 1f;
    public float clickImpulse = 20;

    float impulse = 0;

    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        origScale = transform.localScale;
        t = transform;
    }

    public void OnMouseEnter()
    {
        impulse = 10;
    }

    public void OnMouseDown()
    {
        impulse = clickImpulse;
    }

    // Update is called once per frame
    void Update()
    {
        float deltime = Mathf.Min(Time.deltaTime, 0.3f);
        impulse = Mathf.Max(0,impulse- deltime * 4);

        float forceX = (1-multX) * kX - damp*vX;
        float forceY = (1-multY) * kY - damp*vY;
        vX += (forceX+impulse)* deltime;
        vY += (forceY+impulse)* deltime;
        multX += vX* deltime;
        multY += vY* deltime;

        transform.localScale = new Vector3(origScale.x * multX, origScale.y * multY, origScale.z);
    }
}
