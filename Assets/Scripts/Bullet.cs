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
    public int bouncesorWarps;
    public float aliveTime;
    public int explodeAmount;
    public GameObject explodeBulletType;
    public Transform enemyBulletParent;
    public float laserSpeed;
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
        if (bulletType == "ExplosiveEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
            
        }
        if (bulletType == "WarpEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
        }
        if (bulletType == "LaserEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            
            transform.rotation = Quaternion.FromToRotation(transform.position, dir);
            transform.localScale = new Vector3(transform.localScale.x, .1f, 1f);

            Color color = gameObject.GetComponent<SpriteRenderer>().material.color;
            
            color.a = .1f;
            this.GetComponent<SpriteRenderer>().material.color = color;

            transform.position += (transform.localScale.x/2) * -transform.right;
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletType == "BasicPlayer" || bulletType == "BasicEnemy" || bulletType == "ExplosiveEnemy" || bulletType == "WarpEnemy")
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        if (bulletType == "RicochetEnemy")
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.0)
            {
                dir = Vector3.Reflect(dir, Vector3.right);
                bouncesorWarps--;
            }   
            else if (1.0 < pos.x)
            {
                dir = Vector3.Reflect(dir, Vector3.right);
                bouncesorWarps--;
            }
            else if (pos.y < 0.0)
            {
                dir = Vector3.Reflect(dir, Vector3.up);
                bouncesorWarps--;
            }
            else if (1.0 < pos.y)
            {
                dir = Vector3.Reflect(dir, Vector3.up);
                bouncesorWarps--;
            }
            transform.Translate(dir * speed * Time.deltaTime);
            if (bouncesorWarps < 0)
            {
                Destroy(gameObject);
            }
            
        }
        if (bulletType == "ExplosiveEnemy")
        {
            aliveTime -= Time.deltaTime;

            if (aliveTime <= 0)
            {
                for (int i = 0; i < explodeAmount; i++)
                {
                    GameObject BA = Instantiate(explodeBulletType, transform.position, transform.rotation, enemyBulletParent);
                    BA.GetComponent<Bullet>().dir = Quaternion.Euler(0, 0, (360 / explodeAmount) * i) * dir;
                }
                Destroy(gameObject);
            }
        }
        if (bulletType == "WarpEnemy")
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.0)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            else if (1.0 < pos.x)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            else if (pos.y < 0.0)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            else if (1.0 < pos.y)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            transform.Translate(dir * speed * Time.deltaTime);
            if (bouncesorWarps < 0)
            {
                Destroy(gameObject);
            }
        }
        if (bulletType == "LaserEnemy")
        {
            
            Color color = gameObject.GetComponent<SpriteRenderer>().material.color;
            color.a += .1f * laserSpeed;
            this.GetComponent<SpriteRenderer>().material.color = color;
            transform.localScale += new Vector3(0f, .1f, 0f) * laserSpeed;
            if (transform.localScale.y >= 1f)
            {
                transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
                aliveTime -= Time.deltaTime;
                if (aliveTime <= 0)
                {
                    Destroy(gameObject);
                }
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

        if (bulletType == "BasicEnemy" || bulletType == "RicochetEnemy" || bulletType == "ExplosiveEnemy" || bulletType == "WarpEnemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<BasicPlayerScript>().health -= damage;
            }
        }
        if (bulletType == "LaserEnemy" && transform.localScale.y >= 1f)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<BasicPlayerScript>().health -= damage;
            }
        }
    }
}
