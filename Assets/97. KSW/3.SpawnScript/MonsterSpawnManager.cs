using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public GameObject ghoulPrefab; // Ghoul 몬스터 프리팹
    public GameObject wolfPrefab;
    public GameObject warewolfPrefab;
    public GameObject griffinPrefab;
    public GameObject demonlordPrefab;
    public GameObject wyvernPrefab; // Ghoul 몬스터 프리팹
    public GameObject dragonianPrefab;
    public GameObject dragonPrefab;
    public Transform MonsterSpot;

    void Start()
    {
        // MonsterSpot의 하위 오브젝트들을 순회하며 각각에 맞는 몬스터를 생성합니다.
        foreach (Transform spawnPoint in MonsterSpot)
        {
            if (spawnPoint.name.Equals("ghoul"))
            {
                SpawnGhoul(spawnPoint);
            }
            else if (spawnPoint.name.Equals("wolf"))
            {
                SpawnWolf(spawnPoint);
            }
            else if (spawnPoint.name.Equals("warewolf"))
            {
                SpawnWarewolf(spawnPoint);
            }
            else if (spawnPoint.name.Equals("griffin"))
            {
                SpawnGriffin(spawnPoint);
            }
            else if (spawnPoint.name.Equals("demonlord"))
            {
                SpawnDemonlord(spawnPoint);
            }
            else if (spawnPoint.name.Equals("wyvern"))
            {
                SpawnWyvern(spawnPoint);
            }
            else if (spawnPoint.name.Equals("dragonian"))
            {
                SpawnDragonian(spawnPoint);
            }
            else if (spawnPoint.name.Equals("dragon"))
            {
                SpawnDragon(spawnPoint);
            }
        }
    }

    void SpawnGhoul(Transform spawnPoint)
    {
        // Ghoul 몬스터를 생성합니다.
        GameObject obj =  Instantiate(ghoulPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = ghoulPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnWolf(Transform spawnPoint)
    {
        // wolf 몬스터를 생성합니다.
        GameObject obj = Instantiate(wolfPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = wolfPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnWarewolf(Transform spawnPoint)
    {
        // boss 몬스터를 생성합니다.
        GameObject obj = Instantiate(warewolfPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = warewolfPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnGriffin(Transform spawnPoint)
    {
        // Ghoul 몬스터를 생성합니다.
        GameObject obj = Instantiate(griffinPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = griffinPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnDemonlord(Transform spawnPoint)
    {
        // Ghoul 몬스터를 생성합니다.
        GameObject obj = Instantiate(demonlordPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = demonlordPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnWyvern(Transform spawnPoint)
    {
        // boss 몬스터를 생성합니다.
        GameObject obj = Instantiate(wyvernPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = wyvernPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnDragonian(Transform spawnPoint)
    {
        // Ghoul 몬스터를 생성합니다.
        GameObject obj = Instantiate(dragonianPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = dragonianPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
    void SpawnDragon(Transform spawnPoint)
    {
        // wolf 몬스터를 생성합니다.
        GameObject obj = Instantiate(dragonPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = dragonPrefab.name;
        obj.GetComponent<Enemy>().OnSpawn();
    }
}
