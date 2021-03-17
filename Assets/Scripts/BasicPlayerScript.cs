using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerScript : MonoBehaviour
{

    #region MainVariables
    public int health;
    public float speed;
    public GameObject bullet;
    Rigidbody2D rb;
    public Transform bulletParent;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement

        if (Input.GetAxis("Horizontal") > 0)
        {
            
            rb.AddForce(new Vector2(speed, 0));
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            
            rb.AddForce(new Vector2(-speed, 0));
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(new Vector2(0, speed));
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            rb.AddForce(new Vector2(0, -speed));
        }
        if (rb.velocity.x > speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (rb.velocity.y > speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        if (rb.velocity.y < -speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        #endregion

        if (Input.GetMouseButtonDown(0))
        {
            Fire(bullet);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Fire(GameObject bullet)
    {
        
        GameObject FB = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation, bulletParent);
    }
}
