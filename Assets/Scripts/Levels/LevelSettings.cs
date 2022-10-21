using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary;
using System;

[CreateAssetMenu(fileName = "GenerationSettings", menuName = "Level Generating/Level Generation Setting", order = 1)]
public class LevelSettings : ScriptableObject
{
    
    [field: SerializeField] public string levelName { get; set; }

    [field: SerializeField] public GeneratingConditions[] AllGeneratableObjects { get; set; }
    
    [Serializable]
    public struct GeneratingConditions{
        [field: SerializeField, Range(1, 100)]public int Frequency { get; set; }
        [field: SerializeField]public GeneratableObjectPattern SpawnPattern { get; set; }
        
        
        [field: SerializeReference, SubclassSelector] public SpawnCondition[] SpawnConditions { get; set; }
    }   
}
