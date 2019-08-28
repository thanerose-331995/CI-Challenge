using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public static int damage;
    private Rigidbody rb;
    public static string hitter;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < 2)
        {
            Destroy();
        }
        if (gameObject.transform.position.y > 4)
        {
            Destroy();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy();
    }

    public void setDamage(int dmg)
    {
        damage = dmg;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
        FightData.next = true;
    }
}
