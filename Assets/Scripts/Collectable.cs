using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    private void Start()
    {
        transform.DOJump(new Vector3(transform.position.x, .25f, transform.position.z),1f,1,1f);
    }
}
