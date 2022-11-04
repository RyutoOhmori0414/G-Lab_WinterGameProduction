using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カウントダウンなどで動きを止めておくときに行う処理を実装してください
/// </summary>
public interface IPausable
{
    /// <summary>
    /// Pause 動きを止める処理を書いてください
    /// </summary>
    public void Pause();
    /// <summary>
    /// Pauseを解除する処理
    /// </summary>
    public void Awake();
}
