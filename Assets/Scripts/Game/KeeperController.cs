using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeeperController : MonoBehaviour
{
    [SerializeField] private float rangeX;

    public static UnityAction OnKeepBall;

    private float direction = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x) > rangeX && transform.position.x * direction > 0)
        {
            if (transform.position.x > 0f) direction = Random.Range(-2f, -1f);
            else direction = Random.Range(1f, 2f);
        }
        else
        {
            transform.Translate(Vector2.right * direction * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnKeepBall?.Invoke();
    }
}
