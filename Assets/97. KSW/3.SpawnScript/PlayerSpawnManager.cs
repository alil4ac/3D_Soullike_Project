using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject cameraPrefab;
    [SerializeField]
    GameObject playerPrefab;


    private GameObject spawnedPlayer;
    private GameObject spawnedCamera;

    void Start()
    {
        // 맵에 플레이어 프리팹이 이미 존재하는지 검사
        spawnedCamera = GameObject.FindWithTag("MainCamera");
        spawnedPlayer = GameObject.FindWithTag("Player");

        if (CharacterManager.Instance.Controller == null)
        {
            spawnedPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);

            spawnedCamera = Instantiate(cameraPrefab, transform.position, Quaternion.identity);
            // PlayerPrefab을 생성하여 씬에 추가합니다.

            // 플레이어 프리팹을 씬 전환 시에도 보존하기 위해 DontDestroyOnLoad를 호출합니다.
            //            DontDestroyOnLoad(spawnedPlayer);
        }

    }
}
