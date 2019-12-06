using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManuver : MonoBehaviour
{

    public Vector2 startWait;
    public float dodge;
    public float smoothing;
    public Vector2 manuverTime;
    public Vector2 manuverWait;
    public Boundary boundary;
    public float tilt;

    private float currentSpeed;
    private float targetManuver;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine (Evade());
        currentSpeed = rb.velocity.z;
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManuver = Random.Range(1, dodge) * -Mathf.Sign (transform.position.x);
            yield return new WaitForSeconds(Random.Range (manuverTime.x, manuverTime.y));
            targetManuver = 0;
            yield return new WaitForSeconds(Random.Range (manuverTime.x, manuverTime.y));
        }
    }

    // Update is called once per frame
   void FixedUpdate()
    {
        float newManuver = Mathf.MoveTowards(rb.velocity.x, targetManuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManuver, 0.0f, currentSpeed);
        //rb.position = new Vector3();
      {
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax);
          // 0.0f;
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax);
      }

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
