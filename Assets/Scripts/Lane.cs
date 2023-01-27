using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject effect;

    public TMPro.TextMeshPro redTextInput;
    public TMPro.TextMeshPro blueTextInput;

    public GameObject normalEffect,
        goodEffect,
        perfectEffect,
        missEffect;
    public GameObject notePrefab;
    public Animator animator;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    public int decididor;

    // Start is called before the first frame update
    void Start()
    {
        if (decididor == 1)
        {
            input = RebidingKeyBlue.inputBlue;
            blueTextInput.text = input.ToString();
        }
        if (decididor == 2)
        {
            input = RebidingKeyRed.inputRed;
            redTextInput.text = input.ToString();
        }
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(
                    note.Time,
                    SongManager.midiFile.GetTempoMap()
                );
                timeStamps.Add(
                    (double)metricTimeSpan.Minutes * 60f
                        + metricTimeSpan.Seconds
                        + (double)metricTimeSpan.Milliseconds / 1000f
                );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (
                SongManager.GetAudioSourceTime()
                >= timeStamps[spawnIndex] - SongManager.Instance.noteTime
            )
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime =
                SongManager.GetAudioSourceTime()
                - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (PauseMenu.gameIsPaused == false)
                {
                    if (Math.Abs(audioTime - timeStamp) < marginOfError)
                    {
                        if (
                            marginOfError - Math.Abs(audioTime - timeStamp) > 0
                            && marginOfError - Math.Abs(audioTime - timeStamp) < 0.02
                        )
                        {
                            ScoreManager.NormalHit();
                            Instantiate(normalEffect);
                        }
                        if (
                            marginOfError - Math.Abs(audioTime - timeStamp) > 0.1
                            && marginOfError - Math.Abs(audioTime - timeStamp) < 0.12
                        )
                        {
                            ScoreManager.NormalHit();
                            Instantiate(normalEffect);
                        }
                        if (
                            marginOfError - Math.Abs(audioTime - timeStamp) > 0.02
                            && marginOfError - Math.Abs(audioTime - timeStamp) < 0.04
                        )
                        {
                            ScoreManager.GoodHit();
                            Instantiate(goodEffect);
                        }
                        if (
                            marginOfError - Math.Abs(audioTime - timeStamp) > 0.08
                            && marginOfError - Math.Abs(audioTime - timeStamp) < 0.1
                        )
                        {
                            ScoreManager.GoodHit();
                            Instantiate(goodEffect);
                        }
                        if (
                            marginOfError - Math.Abs(audioTime - timeStamp) > 0.04
                            && marginOfError - Math.Abs(audioTime - timeStamp) < 0.08
                        )
                        {
                            ScoreManager.PerfectHit();
                            Instantiate(perfectEffect);
                        }
                        //Hit();
                        print($"Hit on {inputIndex} note");
                        Instantiate(effect);
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                }
            }

            if (PauseMenu.gameIsPaused == false)
            {
                if (Input.GetKey(RebidingKeyRed.inputRed))
                {
                    animator.SetBool("batendoEmCima", true);
                }
                if (Input.GetKeyUp(RebidingKeyRed.inputRed))
                {
                    animator.SetBool("batendoEmCima", false);
                }

                if (Input.GetKey(RebidingKeyBlue.inputBlue))
                {
                    animator.SetBool("batendoEmBaixo", true);
                }
                if (Input.GetKeyUp(RebidingKeyBlue.inputBlue))
                {
                    animator.SetBool("batendoEmBaixo", false);
                }
            }

            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Instantiate(missEffect);
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }

    private void Miss()
    {
        ScoreManager.Miss();
    }
}
