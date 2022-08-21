using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ResourceLoader
{
    private static readonly Dictionary<string, object> Dict = new();

    public static object Get(string path)
    {
        if (Dict.TryGetValue(path, out object obj)) return obj;

        object temp = Resources.Load(path);
        Dict.Add(path, temp);
        return temp;
    }

    public static T Get<T>(string path) where T : class => Get(path) as T;

    public static object[] GetAll(string path)
    {
        if (Dict.TryGetValue(path, out object obj)) return obj as object[];

        object[] temp = Resources.LoadAll(path);
        Dict.Add(path, temp);
        return temp;
    }

    public static T[] GetAll<T>(string path)
    {
        if (Dict.TryGetValue(path, out object obj)) return obj as T[];

        var temp = Resources.LoadAll(path, typeof(T)).Cast<T>().ToArray();
        Dict.Add(path, temp);
        return temp;
    }

    public static AudioClip GetAudio(string name)
    {
        return Get<AudioClip>(name);
    }

    public static GameObject GetPrefab(string name)
    {
        return Get<GameObject>(name);
    }

    public static Sprite GetSprite(string name)
    {
        return Get<Sprite>(name);
    }

    public static Sprite[] GetPackedSprite(string name)
    {
        return GetAll<Sprite>(name);
    }
}
