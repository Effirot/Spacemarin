using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary;
using System;
using System.Linq;

using Random = System.Random;

[CreateAssetMenu(fileName = "GenerationSettings", menuName = "Level Generating/Level Generation Setting", order = 1)]
public class LevelSettings : ScriptableObject
{
    
    [field: SerializeField] public string levelName { get; set; }

    [field: SerializeField] public GeneratingConditions[] AllGeneratableObjects { get; set; }
    
    [Serializable]
    public class GeneratingConditions{
        [field: SerializeField] string Tag { get; set; }
        [field: Space]
        [field: SerializeField, Range(1, 100)]public int Frequency { get; set; }
        [field: SerializeField] public GeneratableObjectPattern SpawnPattern { get; set; }

        [SerializeField] int HiddenStamina = 0;
        
        [field: SerializeReference, SubclassSelector] public SpawnCondition[] SpawnConditions { get; set; }

        public void Generate(){
            HiddenStamina += Frequency;

            if(HiddenStamina >= 100){
                if(Array.ConvertAll(SpawnConditions, a=>a.IsChecked).AllTrue()) SpawnPattern.GenerateObject();
                HiddenStamina = 0;    
            }
        }
    }   

    public void GenerateTick(){
        foreach(var obj in AllGeneratableObjects)
            obj.Generate();
    }


}
