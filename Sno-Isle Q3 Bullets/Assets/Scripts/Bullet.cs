using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region MainVariables
    public float speed;
    public int damage;
    Vector2 mousePos;
    Rigidbody2D rb;
    Vector2 dir;
    public string bulletType;
    public GameObject target;
    public int bounces;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (bulletType == "BasicPlayer")
        {
            Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mousePos = Camera.main.ScreenToWorldPoint(screenPos);
            dir = (mousePos - new Vector2(transform.position.x, transform.position.y));
            dir = dir.normalized;
        }
        if (bulletType == "BasicEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
        }
        if (bulletType == "RicochetEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletType == "BasicPlayer" || bulletType == "BasicEnemy")
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        if (bulletType == "RicochetEnemy")
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.0)
            {
                dir = Vector3.Reflect(dir, Vector3.right);
                bounces--;
            }   
            else if (1.0 < pos.x)
            {
                dir = Vector3.Reflect(dir, Vector3.right);
                bounces--;
            }
            else if (pos.y < 0.0)
            {
                dir = Vector3.Reflect(dir, Vector3.up);
                bounces--;
            }
            else if (1.0 < pos.y)
            {
                dir = Vector3.Reflect(dir, Vector3.up);
                bounces--;
            }
            transform.Translate(dir * speed * Time.deltaTime);
            if (bounces < 0)
            {
                Destroy(gameObject);
            }
            
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == "BasicPlayer")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<BasicEnemyScript>().health -= damage;
            }
        }

        if (bulletType == "BasicEnemy" || bulletType == "RicochetEnemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<BasicPlayerScript>().health -= damage;
            }
        }
        
    }
}
