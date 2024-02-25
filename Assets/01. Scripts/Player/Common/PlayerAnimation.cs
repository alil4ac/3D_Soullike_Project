using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private PlayerController player;

    private readonly int HashParam = Animator.StringToHash("Param");

    private readonly int HashLAction = Animator.StringToHash("LAction");

    private readonly int HashIsTarget = Animator.StringToHash("IsTarget");

    private readonly int HashHorizontal = Animator.StringToHash("Horizontal");

    private readonly int HashVertical = Animator.StringToHash("Vertical");

    private readonly int HashValue = Animator.StringToHash("Value");

    private void Start()
    {
        _anim = this.GetComponent<Animator>();
    }
}
