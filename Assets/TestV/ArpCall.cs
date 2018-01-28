using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArpCall : MonoBehaviour {

    public float m_CallDuration = 5f;

    private ParticleSystem system;

	// Use this for initialization
	void Start () {
        system = GetComponentInChildren<ParticleSystem>();

        StartCoroutine(Call());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Call()
    {
        system.Play();
        yield return new WaitForSeconds(m_CallDuration);
        system.Stop();
    }
}
