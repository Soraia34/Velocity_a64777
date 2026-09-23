using UnityEngine;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void createBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space))
        {
            createBullet();
        }
    }
}
