using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This only does input stuff, NOTHING else
public class InputManager : MonoBehaviour {
	
    public static InputManager Instance;

    public Action<Vector2> MouseClick;
    public Action<Vector2> MouseRelease;
    
    public Vector2 MousePos { get; private set; }

    void Awake()
    {
	    if (Instance == null) Instance = this; 
	    else Destroy(gameObject);
    }

    private void Update()
    {
	    MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	    
	    if (Input.GetMouseButtonDown(0)) MouseClick?.Invoke(MousePos);
	    if (Input.GetMouseButtonUp(0)) MouseRelease?.Invoke(MousePos);
    }

    void OnDestroy()
    {
    if (this == Instance) Instance = null;
    }
	
}