
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIHud
{
    public class StatBar : VisualElement, INotifyValueChanged<float>
    {
        public int width { get; set; }
        public int height { get; set; }

        public void SetValueWithoutNotify(float newValue)
        {
            m_value = newValue;
        }

        private float m_value;

        public float value
        {
            get
            {
                m_value = Mathf.Clamp(m_value, 0, 1);
                return m_value;
            }
            set
            {
                if (EqualityComparer<float>.Default.Equals(m_value, value))
                    return;
                if (this.panel != null)
                {
                    using ChangeEvent<float> pooled = ChangeEvent<float>.GetPooled(this.m_value, value);

                    pooled.target = (IEventHandler)this;
                    this.SetValueWithoutNotify(value);
                    this.SendEvent((EventBase)pooled);
                }
                else
                {
                    SetValueWithoutNotify(value);
                }
            }
        }
        public enum FillType
        {
            Horizontal,
            Vertical
        }
        public FillType fillType;

        private VisualElement sbParent;
        private VisualElement sbBackground;
        private VisualElement sbForeground;

        public new class UxmlFactory : UxmlFactory<StatBar, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlIntAttributeDescription m_width = new UxmlIntAttributeDescription() { name = "width", defaultValue = 300 };
            UxmlIntAttributeDescription m_height = new UxmlIntAttributeDescription() { name = "height", defaultValue = 50 };
            UxmlFloatAttributeDescription m_value = new UxmlFloatAttributeDescription() { name = "value", defaultValue = 1 };
            UxmlEnumAttributeDescription<StatBar.FillType> m_fillType = new UxmlEnumAttributeDescription<StatBar.FillType>() { name = "fill-type", defaultValue = 0 };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as StatBar;
                ate.width = m_width.GetValueFromBag(bag, cc);
                ate.height = m_height.GetValueFromBag(bag, cc);
                ate.value = m_value.GetValueFromBag(bag, cc);
                ate.fillType = m_fillType.GetValueFromBag(bag, cc);

                ate.Clear();
                VisualTreeAsset vt = Resources.Load<VisualTreeAsset>("UI Documents/Stat");
                VisualElement statBar = vt.Instantiate();
                ate.sbParent = statBar.Q<VisualElement>("stat");
                ate.sbBackground = statBar.Q<VisualElement>("background");
                ate.sbForeground = statBar.Q<VisualElement>("foreground");
                ate.Add(statBar);

                ate.sbParent.style.width = ate.width;
                ate.sbParent.style.height = ate.height;
                ate.style.width = ate.width;
                ate.style.height = ate.height;

                ate.FillBar();
            }
        }

        public void UpdateStat(ChangeEvent<float> evt)
        {

        }

        public void FillBar()
        {
            if (fillType == FillType.Horizontal)
            {
                sbForeground.style.scale = new Scale(new Vector3(value, 1, 0));
            }
            else
            {
                sbForeground.style.scale = new Scale(new Vector3(1, value, 0));
            }
        }
    }

}
