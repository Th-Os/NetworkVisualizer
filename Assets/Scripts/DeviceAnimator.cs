using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceAnimator : MonoBehaviour {


    public Transform CallIndicator;
    private Animator _anim;
    private Transform _indicator;

	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator>();
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("ANIMATE");
            Call();
        }
    }

    public void Call()
    {
        _anim.Play("Call");
        _indicator = Instantiate(CallIndicator, transform).transform;
        _indicator.GetComponent<Canvas>().worldCamera = Camera.main;
        _indicator.LookAt(Camera.main.transform);
        _indicator.localPosition = new Vector3(0f, 0.3f, -1f);
        StartCoroutine(StopCall());
    }

    private IEnumerator StopCall()
    {
        while (_anim.GetCurrentAnimatorStateInfo(0).IsName("Call")) { }
        Destroy(_indicator.gameObject);
        yield return null;
    }
}
