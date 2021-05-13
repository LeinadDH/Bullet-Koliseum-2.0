using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationSword : MonoBehaviour
{
    public Vector3 rotate;
    RotateMode mode = RotateMode.Fast;
    public float flip;
    public int Loops;
    public LoopType loopType;

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(rotate, flip, mode).SetLoops(Loops, loopType);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
