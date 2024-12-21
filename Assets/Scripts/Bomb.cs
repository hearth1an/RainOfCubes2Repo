using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{   
    private float _lifeTime;
    private int _radius = 5;
    private float _explosionForce = 5;
    private MeshRenderer _renderer;
    private float _targetAlpha = 0f;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();    

        _lifeTime = GetLifeTime();              

        StartCoroutine(TriggerExplosion());
    }

    private int GetLifeTime()
    {
        int minTime = 2;
        int maxTime = 5;

        return Random.Range(minTime, maxTime);
    }

    private IEnumerator TriggerExplosion()
    {
        float elapsedTime = 0;

        Color startColor = _renderer.material.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, _targetAlpha);

        while (elapsedTime < _lifeTime)
        {
            elapsedTime += Time.deltaTime;

            _renderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / _lifeTime);

            yield return null;
        }

        _renderer.material.color = targetColor;

        Explode();

    }

    private void Explode()
    {
        float upwardsModifier = 0.1f;
        float destroyDelay = 0.1f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider hit in colliders)
        {            
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {                
                rb.AddExplosionForce(_explosionForce, transform.position, _radius, upwardsModifier, ForceMode.Impulse);
            }
        }

        Destroy(gameObject, destroyDelay);
    }
}
