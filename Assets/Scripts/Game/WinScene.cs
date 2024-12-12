using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    [SerializeField] GameObject catPrefab;
    [SerializeField] SpriteRenderer background;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var c in GameManager.gameState.GetCats()) ConstructCat(c);
    }

    public void CatWander(Cat cat)
    {
        cat.SetWanderBounds(background.bounds.min, background.bounds.max);
    }


    public void ConstructCat(CatSO c){
        Vector2 pos = Vector2.left * UnityEngine.Random.Range(-2f, 2f) + Vector2.right * UnityEngine.Random.Range(-2f, 2f);
        Cat newCat = Instantiate(catPrefab, pos, Quaternion.identity).GetComponent<Cat>();
        newCat.Init(c);
        newCat.GetComponentInChildren<CircleCollider2D>().enabled = false;
        CatWander(newCat);
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * 0.1f), -10);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 15, Time.deltaTime * 0.02f);
    }
}
