using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    CharacterController cr;
    float turnSmoothTime = .1f;
    // Start is called before the first frame update
    float turnSmoothVel;
    public float speed = 6f;
    void Start()
    {
        cr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float ho = Input.GetAxisRaw("Horizontal");
        float ve = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(ho, 0f, ve).normalized;

        if (dir.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            cr.Move(dir * speed * Time.deltaTime);
        }

    }
}
