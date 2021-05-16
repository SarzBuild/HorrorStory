using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace UnityCore
{

    namespace Audio
    {

        public static class AudioAction
        {
            public static Action<AudioType> PlaySound;
        }
        public class AudioManager : MonoBehaviour
        {

            private float timeToNextStep = 0.0f;
            public float stepTime = 0.35f;
            public float scaryThemeTime = 0.0f;

            private void Awake()
            {
                StartMannequin();

            }
            private void OnEnable()
            {
                AudioAction.PlaySound += PlaySound;
            }

            private void OnDisable()
            {
                AudioAction.PlaySound -= PlaySound;

            }
            private void Update()
            {
                //check if player is walking 
                bool playerIsWalking = true; 
                
                if(playerIsWalking)
                {
                    timeToNextStep -= Time.deltaTime;
                    if (timeToNextStep < 0 && LocationController.currentLocation != LocationController.Location.hallway)
                    {
                        Debug.Log("Footstep");
                        timeToNextStep = stepTime + UnityEngine.Random.Range(-0.1f, 0.1f);
                        int footstep =  (int) Mathf.Floor(UnityEngine.Random.Range(0.0f, 4.0f));
                        switch(footstep)
                        {
                            case 0:
                                audioController.PlayAudio(AudioType.Footstep);
                                break;
                            case 1:
                                audioController.PlayAudio(AudioType.Footstep2);
                                break;
                            case 2:
                                audioController.PlayAudio(AudioType.Footstep3);
                                break;
                            case 3:
                                audioController.PlayAudio(AudioType.Footstep4);
                                break;
                        }
                    }
                } else
                {
                    timeToNextStep = 0.0f;
                }


            }

            public AudioController audioController;

            public void PlaySound(AudioType audioType)
            {
                audioController.PlayAudio(audioType, false, 0.00f);
            }
            public void LeaveBathroom()
            {
                audioController.StopAudio(AudioType.SFX_Bathroom_Tap, true, 0.00f);
                EnterHallway();
            }

            public void EnterBathroom()
            {
                audioController.PlayAudio(AudioType.SFX_Bathroom_Tap, true, 0.00f);
                LeaveHallway();
            }

            public void StartingAudio()
            {

            }

            private void StartGeneralAtmosphere()
            {
                audioController.PlayAudio(AudioType.Overall_Atmosphere, true, 0.0f);
            }
            private void StopGeneralAtmosphere()
            {
                audioController.StopAudio(AudioType.Overall_Atmosphere, true, 0.0f);
            }
            public void EnterLivingRoom()
            {
                audioController.PlayAudio(AudioType.TVNoise, true, 0.0f);
                //audioController.PlayAudio(AudioType.ST_LivingRoom_Atmosphere, true, 0.0f);
                LeaveHallway();
            }

            public void LeaveLivingRoom()
            {
                audioController.StopAudio(AudioType.TVNoise, true, 0.0f);
                //audioController.StopAudio(AudioType.ST_LivingRoom_Atmosphere, true, 0.0f);
                EnterHallway();
            }

            public void EnterCorridors()
            {
                LeaveHallway();
                audioController.PlayAudio(AudioType.Grandfather_thumping, true, 0.0f);

            }

            public void LeaveCorridors()
            {
                EnterHallway();
                audioController.StopAudio(AudioType.Grandfather_thumping, true, 0.0f);
            }
            private void EnterHallway()
            {
                scaryThemeTime = audioController.GetAudioTime(AudioType.Overall_Atmosphere);
                audioController.PlayAudio(AudioType.SFX_Into_Hallway_Transition, false, 0.0f);
                StopGeneralAtmosphere();

            }
            private void LeaveHallway()
            {
                audioController.SeekAudio(AudioType.Overall_Atmosphere, scaryThemeTime);
                audioController.StopAudio(AudioType.SFX_Into_Hallway_Transition, true, 0.0f);
                StartGeneralAtmosphere();
            }
            public void StartMannequin()
            {
                audioController.PlayAudio(AudioType.MannequinNoise, true, 0.0f);
            }
        }
    }
}