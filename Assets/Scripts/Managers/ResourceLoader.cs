using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    private Dictionary<string, AudioClip> audios;
    private Dictionary<string, GameObject> prefabs;
    private Dictionary<string, Sprite> sprites;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadAudios();
        LoadPrefabs();
        LoadSprites();
    }

    private void LoadAudios()
    {
        audios = new Dictionary<string, AudioClip>();
        string[] assets = AssetDatabase.FindAssets("t:audioclip", new[] { "Assets/Resources/Audio" });
        foreach(string guid in assets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            audios.Add(name, (AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)));
        }
    }

    private void LoadPrefabs()
    {
        prefabs = new Dictionary<string, GameObject>();
        string[] assets = AssetDatabase.FindAssets("t:gameobject", new[] { "Assets/Resources/Prefabs" });
        foreach(string guid in assets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            prefabs.Add(name, (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        }
    }

    private void LoadSprites()
    {
        sprites = new Dictionary<string, Sprite>();
        string[] assets = AssetDatabase.FindAssets("t:sprite", new[] { "Assets/Resources/Sprites" });
        foreach(string guid in assets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            sprites.Add(name, (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)));
        }
    }

    public AudioClip GetAudio(string name)
    {
        return audios[name];
    }

    public GameObject GetPrefab(string name)
    {
        return prefabs[name];
    }

    public Sprite GetSprite(string name)
    {
        return sprites[name];
    }
}
