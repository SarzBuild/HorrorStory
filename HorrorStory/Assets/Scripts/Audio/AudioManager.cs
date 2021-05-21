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
            public static Action<AudioType> StopSound;

            public static Action StartMannequinSound;
            public static Action WalkingAudio;
            public static Action LivingRoomStart;
            public static Action <AudioType, float> PlaySoundAfterDelay;

            public static Action playTVAudio;
            public static Action stopTVAudio;

        }
        public class AudioManager : MonoBehaviour
        {

            private float timeToNextStep = 0.0f;
            public float stepTime = 0.25f;
            public float scaryThemeTime = 0.0f;
            public float tvAudioTime = 0.0f;
            public SpawnMannequin mannequinSpawner;


            private void Awake()
            {

            }
            private void OnEnable()
            {
                AudioAction.PlaySound += PlaySound;
                AudioAction.StopSound += StopSound;

                AudioAction.StartMannequinSound += StartMannequin;
                AudioAction.WalkingAudio += FootstepsAudio;
                AudioAction.LivingRoomStart += EnterLivingRoom;
                AudioAction.PlaySoundAfterDelay += PlaySoundAfterDelay;
                AudioAction.playTVAudio += PlayTVAudio;
                AudioAction.stopTVAudio += StopTVAudio;

            }

            private void OnDisable()
            {
                AudioAction.PlaySound -= PlaySound;
                AudioAction.StopSound -= StopSound;

                AudioAction.StartMannequinSound -= StartMannequin;
                AudioAction.WalkingAudio -= FootstepsAudio;
                AudioAction.LivingRoomStart -= EnterLivingRoom;
                AudioAction.PlaySoundAfterDelay -= PlaySoundAfterDelay;
                AudioAction.playTVAudio -= PlayTVAudio;
                AudioAction.stopTVAudio -= StopTVAudio;


            }

            public void Update()
            {
                
            }
            private void FootstepsAudio()
            {

                timeToNextStep -= Time.deltaTime;
                if (timeToNextStep < 0 && LocationController.currentLocation != LocationController.Location.hallway)
                {
                    //Debug.Log("Footstep");
                    timeToNextStep = stepTime + UnityEngine.Random.Range(-0.1f, 0.1f);
                    int footstep = (int)Mathf.Floor(UnityEngine.Random.Range(0.0f, 4.0f));
                    switch (footstep)
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
            }



            public AudioController audioController;

            public void PlaySound(AudioType audioType)
            {
                audioController.PlayAudio(audioType, false, 0.00f);
            }

            IEnumerator delaySFX(AudioType audioType, float time)
            {
                yield return new WaitForSeconds(time);
                PlaySound(audioType);
            }
            public void PlaySoundAfterDelay(AudioType audioType, float delay)
            {
                StartCoroutine(delaySFX(audioType, delay));
            }
            public void StopSound(AudioType audioType)
            {
                audioController.StopAudio(audioType, false, 0.00f);

            }
            public void LeaveBathroom()
            {
                audioController.StopAudio(AudioType.SFX_Bathroom_Tap, true, 0.00f);
                EnterHallway();
            }
            public void EnterNormalBathroom()
            {
                audioController.PlayAudio(AudioType.bathroomNormal, true, 0.00f);
                LeaveHallway();

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

                //audioController.PlayAudio(AudioType.ST_LivingRoom_Atmosphere, true, 0.0f);
                LeaveHallway();
            }

            public void PlayTVAudio()
            {
                //Sc_PlayerReferences playerRef = GameObject.FindObjectOfType<Sc_PlayerReferences>();
                if (LocationController.currentLocation == LocationController.Location.livingRoom)
                {
                    audioController.PlayAudio(AudioType.TVNoise, true, 0.0f);
                    audioController.SeekAudio(AudioType.TVNoise, tvAudioTime);
                    
                }

            }

            public void StopTVAudio()
            {
                //Sc_PlayerReferences playerRef = GameObject.FindObjectOfType<Sc_PlayerReferences>();
                if (LocationController.currentLocation == LocationController.Location.livingRoom)
                {
                    tvAudioTime = audioController.GetAudioTime(AudioType.TVNoise);
                    audioController.StopAudio(AudioType.TVNoise, false, 0.0f);
                }


            }
            public void LeaveLivingRoom()
            {
                Sc_PlayerReferences playerRef = GameObject.FindObjectOfType<Sc_PlayerReferences>();
                if (playerRef.lensShowing)
                {
                    tvAudioTime = audioController.GetAudioTime(AudioType.TVNoise);
                    audioController.StopAudio(AudioType.TVNoise, true, 0.0f);
                }
                //audioController.StopAudio(AudioType.ST_LivingRoom_Atmosphere, true, 0.0f);
                EnterHallway();
            }

            public void EnterCorridors(int corridor)
            {


                switch(corridor) {
                    case 1:
                        mannequinSpawner.SpeedUpMannequin();

                        LeaveHallway();
                        audioController.PlayAudio(AudioType.Grandfather_thumping, true, 0.0f);
                        break;
                    case 2:
                        mannequinSpawner.SpeedUpMannequin();
                        audioController.PlayAudio(AudioType.SecondClockNoise, true, 0.0f);
                        break;
                    case 3:
                        mannequinSpawner.SpeedUpMannequin();

                        audioController.PlayAudio(AudioType.ThirdClockNoise, true, 0.0f);
                        break;
                    case 4:
                        mannequinSpawner.SpeedUpMannequin();

                        audioController.PlayAudio(AudioType.ForthClockNoise, true, 0.0f);
                        break;
                    case 5:
                        mannequinSpawner.SpeedUpMannequin();

                        audioController.PlayAudio(AudioType.EndCutscene, true, 0.0f); 
                        break;
                }
            

            }

            public void EndOfLevel()
            {
                if (mannequinSpawner != null)
                {
                    audioController.StopAudio(AudioType.Grandfather_thumping, true, 0.0f);
                    audioController.StopAudio(AudioType.SecondClockNoise, true, 0.0f);
                    audioController.StopAudio(AudioType.ThirdClockNoise, true, 0.0f);
                    audioController.StopAudio(AudioType.ForthClockNoise, true, 0.0f);
                    mannequinSpawner.Die();
                }



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