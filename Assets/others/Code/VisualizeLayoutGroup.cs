using UnityEngine;
using UnityEngine.UI;

namespace others.Code
{
    [ExecuteAlways]
    [RequireComponent(typeof(LayoutGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class VisualizeLayoutGroup : MonoBehaviour
    {
        [SerializeField] private Color _childColor = new(1f, 0.8f, 0.2f, 0.5f);
        [SerializeField] private Color _paddingColor = new(0f, 0.6f, 1f, 0.3f);
        [SerializeField] private bool _showChildRects = true;
        [SerializeField] private bool _showPadding = true;

        private LayoutGroup _group;

        private void OnEnable()
        {
            _group = GetComponent<LayoutGroup>();
        }

        private void OnDrawGizmos()
        {
            if (!_group) _group = GetComponent<LayoutGroup>();
            if (!_group) return;
            var rt = transform as RectTransform;
            if (!rt) return;
            DrawWorldRectFromRectTransform(rt, _paddingColor);
            if (_showPadding)
            {
                var canvas = GetComponentInParent<Canvas>();
                var scale = canvas ? canvas.scaleFactor : 1f;
                var p = _group.padding;
                var corners = new Vector3[4];
                rt.GetWorldCorners(corners);
                var bl = corners[0];
                var tl = corners[1];
                var tr = corners[2];
                var br = corners[3];
                var rightDir = (br - bl).normalized;
                var upDir = (tl - bl).normalized;
                var leftWorld = p.left / scale;
                var rightWorld = p.right / scale;
                var topWorld = p.top / scale;
                var bottomWorld = p.bottom / scale;
                var bl2 = bl + rightDir * leftWorld + upDir * bottomWorld;
                var br2 = br - rightDir * rightWorld + upDir * bottomWorld;
                var tl2 = tl + rightDir * leftWorld - upDir * topWorld;
                var tr2 = tr - rightDir * rightWorld - upDir * topWorld;
                DrawRectFromCorners(new[] { bl2, tl2, tr2, br2 }, _paddingColor);
            }

            if (_showChildRects)
            {
                for (var i = 0; i < rt.childCount; i++)
                {
                    var c = rt.GetChild(i) as RectTransform;
                    if (c) DrawWorldRectFromRectTransform(c, _childColor);
                }
            }
        }

        private void DrawWorldRectFromRectTransform(RectTransform r, Color c)
        {
            var corners = new Vector3[4];
            r.GetWorldCorners(corners);
            DrawRectFromCorners(corners, c);
        }

        private void DrawRectFromCorners(Vector3[] corners, Color c)
        {
            Gizmos.color = c;
            Gizmos.DrawLine(corners[0], corners[1]);
            Gizmos.DrawLine(corners[1], corners[2]);
            Gizmos.DrawLine(corners[2], corners[3]);
            Gizmos.DrawLine(corners[3], corners[0]);
        }
    }
}