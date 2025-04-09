using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Flashlight : MonoBehaviour
{
    Light m_Light;

    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;


    public TMP_Text Text;
    public TMP_Text batteryText;

    public float Lifetime = 100;
    public float batteries = 1;

    public PlayerSanity PS;

    private bool on;
    private bool off;

    private bool enemyInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Light = GetComponent<Light>();
        on = false;
        off = true;

        m_Light.enabled = false;

        StartCoroutine(DamageOverTime());

    }


    void Update()
    {

        Text.text = "Flashlight " + Lifetime.ToString("0") + "%";
        batteryText.text = batteries.ToString();

        m_Light.intensity = Mathf.Clamp(m_Light.intensity, minBrightness, maxBrightness);
        if (drainOverTime == true && m_Light.enabled == true)
        {

            if (m_Light.intensity > minBrightness)
            {
                m_Light.intensity -= Time.deltaTime * (drainRate / 1000);
            }

        }
        if (Input.GetButtonDown("Flashlight") && off)
        {

            m_Light.enabled = true;
            on = true;
            off = false;
        }
        else if (Input.GetButtonDown("Flashlight") && on)
        {
            m_Light.enabled = false;
            on = false;
            off = true;
        }


        if (on)
        {
            Lifetime -= 1 * Time.deltaTime;
        }
        if (Lifetime <= 0)
        {
            m_Light.enabled = false;
            Lifetime = 0;
        }
        if (Lifetime >= 100)
        {
            Lifetime = 100;

        }
        if (Input.GetButtonDown("reload") && batteries >= 1)
        {
            batteries -= 1;
            Lifetime += 50;

        }
        if (Input.GetButtonDown("reload") && batteries == 0)
        {
            return;

        }
        if (batteries <= 0)
        {
            batteries = 0;
        }

        if (enemyInRange)
        {
            DealDamageOverTime();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (on && other.CompareTag("Enemy"))
        {
            NavMeshAgent enemyAgent = other.GetComponent<NavMeshAgent>();
            if (enemyAgent != null)
            {
                enemyAgent.isStopped = true;
            }

        }
        if (off && other.CompareTag("Enemy"))
        {
            enemyInRange = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            NavMeshAgent enemyAgent = other.GetComponent<NavMeshAgent>();
            if (enemyAgent != null)
            {
                enemyAgent.isStopped = false;

            }
            enemyInRange = false;

        }
    }
    private void DealDamageOverTime()
    {
        PS.TakeDamage(1);

    }
    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (enemyInRange && off)
            {
                DealDamageOverTime();
            }
        }
    }
}
