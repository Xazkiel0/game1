using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject NPCTarget;
    public Camera m_Camera;
    public Slider HealthBarUI;
    public PlayerHealth EnemyHealth;
    private RectTransform rt;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        //m_Camera = GameObject.FindGameObjectWithTag("Main Camera").GetComponent<Camera>();
        rt = GetComponent<RectTransform>();
        HealthBarUI.maxValue = EnemyHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarUI.value = EnemyHealth.health;
        if (NPCTarget)
        {
            pos = RectTransformUtility.WorldToScreenPoint(m_Camera, NPCTarget.transform.position);
            rt.position = pos;
        }
        if (HealthBarUI.value == 0)
        {
            Destroy(gameObject);
        }
    }
}
