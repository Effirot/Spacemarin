using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary;
using System;

[CreateAssetMenu(fileName = "GenerationSettings", menuName = "Level Generating/Level Generation Setting", order = 1)]
public class LevelSettings : ScriptableObject
{
    
    [field: SerializeField] public string levelName { get; set; }

    [field: SerializeField] GeneratingConditions[] AllGeneratableObjects { get; set; }
    public GeneratableObjectPattern[] AllPatterns => Array.ConvertAll(AllGeneratableObjects, a=>a.SpawnPattern);
    
    [Serializable]
    struct GeneratingConditions{
        [field: SerializeField, Range(1, 100)]public int Frequency { get; set; }
        [field: SerializeField]public GeneratableObjectPattern SpawnPattern { get; set; }
        
        [field: SerializeReference, SubclassSelector] public SpawnCondition[] SpawnConditions { get; set; }
    }   
}