using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnGhost : MonoBehaviour
{
    [SerializeField] private Transform feetPosition;
    [SerializeField] private Transform headPosition;
    [SerializeField] private Material GhostMaterial;
    [SerializeField] private VisualEffect GhostParticles;
    [SerializeField] private bool spawn;
    [SerializeField] private float speed;
    [SerializeField] private bool respawn;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _endMusic;

    [SerializeField] private float emissionAmount = 1;
    [SerializeField] private float dissolveAmount = 0;
    [SerializeField] private float colorLerp = 0;

    
    [SerializeField] [ColorUsage(true, true)]
    private Color beforeBadColor;
    [SerializeField] [ColorUsage(true, true)]
    private Color afterBadColor;

    private Color startColor;
    
    
    private float timer;
    void Start()
    {
        dissolveAmount = 0;
        emissionAmount = 1;
        GhostMaterial.SetFloat("_EmissionAmount", emissionAmount);
        GhostMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        GhostMaterial.SetColor("_EmissionColor", beforeBadColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            Spawn();
        }
        if (respawn)
        {
            resetSpawn();
        }
        GhostMaterial.SetFloat("_EmissionAmount", emissionAmount);
        GhostMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        Color lerp = Color.Lerp(beforeBadColor, afterBadColor, colorLerp);
        GhostMaterial.SetColor("_Emission", lerp);

    }
    void Spawn()
    {
        timer += Time.deltaTime;
        GhostMaterial.SetFloat("_EndPosition", feetPosition.position.y);
        GhostMaterial.SetFloat("_StartPosition", headPosition.position.y);
        GhostMaterial.SetFloat("_SpawnEffect", CoolMath.map(timer, 0, speed, 0, 1)) ;
        GhostParticles.SetFloat("SpawnRate", CoolMath.map(timer, 0, speed, 0, 1));
    }
    void resetSpawn()
    {
        GhostParticles.SetFloat("SpawnRate", 0);
        GhostMaterial.SetFloat("_SpawnEffect", 0);
        timer = 0;
    }

    public void PlayMusic()
    {
        _music.Play();
        EndingStats.singleton.end = EndingStats.Ending.GOOD;
    }
    
    public void PlayEndMusic()
    {
        _endMusic.Play();
        EndingStats.singleton.end = EndingStats.Ending.BAD;
    }
    

    public void LoadScene()
    {
        SceneTransitioner.singleton.SwitchScene("OfficeTest", 3);
    }
}
