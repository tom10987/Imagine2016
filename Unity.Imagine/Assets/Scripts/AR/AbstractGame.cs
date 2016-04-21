﻿
using UnityEngine;
using System.Collections.Generic;

//------------------------------------------------------------
// NOTICE:
// ミニゲームの処理を司る抽象クラス
// 派生クラスに詳細なゲームの処理を実装する
//
//------------------------------------------------------------
// TIPS:
// コンポーネントとして使いますが、AR モデルには設定しないでください
// 基本的に単体で動作できるような仕組みにしています
//
// void Action()
// * ミニゲームのメインとなる処理を記述してください
//
// bool IsFinish()
// * ゲーム終了の判定に使用します
// * ゲーム終了状態になったら true を返すようにしてください
//
// virtual bool IsDraw()
// * 引き分けの判定に使用します
// * 引き分け状態になるゲームの場合のみ override してください
//
// Transform GetWinner()
// * 勝利プレイヤーが使用していたモデルの Transform を返すようにしてください
//
// string gameRule { get; }
// * ゲーム開始前のルール説明で使用します
// * 派生クラスの Start() メソッドなどで文字列を入力してください
//
//------------------------------------------------------------

public abstract class AbstractGame : MonoBehaviour
{
  // ゲームの処理
  public abstract void Action();

  // ゲームが終了したとき true を返す
  public abstract bool IsFinish();

  // ゲームが引き分けなら true を返す
  // TIPS: 引き分けにならないゲームは override しなくて大丈夫です
  public virtual bool IsDraw() { return false; }

  // ゲームの勝者
  public abstract Transform GetWinner();

  /// <summary> ゲームルールの説明 </summary>
  public string gameRule { get; protected set; }

  /// <summary> プレイヤーの情報を取り出す </summary>
  public GameManager gameManager { get; set; }

  public ARModel player1 { get; protected set; }
  public ARModel player2 { get; protected set; }

  // TIPS: プレイヤーの入力取得用プロパティ
  static GameController controller { get { return GameController.instance; } }
  protected static IEnumerable<KeyCode> inputP1 { get { return controller.player1; } }
  protected static IEnumerable<KeyCode> inputP2 { get { return controller.player2; } }
}
