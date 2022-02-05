using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    CharacterController cr;
    Animator anim;
    public Transform Camera;
    float turnSmoothTime = .1f;
    // Start is called before the first frame update
    float turnSmoothVel;
    public float speed = 4f;
    public float runSpeed = 8f;
    void Start()
    {
        cr = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float ho = Input.GetAxisRaw("Horizontal");
        float ve = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(ho, 0f, ve).normalized;

        if (dir.magnitude >= .1f && !Input.GetKey(KeyCode.LeftShift))
        {
            //mlaku
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            cr.Move(moveDirection.normalized * speed * Time.deltaTime);
            anim.SetFloat("Speed", 1);
        }
        else if (dir.magnitude >= .1f && Input.GetKey(KeyCode.LeftShift))
        {
            //mlayu
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            cr.Move(dir * runSpeed * Time.deltaTime);
            anim.SetFloat("Speed", 2);
        }
        else if (dir.magnitude == 0f)
        {
            //meneng
            anim.SetFloat("Speed", 0);
        }

    }

    private void OnTriggerStay(Collider other) {
        Animator door = other.GetComponentInChildren<Animator>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            door.SetTrigger("Door");
        }
    }
}
