using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceAnimator : MonoBehaviour {


    public Canvas CallIndicator;
    private Animator _anim;

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
        Transform indicator = Instantiate(CallIndicator, transform).transform;
        indicator.GetComponent<Canvas>().worldCamera = Camera.main;
        indicator.LookAt(Camera.main.transform);
        indicator.localPosition += new Vector3(0f, 0.3f, -1f);
    }
}
