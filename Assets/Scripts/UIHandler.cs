using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _spawnedCubes;
    [SerializeField] private TMP_Text _createdObjects;
    [SerializeField] private TMP_Text _activeObjects;

    private const string Spawned = "Spawned objects: ";
    private const string Created = "Created objects: ";
    private const string Active = "Active objects: ";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
