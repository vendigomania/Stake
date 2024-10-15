using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZag : MonoBehaviour
{
    [SerializeField] private float rangeX;

    float direction;

    private void Start()
    {
        ReloadDirection();
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -rangeX, rangeX), transform.position.y);
    }

    private void ReloadDirection()
    {
        direction = -Mathf.Sign(direction) * Random.Range(0.1f, 1f);

        Invoke("ReloadDirection", Random.Range(5f, 12f));
    }
}
