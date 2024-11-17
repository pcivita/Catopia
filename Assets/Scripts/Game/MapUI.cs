using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public GameObject mapObj;
    public Transform[] nodes;
    public void Open()
    {
        mapObj.SetActive(true);
    }
    public void Close()
    {
        mapObj.SetActive(false);
    }
    private void Update(){
        //do a lil animation for the current node
        nodes[GameManager.instance.gameState.mapNode].localScale = Vector3.one * (Mathf.Sin(Time.timeSinceLevelLoad) + 1.5f);
    }
}
