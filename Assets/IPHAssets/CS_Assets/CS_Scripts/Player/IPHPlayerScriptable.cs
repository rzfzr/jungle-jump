using InfiniteHopper.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPHPlayerScriptable : ScriptableObject
{
    private static IPHPlayerScriptable[] m_all;

    public static IPHPlayerScriptable[] All
    {
        get
        {
            if (m_all == null)
            {
                var all = Resources.LoadAll<IPHPlayerScriptable>("Players");

                m_all = new IPHPlayerScriptable[all.Length];

                for (int i = 0; i < all.Length; i++)
                    m_all[i] = Instantiate(all[i]) as IPHPlayerScriptable;
            }

            return m_all;

        }
    }

    public static IPHPlayerScriptable FindByName(string name)
    {
        int index = Array.FindIndex(All, x => x.name == name);

        if (index != -1)
            return All[index];

        Debug.LogWarning(string.Format("Could not find level: {0}", name));
        return null;
    }

    /// <summary>
    /// How fast the player's jump power increases when we are holding the jump button
    /// </summary>
    [SerializeField]
    private float jumpChargeSpeed = 30;
    /// <summary>
    /// The maximum jump power of the player
    /// </summary>
    [SerializeField]
    private float jumpChargeMax = 20;
    /// <summary>
    /// The *horizontal* movement speed of the player when it is jumping
    /// </summary>
    [SerializeField]
    private float moveSpeed = 4;
    /// <summary>
    /// The offset of collider that close the player avatar
    /// </summary>
    [SerializeField]
    private Vector2 colliderOffset;
    /// <summary>
    /// The size of collider that close the player avatar
    /// </summary>
    [SerializeField]
    private Vector2 colliderSize;
    /// <summary>
    /// The particle effects that will play when the player jumps and lands on a column
    /// </summary>
    [Header("Particles")]
    [SerializeField]
    private ParticleSystem jumpEffect;
    [SerializeField]
    private ParticleSystem landEffect;
    [SerializeField]
    private ParticleSystem perfectEffect;
    /// <summary>
    /// Various animations for the player
    /// </summary>
    [Space(10)]
    [Header("Animations")]
    [SerializeField]
    private AnimationClip animationJumpStart;
    [SerializeField]
    private AnimationClip animationJumpEnd;
    [SerializeField]
    private AnimationClip animationFullPower;
    [SerializeField]
    private AnimationClip animationFalling;
    [SerializeField]
    private AnimationClip animationLanded;
    /// <summary>
    /// Various sounds and their source
    /// </summary>
    [Space(10)]
    [Header("Sounds")]
    [SerializeField]
    private AudioClip soundStartJump;
    [SerializeField]
    private AudioClip soundEndJump;
    [SerializeField]
    private AudioClip soundLand;
    [SerializeField]
    private AudioClip soundCrash;
    [SerializeField]
    private AudioClip soundPerfect;
    /// <summary>
    /// Player references to update the asset when instantiate
    /// </summary>
    [Space(10)]
    [Header("References")]
    [SerializeField]
    private GameObject avatar;

    public float JumpChargeSpeed { get { return jumpChargeSpeed; } }
    public float JumpChargeMax { get { return jumpChargeMax; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Vector2 ColliderOffset { get { return colliderOffset; } }
    public Vector2 ColliderSize { get { return colliderSize; } }
    public ParticleSystem JumpEffect { get { return jumpEffect; } }
    public ParticleSystem LandEffect { get { return landEffect; } }
    public ParticleSystem PerfectEffect { get { return perfectEffect; } }
    public AnimationClip AnimationJumpStart { get { return animationJumpStart; } }
    public AnimationClip AnimationJumpEnd { get { return animationJumpEnd; } }
    public AnimationClip AnimationFullPower { get { return animationFullPower; } }
    public AnimationClip AnimationFalling { get { return animationFalling; } }
    public AnimationClip AnimationLanded { get { return animationLanded; } }
    public AudioClip SoundStartJump { get { return soundStartJump; } }
    public AudioClip SoundEndJump { get { return soundEndJump; } }
    public AudioClip SoundLand { get { return soundLand; } }
    public AudioClip SoundCrash { get { return soundCrash; } }
    public AudioClip SoundPerfect { get { return soundPerfect; } }
    public GameObject Avatar { get { return avatar; } }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Assets/Create/GameAssets/Player", false, 0)]
    public static void CreateAsset()
    {
        IPHPlayerScriptable asset = CreateInstance<IPHPlayerScriptable>();

        string path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);

        if (path == "")
            path = "Assets";

        else if (System.IO.Path.GetExtension(path) != "")
            path = path.Replace(System.IO.Path.GetFileName(UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject)), "");

        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(IPHPlayerScriptable) + ".asset");

        UnityEditor.AssetDatabase.CreateAsset(asset, assetPathAndName);

        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = asset;
    }
#endif
}
