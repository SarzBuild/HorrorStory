using System.Collections;
using UnityEngine;


namespace UnityCore
{
    namespace Audio
    {
        public class AudioController : MonoBehaviour
        {
            //members 

            public static AudioController instance;

            public bool debug;
            public AudioTrack[] tracks; 

            private Hashtable m_AudioTable; //relationship between audio types (key) and audio tracks (value)
            private Hashtable m_JobTable; //relationship between audio types (key) and jobs (value) (Coroutine, IEnumerator)



            [System.Serializable]
            public class AudioObject
            {
                public AudioType type;
                public AudioClip clip;  //Store the actual music/effect
                [Range(0f, 1f)]         //limit the range in the Unity editor
                public float volume = 1.0f;    //Store our volume
                [Range(0.1f, 3f)]       //Limit the Range again
                public float pitch = 1.0f;     // set the picth for our music/effect
                public bool loop = false;// should this sound loop 
            }
                [System.Serializable]
            public class AudioTrack
            {
                public AudioSource source;
                public AudioObject[] audio;

            }

            private class AudioJob
            {
                public AudioAction action;
                public AudioType type;
                public bool fade;
                public float delay;
                public AudioJob(AudioAction _action, AudioType _type, bool _fade, float _delay)
                {
                    action = _action;
                    type = _type;
                    fade = _fade;
                    delay = _delay;
                }


            }
            private enum AudioAction 
            {
                START,
                STOP,
                RESTART,
            }
            #region Unity Functions
            private void Awake()
                {
                    //instance
                    if (!instance)
                    {
                        Configure();
                        
                    }
                }

                private void OnDisable()
                {
                    Dispose();
                }
            #endregion

            #region Public Functions 
                public void PlayAudio(AudioType _type, bool _fade = false, float _delay = 0.0f)
                {
                    AddJob(new AudioJob(AudioAction.START, _type, _fade, _delay));
                }
               
                public void StopAudio(AudioType _type, bool _fade = false, float _delay = 0.0f)
                {
                    AddJob(new AudioJob(AudioAction.STOP, _type, _fade, _delay));

                }

                public void RestartAudio(AudioType _type, bool _fade = false, float _delay = 0.0f)
                {
                    AddJob(new AudioJob(AudioAction.RESTART, _type, _fade, _delay));

                }

                public float GetAudioTime(AudioType _type)
                {
                AudioTrack _track = GetAudioTrack(_type);// (AudioTrack)m_AudioTable[_job.type]
                return _track.source.time;

                }

                public void SeekAudio(AudioType _type, float time)
                    {
                        AudioTrack _track = GetAudioTrack(_type);// (AudioTrack)m_AudioTable[_job.type]
                        _track.source.time = time;
                    }

            #endregion

            #region Private Functions
            private void Configure()
                {
                    instance = this;
                    m_AudioTable = new Hashtable();
                    m_JobTable = new Hashtable();
                    GenerateAudioTable();
                }



                private void GenerateAudioTable()
                {
                    foreach(AudioTrack _track in tracks)
                    {
                        foreach(AudioObject _obj in _track.audio)
                        {
                            if (m_AudioTable.ContainsKey(_obj.type))
                            {
                                LogWarning("You are trying to register audio [" + _obj.type + "] that has already been registered.");
                            } else
                            {
                                m_AudioTable.Add(_obj.type, _track);
                                Log("Registering audio [" + _obj.type + "].");
                            }
                        }
                    }
                }
            private IEnumerator RunAudioJob(AudioJob _job)
            {

                //if (_job.delay != null) yield return _job.delay; // new WaitForSeconds(_job.delay);
                                                                 //yield return new WaitForSeconds(_job.delay);
               // Debug.Log("type " +  _job.action.ToString() + "fade ? " + _job.fade.ToString() + " delay ? " + (_job.delay));


                AudioTrack _track = GetAudioTrack(_job.type);// (AudioTrack)m_AudioTable[_job.type]
                AudioObject _jobSource = GetAudioObjectFromAudioTrack(_job.type, _track);
                //make the footsteps sound different
                if (_job.type == AudioType.Footstep || _job.type == AudioType.Footstep2 || _job.type == AudioType.Footstep3 || _job.type == AudioType.Footstep4)
                {
                    _jobSource.pitch = 1.0f +  Random.RandomRange(-0.4f, 0.4f);
                    _jobSource.volume = 1.0f + _jobSource.volume * Random.Range(-0.25f, 0.25f);
                }
                _track.source.clip = _jobSource.clip;//GetAudioClipFromAudioTrack(_job.type, _track);

                switch (_job.action)
                {
                    case AudioAction.START:
                        _track.source.pitch = _jobSource.pitch;
                        if (!_job.fade)
                        {
                            _track.source.volume = _jobSource.volume;
                        }
                        _track.source.loop = _jobSource.loop;
                        _track.source.Play();
                        break;
                    case AudioAction.STOP:
                       // Debug.Log("Why");

                        if (!_job.fade)
                        {
                            _track.source.Stop();
                        }
                        break;
                    case AudioAction.RESTART:
                        _track.source.Stop();
                        _track.source.Play();

                        break;

                }

                if (_job.fade)
                {
                    float _initial = (_job.action == AudioAction.START || _job.action == AudioAction.RESTART) ? 0.0f : _jobSource.volume;
                    float _target = _initial == 0 ? _jobSource.volume : 0.0f;
                    float _duration = 1.0f; 
                    float _timer = 0.0f;
                   // Debug.Log("initial " + _initial + " target " + _target + " duration: " + _duration + " timer " + _timer );
                    while (_timer <= _duration)
                    {
                        _track.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                        _timer += Time.deltaTime;
                        if (_job.action == AudioAction.STOP) Debug.Log(_target);
                        yield return null;
                    }
                   //Debug.Log("initial " + _initial + " target " + _target + " duration: " + _duration + " timer " + _timer);

                    if (_job.action == AudioAction.STOP)
                    {
                        _track.source.Stop();
                    }

                }

                    m_JobTable.Remove(_job.type);
                    Log("Job Count: " + m_JobTable.Count);

                    yield return null;
                }

                private void AddJob(AudioJob _job)
                {
                    //remove conflicting jobs
                    RemoveConflictingJobs(_job.type);
                //start job
                    Coroutine _jobRunner = StartCoroutine(RunAudioJob(_job));
                    m_JobTable.Add(_job.type, _jobRunner);
                    Log("Starting job on [" + _job.type + "] with operation: " + _job.action);
                }

                private void RemoveJob(AudioType _type)
                {
                    if (!m_JobTable.ContainsKey(_type))
                    {
                        LogWarning("Trying to stop a job [" + _type + "] that isn't running.");
                        return;
                    }
                    Coroutine _runningJob = (Coroutine)m_JobTable[_type];
                    StopCoroutine(_runningJob);
                    m_JobTable.Remove(_type);
                }
                private void RemoveConflictingJobs(AudioType _type)
                {
                    if (m_JobTable.ContainsKey(_type))
                    {
                        RemoveJob(_type);
                    }
                    AudioType _conflictAudio = AudioType.None;
                    AudioTrack _audioTrackNeeded = (AudioTrack)m_AudioTable[_type];

                    foreach (DictionaryEntry _entry in m_JobTable)
                    {
                        AudioType _audioType = (AudioType)_entry.Key;
                        AudioTrack _audioTrackInUse = (AudioTrack) m_AudioTable[_audioType];
                        if (_audioTrackNeeded.source == _audioTrackInUse.source)
                        {
                            _conflictAudio = _audioType;
                        }
                    }
                    if (_conflictAudio != AudioType.None)
                    {
                        RemoveJob(_conflictAudio);
                    }
                }

                public AudioClip GetAudioClipFromAudioTrack(AudioType _type, AudioTrack _track)
                {
                    foreach (AudioObject _obj in _track.audio)
                    {
                        if (_obj.type == _type)
                        {
                            return _obj.clip;
                        }
                    }
                    return null;
                }
                private AudioTrack GetAudioTrack(AudioType _type, string _job = "")
                {
                    if (!m_AudioTable.ContainsKey(_type))
                    {
                        LogWarning("You are trying to <color=#fff>" + _job + "</color> for [" + _type + "] but no track was found supporting this audio type.");
                        return null;
                    }
                    return (AudioTrack)m_AudioTable[_type];
                }
                public AudioObject GetAudioObjectFromAudioTrack(AudioType _type, AudioTrack _track)
                {
                    foreach (AudioObject _obj in _track.audio)
                    {
                        if (_obj.type == _type)
                        {
                            return _obj;
                        }
                    }
                    return null;
                }
                private void Dispose()
                {
                    foreach (DictionaryEntry _entry in m_JobTable)
                    {
                        IEnumerator _job = (IEnumerator)_entry.Value;
                        StopCoroutine(_job);
                    }
                }
                private void Log(string _msg)
                {
                    if (!debug) return;
                    Debug.Log("[Audio Controller]: " + _msg);
                }

                private void LogWarning(string _msg)
                {
                    if (!debug) return;
                    Debug.LogWarning("[Audio Controller]: " + _msg);
                }


            #endregion
        }
    }
}
