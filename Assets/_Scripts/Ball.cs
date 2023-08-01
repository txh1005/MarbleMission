using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    public float ballSpeed = 10f;
    Vector3 direction;
    bool isFire;
    private Vector3 initialPosition;
    [SerializeField] ballType ballType;
    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
        initialPosition = transform.position;
        //rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LaunchBall()
    {
        if (!isFire)
        {
            direction = initialPosition;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //rigid.AddForce((mousePosition - direction) * ballSpeed, ForceMode2D.Impulse);
            rigid.velocity = (mousePosition - direction) * ballSpeed;
        }
        isFire = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "destroy")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            Debug.Log(ballType);
        }
    }
}
