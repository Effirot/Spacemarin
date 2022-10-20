using UnityEngine;
using System.Collections;
using System;

#nullable enable

public class LevelController : MonoBehaviour{
    public static float CurrentProgress = 0.0f;
    public LevelSettings? Pattern;

    public float ProgressPerTick = 0.001f;

    public static Coroutine? routine;

    void Start(){
        CurrentProgress = 0.0f;
        if(Pattern == null) throw new ArgumentNullException("Pattern is null");

        routine = StartCoroutine(TickTimer());
    }

    IEnumerator TickTimer(){
        CurrentProgress+=ProgressPerTick;
        
        for(int i = 0; i<20; i++)
            yield return new WaitForFixedUpdate();
    }
}