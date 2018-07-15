﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// UFOのController
/// </summary>
public class UFOController : MonoBehaviour
{
    /// <summary>
    /// UFOのプレファブ
    /// </summary>
    [SerializeField] private GameObject ufoPrefab;
    /// <summary>
    /// UFOのインスタンス
    /// </summary>
    private GameObject ufo = null;
    /// <summary>
    /// UFOのインスタンスにアタッチされたUFOMoverの参照
    /// </summary>
    private UFOMover ufoMover;
    /// <summary>
    /// UFOのインスタンスにアタッチされたUFOHealthの参照
    /// </summary>
    private UFOHealth ufoHelath;
    /// <summary>
    /// UFOの幅
    /// </summary>
    [SerializeField] private float ufoWidth;
    /// <summary>
    /// UFOが生成される位置y
    /// </summary>
    [SerializeField] private float ufoStartPosY;
    /// <summary>
    /// UFOが出現する時間感覚
    /// </summary>
    [SerializeField] private float interval = 25;
    /// <summary>
    /// 画面端の位置x
    /// </summary>
    private float cornerPosX;
    /// <summary>
    /// 倒した時にポイント加算をするメソッド
    /// </summary>
    private UnityAction<int> onAddScore;
    /// <summary>
    /// 画面右上端の位置
    /// </summary>
    private Vector3 maxPos;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    /// <summary>
    /// UFOが死んだ時に呼ばれるメソッド
    /// </summary>
    private UnityAction onDeath;

    public UnityAction OnDeath
    {
        get { return onDeath; }
        set { onDeath = value; }
    }

    void Awake()
    {
        if (ufo == null)
        {
            ufo = Instantiate(ufoPrefab);
        }

        ufoMover = ufo.GetComponent<UFOMover>();
        ufoHelath = ufo.GetComponent<UFOHealth>();
        cornerPosX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, ufo.transform.position.z - Camera.main.transform.position.z)).x;
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    public void BootUp(Vector3 _maxPos, UnityAction<int> _onAddScore, UnityAction _onDeath)
    {
        this.maxPos = _maxPos;
        ufoHelath.OnAddScore　=　_onAddScore;
        ufoHelath.OnDeath += _onDeath;
        StartCoroutine(Move());
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        while (true)        //TODO Enemyが一定数数以下になった場合に、bool変数でwhileを抜けるようにしても良いかも
        {
            if (ufo.activeSelf)
            {
                yield return null;
                continue;
            }
            yield return new WaitForSeconds(interval);
            ActiveUfo();
        }
    }

    /// <summary>
    /// UFOを稼働する
    /// </summary>
    public void ActiveUfo()
    {
        bool isRight = Random.Range(0, 2) == 0 ? true : false;
        float sign = isRight ? 1 : -1;
        
        ufo.transform.position = new Vector3(sign * (cornerPosX + ufoWidth), this.maxPos.y - ufoStartPosY, 0);
        float otherCornerPosX = -sign * (cornerPosX + ufoWidth);

        ufoMover.PrepareToMove(-sign, otherCornerPosX, () =>
        {
            ufo.SetActive(false);
        });
        ufo.SetActive(true);
    }
}
