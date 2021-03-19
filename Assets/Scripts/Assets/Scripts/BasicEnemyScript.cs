using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    public GameObject player;
    public float initBC;
    float bulletCountdown;
    public int health;
    public GameObject bullet;
    public Transform bulletParent;
    // Start is called before the first frame update
    void Start()
    {
        bulletCountdown = initBC;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (bulletCountdown <= 0)
        {
            ShootPlayer(bullet);
            bulletCountdown = initBC;
        }
        bulletCountdown -= Time.deltaTime;
    }

    void ShootPlayer(GameObject bullet)
    {
        GameObject BA = Instantiate(bullet, transform.position, transform.rotation, bulletParent);
        BA.GetComponent<Bullet>().target = player;
    }
}
