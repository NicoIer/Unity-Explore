using System.Collections.Generic;
using Nico;
using Nico.Collections;
using UnityEngine;

namespace Nico
{
    public class UILayerManager
    {
        // private readonly int _uiLayerMask;
        // private readonly int _hiddenLayerMask;
        private PriorityQueue<UIPanel> _queue;
        public bool HasItem => _queue.Count > 0;
        private Stack<UIPanel> _tmp;
        private Transform rectTransform;
        public UILayerManager(RectTransform transform)//,int uiLayerMask, int hiddenLayerMask)
        {
            this.rectTransform = transform;
            // this._uiLayerMask = uiLayerMask;
            // this._hiddenLayerMask = hiddenLayerMask;
            _queue = new PriorityQueue<UIPanel>((_1, _2) => _1.Priority() - _2.Priority());
            _tmp = new Stack<UIPanel>();
        }

        public void Remove(UIPanel panel)
        {
            _tmp.Clear();
            while (_queue.Count > 0)
            {
                UIPanel current = _queue.Dequeue();
                if (panel == current)
                {
                    HidePanel(current);
                    break;
                }

                _tmp.Push(current);
            }

            while (_tmp.Count > 0)
            {
                _queue.Enqueue(_tmp.Pop());
            }
        }

        public void RemoveAll()
        {
            _tmp.Clear();
            while (_queue.Count > 0)
            {
                UIPanel current = _queue.Dequeue();
                HidePanel(current);
            }
        }

        public void Push(UIPanel panel)
        {
            ShowPanel(panel);
            _queue.Enqueue(panel);
            if (panel.Priority() > _queue.Peek().Priority())
            {
                panel.transform.SetAsLastSibling();
                return;
            }

            foreach (UIPanel uiPanel in _queue.EnumerateMinToMax())
            {
                uiPanel.transform.SetAsLastSibling();
            }
        }

        public bool Pop(out UIPanel panel)
        {
            panel = default;
            if (_queue.Count == 0)
            {
                return false;
            }

            panel = _queue.Dequeue();
            HidePanel(panel);
            return true;
        }

        public void ShowPanel(UIPanel panel)
        {
            panel.gameObject.SetActive(true);
            // panel.gameObject.layer = _hiddenLayerMask;
            panel.OnShow();
        }

        public void HidePanel(UIPanel panel)
        {
            // panel.gameObject.layer = _uiLayerMask;
            panel.gameObject.SetActive(false);
            panel.OnHide();
        }
    }
}