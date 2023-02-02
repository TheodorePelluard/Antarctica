using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Interactible
{
    [SerializeField] AnimationCurve _jumpCurve;
    [SerializeField] float _jumpSpeed = 1.5f;
    Coroutine _jumpCoroutine;
    [Space]
    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _heightPoint;
    [SerializeField] Transform _endPoint;

    public override void Interact()
    {
        base.Interact();
            
        if(_jumpCoroutine == null) { }
            StartCoroutine(JumpSequence());

        IEnumerator JumpSequence()
        {
            Debug.Log("Player Jump");

            PlayerManager.Instance.DisablePlayerControl();

            float t = 0f;
            Transform player = PlayerManager.Instance.PlayerTransform;

            yield return StartCoroutine(lerpToStartPoint(_startPoint));

            while (t < 1f)
            {
                t += Time.deltaTime * _jumpSpeed;
                player.position = MathParabola.Parabola(_startPoint.position, _endPoint.position, _heightPoint.position.y - _startPoint.position.y, _jumpCurve.Evaluate(t));
                yield return null;
            }

            PlayerManager.Instance.EnablePlayerControl();
            _jumpCoroutine = null;
        }
    }

    IEnumerator lerpToStartPoint(Transform startPoint)
    {
        float t = 0f;
        Transform player = PlayerManager.Instance.PlayerTransform;

        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            player.position = Vector3.Lerp(player.position, _startPoint.position, t);
            yield return null;
        }
    }
}
