using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSc : MonoBehaviour
{
    public float damagedDuration = 2f;
    public float currDurD = 2f;
    public float currDurR = 1f;
    public int hitCount = 0;
    public int maxHitCount = 3;
    public enum State
    {
        None,
        Damaged,
        Regen
    }
    public State state = State.None;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Regen)
        {
            regen();
        }
        if (state == State.Damaged)
        {
            Tick();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currDurD = damagedDuration;
            currDurR = 1f;
            hitCount++;
            state = State.Damaged;
        }

    }

    void Tick()
    {
        currDurD -= Time.deltaTime;
        if (currDurD <= 0)
        {
            currDurD = damagedDuration;
            state = State.Regen;
            print("Start 2 Regen");
        }
    }
    void regen()
    {
        if (hitCount <= 0)
        {
            state = State.None;
            print("Health is full");
            return;
        }
        currDurR -= Time.deltaTime;
        if (currDurR <= 0)
        {
            currDurR = 1f;
            hitCount--;
        }
    }

}
