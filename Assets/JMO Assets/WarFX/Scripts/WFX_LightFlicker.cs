using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *
 *	(c) 2015, Jean Moreno
**/
[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
    public float time = 0.05f;
    public bool loops = true;

    private float timer;

    void Start()
    {
        timer = time;
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        if (loops)
        {
            while (true)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;

                do
                {
                    timer -= Time.deltaTime;
                    yield return null;
                } while (timer > 0);

                timer = time;
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;

                do
                {
                    timer -= Time.deltaTime;
                    yield return null;
                } while (timer > 0);

                timer = time;
            }
        }
    }
}
