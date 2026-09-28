using UnityEngine;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;
    float speed = 15;
    float rotSpeed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    void rotateTurret()
    {
        float? angle = CalculateAngle(true);
        if (angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);;
        }
    }

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrtRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrtRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrtRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
            {
                return null;
            }

    }

    /*Vector3 calculateTrajectory()
    {
        Vector3 p = enemy.transform.position - this.transform.position;
        Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
        float s = bullet.GetComponent<MoveShell>().speed;

        float a = Vector3.Dot(v, v) - s * s;
        float b = Vector3.Dot(p, v);    
        float c = Vector3.Dot(p, p);
        float d = b * b - a * c;

        if (d < 0.1f)
        {
            return Vector3.zero;
        }

        float sqrt = Mathf.Sqrt(d);
        float t1 = (-b - sqrt) / c;
        float t2 = (-b + sqrt) / c;

        float t = 0;

        if (t1 < 0 && t2 < 0)
        {
            return Vector3.zero;
        }
        else if (t1 < 0)
        {
            t = t2;
        }
        else if (t2 < 0)
        {
            t = t1;
        }
        else
        {
            t = Mathf.Max(new float[] {t1, t2});
        }
        return t * p + v;
    }*/

    // Update is called once per frame
    void Update()
    {
        Vector3 diretion = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(diretion.x, 0, diretion.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        rotateTurret();
        if( Input.GetKeyDown(KeyCode.Space))
        {
            //Vector3 aimAt = calculateTrajectory();
            //if (aimAt != Vector3.zero)
            //{
                //this.transform.forward = aimAt;
                CreateBullet();
            //}
        }
    }
}
