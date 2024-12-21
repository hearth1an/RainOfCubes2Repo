using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPlatform _spawnPlatform;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;

    private float _repeatRate = 0.5f;

    private ObjectPool<Cube> _cubePool;

    private WaitForSeconds _delay;
    private Vector3 _platformPosition => _spawnPlatform.transform.position;
    private Vector3 _platformScale => _spawnPlatform.transform.localScale;    

    private void Awake()
    {

        _cubePool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (_cubePrefab) => ActionOnGet(_cubePrefab));
    }

    private void Start()
    {
        float time = 0f;
        InvokeRepeating(nameof(GetCube), time, _repeatRate);
    }
    
    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetRandomPosition();
        cube.CubeDestroyed += CreateBomb;
    }

    private void CreateBomb(Cube cube)
    {
        Instantiate(_bombPrefab).transform.position = cube.transform.position;
        cube.CubeDestroyed -= CreateBomb;
    }
    private void GetCube()
    {
        _cubePool.Get();
    }

    private Vector3 GetRandomPosition()
    {
        int halfDivide = 2;

        float halfX = _platformScale.x / halfDivide;
        float halfZ = _platformScale.z / halfDivide;
        
        float xPosition = _platformPosition.x + UnityEngine.Random.Range(-halfX, halfX);
        float zPosition = _platformPosition.z + UnityEngine.Random.Range(-halfZ, halfZ);

        return new Vector3(xPosition, _platformPosition.y, zPosition);
    }

}
