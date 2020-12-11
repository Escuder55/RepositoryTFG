using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //Gun
    [Header("GUN")]
    public float damage = 10f;
    public float bulletSpeed = 100;
    public float CrystalSpeed = 80;
    public Rigidbody bullet;
    public Rigidbody crystal;
    public Transform rightPistol;
    public Transform leftPistol;
    bool canShoot = true;
    bool canShoot2 = true;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float fireRate2 = 0.8f;
    float timerToShoot, timerToShoot2;

    // Start is called before the first frame update
    void Start()
    {
        timerToShoot = fireRate;
        timerToShoot2 = fireRate2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            ShootNegative();
        }
        if (Input.GetButtonDown("Fire2") && canShoot2)
        {
            ShootPositive();
        }
    }
    private void FixedUpdate()
    {
        if (!canShoot)
        {
            timerToShoot -= Time.fixedDeltaTime;
            if (timerToShoot <= 0)
            {
                canShoot = true;
                timerToShoot = fireRate;
            }
        }

        if (!canShoot2)
        {
            timerToShoot2 -= Time.fixedDeltaTime;
            if (timerToShoot2 <= 0)
            {
                canShoot2 = true;
                timerToShoot2 = fireRate2;
            }
        }
    }
    void ShootNegative()
    {
        canShoot = false;
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, rightPistol.transform.position, rightPistol.transform.rotation);
        bulletClone.velocity = transform.forward * bulletSpeed;
    }

    void ShootPositive()
    {
        canShoot2 = false;
        Rigidbody bulletClone2 = (Rigidbody)Instantiate(crystal, leftPistol.transform.position, leftPistol.transform.rotation);
        bulletClone2.velocity = transform.forward * CrystalSpeed;
    }
}
