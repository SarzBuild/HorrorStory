
using UnityEngine;


namespace UnityCore
{

    namespace Audio
    {
        public class TestAudio: MonoBehaviour
        {
            public AudioController audioController;

            #region Unity Functions

#if UNITY_EDITOR

            public void Update()
            {
                if (Input.GetKeyUp(KeyCode.T))
                {
                    audioController.PlayAudio(AudioType.ST_01, true, 1.0f);

                }
                if (Input.GetKeyUp(KeyCode.G))
                {
                    audioController.StopAudio(AudioType.ST_01, true, 1.0f);

                }
                if(Input.GetKeyUp(KeyCode.B))
                {
                    audioController.RestartAudio(AudioType.ST_01, true, 1.0f);
                }


                if (Input.GetKeyUp(KeyCode.T))
                {
                    audioController.PlayAudio(AudioType.SFX_01, true, 1.0f);

                }
                if (Input.GetKeyUp(KeyCode.G))
                {
                    audioController.StopAudio(AudioType.SFX_01, true, 1.0f);

                }
                if (Input.GetKeyUp(KeyCode.B))
                {
                    audioController.RestartAudio(AudioType.SFX_01, true, 1.0f);
                }
            }

#endif
            #endregion
        }
    }
}
