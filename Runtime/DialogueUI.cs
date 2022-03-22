using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using EventDriven;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour, IEventListener<DialogueSentenceSaid>, IEventListener<DialogueStart>, IEventListener<DialogueEnd>
    {
        [SerializeField] private TMP_Text _sentenceText;
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private CanvasGroup _group;

        [Header("Prefabs")] [SerializeField] private DialogueOptionButton _buttonPrefab;
        [SerializeField] private GameObject _buttonsContainer;
        
        [Header("Instantiated Buttons")]
        [SerializeField] private List<DialogueOptionButton> _instantiatedButtons = new List<DialogueOptionButton>();

        private IEnumerator DisplaySentenceCoroutine;
        private DialogueSentenceSaid _currentSentenceData;
        private bool _canSkip = false;
        
        private void OnEnable()
        {
            this.StartListening<DialogueSentenceSaid>();
            this.StartListening<DialogueEnd>();
            this.StartListening<DialogueStart>();
        }

        private void OnDisable()
        {
            this.StopListening<DialogueSentenceSaid>();
            this.StopListening<DialogueEnd>();
            this.StopListening<DialogueStart>();
        }

        public void OnTriggerEvent(DialogueSentenceSaid data)
        {
            _characterName.text = data.Sentence.Character.Name;
            _canSkip = true;
            _currentSentenceData = data;

            DisplaySentence(data.Sentence.DialogueText);
        }

        /// <summary>
        /// Displays the sentence on the ui.
        /// </summary>
        /// <param name="sentence"></param>
        private void DisplaySentence(string sentence)
        {
            if(DisplaySentenceCoroutine != null)
                StopCoroutine(DisplaySentenceCoroutine);
            
            DisplaySentenceCoroutine = DisplaySentence_CO(sentence);
            StartCoroutine(DisplaySentenceCoroutine);
        }

        /// <summary>
        /// Shows the sentence in the UI letter by letter.
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private IEnumerator DisplaySentence_CO(string sentence)
        {
            _sentenceText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                _sentenceText.text += letter;
                yield return null;
            }

            _canSkip = false;
            ShowOptions();
        }

        private void ShowOptions()
        {
            ClearPreviousButtons();

            if (!_currentSentenceData.HasOptions)
            {
                DialogueOptionButton ou = Instantiate(_buttonPrefab, _buttonsContainer.transform, true);
                ou.Init();
                
                _instantiatedButtons.Add(ou);
            }
            
            foreach(var option in _currentSentenceData.Options)
            {
                DialogueOptionButton ou = Instantiate(_buttonPrefab, _buttonsContainer.transform, true);
                ou.Init(option);
                
                _instantiatedButtons.Add(ou);
            }
        }

        private void ClearPreviousButtons()
        {
            if (_instantiatedButtons == null)
                return;

            if (_instantiatedButtons.Count == 0)
                return;
            
            foreach (var button in _instantiatedButtons)
            {
                Destroy(button.gameObject);
            }
            
            _instantiatedButtons.Clear();
        }

        public void OnTriggerEvent(DialogueStart data)
        {
            _group.alpha = 1f;
        }

        public void OnTriggerEvent(DialogueEnd data)
        {
            _group.alpha = 0f;
            ClearPreviousButtons();
        }
    }
}