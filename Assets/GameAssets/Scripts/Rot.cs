using DG.Tweening;
using Obi;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Doozy.Runtime.Colors.Models.HSV;

public class Rot : MonoBehaviour
{
    public float rotY;
    public float posX;
    public float time;

    public float timeScale1 = 0.2f;
    public float timeScale2 = 0.15f;
    public Vector3 torque = new Vector3(0, 0, 10f);
    public List<ObiCloth> cloths;
    [Button]
    public void Rotd()
    {
        var curRot = transform.eulerAngles;
        curRot.y += 180;
        var sq = DOTween.Sequence();
        float originScale = 1.5f;
        var originPos = transform.localPosition;
        var localPos = transform.localPosition;
        localPos.y += 4f;
        localPos.x -= 0.75f;
        transform.localPosition = localPos;
        foreach (var c in cloths)
        {
            c.enabled = true;
            c.AddTorque(torque, ForceMode.VelocityChange);
        }
        sq.Append(transform.DORotate(curRot, 0.75f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sq.Join(transform.DOLocalMove(originPos, 0.4f).OnComplete(() =>
        {

        }));
        sq.Join(DOVirtual.Float(originScale, 2f, 0.4f, (v) =>
        {
            foreach (var c in cloths)
                c.stretchingScale = v;
        }).SetLoops(2, LoopType.Yoyo));
        sq.AppendInterval(1f);
        sq.AppendCallback(() =>
        {
            foreach (var c in cloths)
                c.enabled = false;
        });

    }
    [Button]
    public void move()
    {
        var curPos = transform.position;
        curPos.x += posX;
        transform.DOMove(curPos, time);
    }

    [Button]
    public void Torque()
    {
        foreach (var c in cloths)
        {
            c.enabled = true;
            c.AddTorque(torque, ForceMode.VelocityChange);
        }

    }
}
