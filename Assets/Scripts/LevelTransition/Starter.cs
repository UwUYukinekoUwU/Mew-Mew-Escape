using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapM;
using static GameM;
using static PrefabHolder;
using System;

/// <summary>
/// Class responsible for positioning the player next to the right level entrance.
/// </summary>
public class Starter : MonoBehaviour
{
    [Header("Entrance links")]
    [SerializeField] private Transform TopEntrance;
    [SerializeField] private Transform BottomEntrance;
    [SerializeField] private Transform RightEntrance;
    [SerializeField] private Transform LeftEntrance;


    private Dictionary<string, Func<Transform>> initTransformItem = new Dictionary<string, Func<Transform>>();

    void Start()
    {
        initTransformItem = new Dictionary<string, Func<Transform>>()
        {
            { "Player", InitPlayer },
            { "HidingBox", InitHidingBox },
            { "Skateboard", InitSkateboard },
        };

        PositionPlayer();
    }

    /// <summary>
    /// Positions the player next to the right entrance
    /// </summary>
    private void PositionPlayer()
    {
        string currentTransformationItem = Game.GetPlayerTransformationItem();
        Transform _Player = initTransformItem[currentTransformationItem]();


        Vector2 diff = Map.WantedPosition - Map.CurrentPosition;

        if (diff.x > 0) //enters from left
            _Player.transform.position = LeftEntrance.position;
        if (diff.x < 0) //enters from right
            _Player.transform.position = RightEntrance.position;
        if (diff.y < 0) //enters from top
            _Player.transform.position = TopEntrance.position;
        if (diff.y > 0) //enters from bottom
            _Player.transform.position = BottomEntrance.position;


        Map.CurrentPosition = Map.WantedPosition;
    }


    private Transform InitPlayer()
    {
        return Game.GetComponentByName<Transform>("Player");
    }

    private Transform InitSkateboard()
    {
        GameObject skateboard = Instantiate(Prefabs.GetByTag("Skateboard"));

        AbilityController player = Game.GetComponentByName<AbilityController>("Player");
        skateboard.GetComponent<Skate>().GetPlayerReference();
        player.SkateTransform(skateboard);

        return skateboard.transform;
    }


    private Transform InitHidingBox()
    {
        GameObject box = Instantiate(Prefabs.GetByTag("HidingBox"));

        AbilityController player = Game.GetComponentByName<AbilityController>("Player");
        box.GetComponent<HidingBox>().GetPlayerReference();
        player.BoxTransform(box);

        return box.transform;
    }
}
