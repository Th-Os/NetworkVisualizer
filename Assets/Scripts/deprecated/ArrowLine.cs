using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on: http://answers.unity3d.com/questions/1100566/making-a-arrow-instead-of-linerenderer.html

[RequireComponent(typeof(LineRenderer))]
public class ArrowLine : MonoBehaviour {

    private LineRenderer line;
    void Start()
    {
        Vector3 origin = line.GetPosition(0);
        Vector3 target = line.GetPosition(1);
        line = this.GetComponent<LineRenderer>();
        line.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.4f)
            , new Keyframe(0.9f, 0.4f) // neck of arrow
            , new Keyframe(0.91f, 1f)  // max width of arrow head
            , new Keyframe(1, 0f));  // tip of arrow
        line.SetPositions(new Vector3[] {
            origin
            , Vector3.Lerp(origin, target, 0.9f)
            , Vector3.Lerp(origin, target, 0.91f)
            , target });
    }
}
