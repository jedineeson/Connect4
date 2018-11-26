using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSourceMusic;

    [SerializeField]
    private AudioClip m_Music;

    [SerializeField]
    private GameObject m_SFX;

    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {        
        m_AudioSourceMusic.clip = m_Music;
        m_AudioSourceMusic.Play();
    }

    public void PlaySFX(AudioClip aClip, Vector3 aPosition)
    {
        GameObject audio = Instantiate(m_SFX, aPosition, new Quaternion());
        audio.GetComponent<SFXAudio>().Setup(aClip);
        audio.GetComponent<SFXAudio>().Play();
    }
}



