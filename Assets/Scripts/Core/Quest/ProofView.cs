using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using Core.NPC;
using Core.Proof;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProofView : MonoBehaviour
{
    [Inject]
    private SignalBus _signalBus;
    [SerializeField]
    private ProofModel _model;
    private bool _playerInRange = false;

    public ProofModel Model => _model;

    public void SetModel(ProofModel model)
    {
        _model = model;
    }

    private void OnMouseDown()
    {
        if (_playerInRange)
        {
            _signalBus.Fire(new PlayerCollectedProof { Proof = _model });
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
