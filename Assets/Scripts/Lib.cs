using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = System.Random;

using ClassLibrary;

#nullable enable

namespace ClassLibrary{

    [Serializable]
    public class GeneratableObjectPattern{
        static Random ValueGenerator = new Random();

        [field: Space]
        [field: Header("GameObject prefab")]
        [field: SerializeField] public GameObject? GenerationPrefab { get; set; }
        static GameObject? _LastCreated = null; 
        public static GameObject? LastCreated { get => _LastCreated; set { _LastCreated = value; LastGeneratedObject.Invoke(value); } } 
        public static UnityEvent<GameObject?> LastGeneratedObject = new UnityEvent<GameObject?>();

        [field: SerializeField] public Mesh[] PossibleMeshes { get; set; } = new Mesh[] {};
        public Mesh? GetMesh(){
            try { return PossibleMeshes[ValueGenerator.Next(0, PossibleMeshes.Length)]; }
            catch { } 
            return null; 
        }

        [field: SerializeField] public Vector2[] PossibleSpawnPositions { get; set; } = new Vector2[] {};
        public Vector2? GetPosition (){
            try { return PossibleSpawnPositions[ValueGenerator.Next(0, PossibleSpawnPositions.Length)]; }
            catch { } 
            return null; 
        }

        [field: Space]
        [field: SerializeField] public Vector2 PossibleSpawnForceVector1 { get; set; }
        [field: SerializeField] public Vector2 PossibleSpawnForceVector2 { get; set; }
        public Vector2 GetVector() => PossibleSpawnForceVector1 + ((PossibleSpawnForceVector1 - PossibleSpawnForceVector2) * (float)ValueGenerator.NextDouble());

        [field: Space]
        [field: SerializeField, Range(1, 500)] public int MaxSizePercent { get; set; } = 100;
        [field: SerializeField, Range(1, 500)] public int MinSizePercent { get; set; } = 50;
        public Vector3 GetSize(){
            if(MaxSizePercent < MinSizePercent) throw new ArgumentException("MaxSizePercent must be less than MinSizePercent");
            return Vector3.one * (ValueGenerator.Next(MinSizePercent, MaxSizePercent) / 100f);
        }
        
        [field: Space]
        [field: SerializeField] public bool ModelRandomRotation { get; set; }
        
        public void GenerateObject(){
            if(MaxSizePercent < MinSizePercent) throw new ArgumentException("MaxSizePercent must be less than MinSizePercent");
            if(GenerationPrefab == null) throw new ArgumentException("Prefab was not set");
            
            LastCreated = GameObject.Instantiate(
                GenerationPrefab, 
                GetPosition() ?? new Vector2(0, 0), 
                Quaternion.identity);

            lock(LastCreated)
            {
                var mesh = GetMesh();
                var rb = LastCreated.GetComponent<Rigidbody2D>();

                LastCreated.transform.localScale = (ValueGenerator.Next(MinSizePercent, MaxSizePercent) / 100f) * Vector3.one;

                if(mesh != null) LastCreated.GetComponent<MeshFilter>().mesh = mesh;
                if(rb != null) {
                    rb.AddForce(GetVector() * 100);
                    if(ModelRandomRotation)
                        rb.AddTorque(ValueGenerator.Next(-1000, 1000));
                }            
            }
            Debug.Log("Spawned");
            
            
        }
    }   


    [Serializable] public abstract class SpawnCondition{
        public abstract bool IsChecked { get; }
        public static implicit operator bool(SpawnCondition condition) => condition.IsChecked;

        public float CurrentLevelProgress => LevelController.CurrentProgress;
    }
    [Serializable] public class SpawnBefore : SpawnCondition{
        [field: SerializeField, Range(0, 100)] public int BeforeProgress { get; set; }

        public override bool IsChecked => CurrentLevelProgress <= BeforeProgress;
    }    
    [Serializable] public class SpawnAfter : SpawnCondition{
        [field: SerializeField, Range(0, 100)] public int AfterProgress { get; set; }

        public override bool IsChecked => CurrentLevelProgress > AfterProgress;
    }








}
