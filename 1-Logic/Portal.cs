using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int NextSceneIndex;


    private bool IsStayPlayer = false;

    private void OnTriggerStay(Collider other)
    {

        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHit"))
            {
                IsStayPlayer = true;
            }

            if (IsStayPlayer && Input.GetKeyDown(KeyCode.E))
            {
                GameSoundManager.Instance.SetPlayerFx("warming-stone-sound-effect", transform);
                IsStayPlayer = false;
                GameManager.Instance.IsPaused = true;
                GameManager.Instance.IsBossFelled = false;
                UIManager.Instance.InGameFadeIn(NextSceneIndex);
                GameManager.Instance.ClearTargetList();
            }
            else { return; }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHit"))
        {
            IsStayPlayer = false;
        }
        else { return; }
    }
}
