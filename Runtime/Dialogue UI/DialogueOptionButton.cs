using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogueOptionButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private DialogueOption _option;

        [SerializeField] private Button _button;

        public void Init(DialogueOption option)
        {
            _text.text = option.PortName;
            _option = option;

            _button.onClick.AddListener(SayNextSentence);
        }

        public void Init()
        {
            _text.text = ">";
            _button.onClick.AddListener(() =>
            {
                DialogueManager.Instance.SayNextSentence();
            });
        }

        private void SayNextSentence()
        {
            DialogueManager.Instance.SaySentence(_option.TargetNodeGUID);
        }
    }
}