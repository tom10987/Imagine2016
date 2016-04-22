﻿using UnityEngine;
using System.Collections.Generic;

public class ChargePlayer : MonoBehaviour
{
    //[SerializeField]
    //private Gage _gage;

    Gage[] _gage;

    ChargeGameController _chargeGameController;

    bool[] _pressOnce;

    public bool PressOnce1 { get { return _pressOnce[0]; } set { _pressOnce[0] = value; } }
    public bool PressOnce2 { get { return _pressOnce[1]; } set { _pressOnce[1] = value; } }


    public float _totalScorePlayer1 = 0;

    public float getTotalScorePlayer1 { get { return _totalScorePlayer1; } set { _totalScorePlayer1 = value; } }

    public float _totalScorePlayer2 = 0;

    public float getTotalScorePlayer2 { get { return _totalScorePlayer2; } set { _totalScorePlayer2 = value; } }

    int[] _firstSkipCount;

    GameController _controller;

    IEnumerator<KeyCode> P1Key;
    IEnumerator<KeyCode> P2Key;

   // [SerializeField]
    private EnergyGage[] _energyGage;

    //[SerializeField]
    Round _round = null;

    void Start()
    {
        _firstSkipCount = new int[2];
        _firstSkipCount[0] = 0;
        _firstSkipCount[1] = 0;
        _pressOnce = new bool[2];
        _pressOnce[1] = false;
        _pressOnce[0] = false;
        _gage = new Gage[2];
        
        _round = FindObjectOfType<Round>();
        _chargeGameController = GetComponent<ChargeGameController>();
        
        _controller = FindObjectOfType<GameController>();

        _gage[0] = GameObject.Find("GageUI1").GetComponent<Gage>();
        _gage[1] = GameObject.Find("GageUI2").GetComponent<Gage>();
        _energyGage = new EnergyGage[2];
        _energyGage[0] = GameObject.Find("EnergyGage1").GetComponent<EnergyGage>();
        _energyGage[1] = GameObject.Find("EnergyGage2").GetComponent<EnergyGage>();

    }

    void Update()
    {
        // IsKeyDownMoveGage();
        // EnergyGageMove();

    }

    public void IsKeyDownMoveGage()
    {
        

        P1Key = _controller.player1.GetEnumerator();
        P2Key = _controller.player2.GetEnumerator();

        if ( Input.GetKey(P1Key.Current) /*&& _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1*/)
        {
            
            if (!(_firstSkipCount[0] == 0))
            {
                if (_pressOnce[0]) return;
                _gage[0].MoveSelectGage();
                //   Debug.Log("homo1");
            }
        }
        else
        if (Input.GetKeyUp(P1Key.Current) /*&& _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1*/)
        {
            if (_pressOnce[0]) return;
            if (_firstSkipCount[0] >= 1)
            {
                _totalScorePlayer1 += _gage[0].RangeSelectNow();
                _pressOnce[0] = true;

            }
            _firstSkipCount[0]++;
        }

        if ( Input.GetKey(P2Key.Current) /*&& _energyGage[1].getSelectPlayer == EnergyGage.Player.Player2*/)
        {
            
            if (!(_firstSkipCount[1] == 0))
            {
                if (_pressOnce[1]) return;
                _gage[1].MoveSelectGage();
                //   Debug.Log("homo2");
            }
        }
        else
        if (Input.GetKeyUp(P2Key.Current) /*&& _energyGage[1].getSelectPlayer == EnergyGage.Player.Player2*/)
        {
            if (_pressOnce[1]) return;
            if(_firstSkipCount[1] >= 1)
            {
                _totalScorePlayer2 += _gage[1].RangeSelectNow();
                _pressOnce[1] = true;
            }
            _firstSkipCount[1]++;
        }

    }

    public void EnergyGageMove()
    {
        _chargeGameController = GetComponent<ChargeGameController>();

		if (_chargeGameController.name == "_GameInstance")
			return;
		Debug.Log (_chargeGameController);
        //Debug.Log(_chargeGameController.player1Obj);
        //Debug.Log(_chargeGameController.player2Obj);
        if (_chargeGameController.player1Obj == gameObject.transform.parent.gameObject)
        {
            if (_energyGage[0].ChargePowerGage() == true)
            {
                _round.NextRound();
            }
        }
        else
        if (_chargeGameController.player2Obj == gameObject.transform.parent.gameObject)
        {
            if (_energyGage[1].ChargePowerGage() == true)
            {
                _round.NextRound();
            }
        }

    }

    public void Init()
    {
        foreach(var gage in _gage)
        {
            gage.InitGage();
        }
        //_gage.InitGage();
        _pressOnce[0] = false;
        _pressOnce[1] = false;

    }

}
