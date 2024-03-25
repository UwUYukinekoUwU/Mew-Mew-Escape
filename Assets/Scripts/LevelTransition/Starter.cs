using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapM;
using static GameM;
using Cinemachine;

public class Starter : MonoBehaviour
{
    [Header("Entrance links")]
    [SerializeField] private Transform TopEntrance;
    [SerializeField] private Transform BottomEntrance;
    [SerializeField] private Transform RightEntrance;
    [SerializeField] private Transform LeftEntrance;


    void Start()
    {
        PositionPlayer();
    }


    private void PositionPlayer()
    {
        Transform _Player = Game.GetComponentByName<Transform>("Player");

        Vector2 diff = Map.WantedPosition - Map.CurrentPosition;

        if (diff.x > 0) //enters from left
            _Player.Translate(LeftEntrance.position);
        if (diff.x < 0) //enters from right
            _Player.Translate(RightEntrance.position);
        if (diff.y < 0) //enters from top
            _Player.Translate(TopEntrance.position);
        if (diff.y > 0) //enters from bottom
            _Player.Translate(BottomEntrance.position);

        Map.CurrentPosition = Map.WantedPosition;
    }
}
