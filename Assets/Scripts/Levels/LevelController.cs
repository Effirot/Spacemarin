using UnityEngine;
using System.Collections;
using System;

using Random = System.Random;
#nullable enable

public class LevelController : MonoBehaviour{
    public static float CurrentProgress = 0.0f;
    public LevelSettings? Pattern;

    public float ProgressPerTick = 0.01f;

    public static Coroutine? routine;
    
    void Start(){
        CurrentProgress = 0.0f;
        if(Pattern == null) throw new ArgumentNullException("Pattern is null");

        routine = StartCoroutine(TickTimer());
    }
    void OnDisable() => StopCoroutine(routine);
    void OnDestroy() => StopCoroutine(routine);

    public void GenerateObjectTick(){
        if(Pattern == null) throw new ArgumentNullException("Pattern is null");

        Pattern.GenerateTick();
    }

    IEnumerator TickTimer(){
        while(Pattern != null){
            CurrentProgress+=ProgressPerTick;
            foreach(var a in Pattern.AllGeneratableObjects) GenerateObjectTick();
            for(int i = 0; i<20; i++) yield return new WaitForFixedUpdate();
        }
    }
}