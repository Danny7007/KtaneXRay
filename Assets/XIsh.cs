using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class XIsh : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;

    public KMSelectable[] buttons;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    public Transform exish;
    bool scrollUp;
    void Awake()
    {
        moduleId = moduleIdCounter++;
        
        foreach (KMSelectable button in buttons) 
            button.OnInteract += delegate () { ButtonPress(button); return false; };
        scrollUp = UnityEngine.Random.Range(0, 2) == 0;
    }

    void ButtonPress(KMSelectable btn)
    {
        btn.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
        if (moduleSolved)
            return;
        moduleSolved = true;
        Audio.PlaySoundAtTransform("X-RaySolve", transform);
        exish.gameObject.SetActive(false);
        Module.HandlePass();
    }

    void Start()
    {
        Module.OnActivate += () => StartCoroutine(GoExishGo());
    }

    IEnumerator GoExishGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            float duration = 3f, delta = 0;
            while (delta < 1)
            {
                exish.localPosition = new Vector3(0, 0.009f, (scrollUp ? -1 : +1) * Mathf.Lerp(0.08f, -0.08f, delta));
                delta += Time.deltaTime / duration;
                yield return null;
            }
        }
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use <!{0} foobar> to do something.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToUpperInvariant();
        List<string> parameters = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
}