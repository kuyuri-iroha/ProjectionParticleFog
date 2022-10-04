using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kuyuri.UIToolkitExtensions
{
    /// <summary>
  ///        <para>
  /// A Vector4 editor field.
  /// </para>
  ///      </summary>
  public class Parameter4Field : BaseCompositeField<Vector4, FloatField, float>
  {
    /// <summary>
    ///        <para>
    /// USS class name of elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string ussClassName = "kuyuri-parameter4-field";
    /// <summary>
    ///        <para>
    /// USS class name of labels in elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string labelUssClassName = Parameter4Field.ussClassName + "__label";
    /// <summary>
    ///        <para>
    /// USS class name of input elements in elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string inputUssClassName = Parameter4Field.ussClassName + "__input";

    private string m_XLabel = "X";
    private string m_YLabel = "Y";
    private string m_ZLabel = "Z";
    private string m_WLabel = "W";

    internal override BaseCompositeField<Vector4, FloatField, float>.FieldDescription[] DescribeFields()
    {
      Debug.Log($"Label1: {m_XLabel}");
      return new BaseCompositeField<Vector4, FloatField, float>.FieldDescription[4]
      {
        new BaseCompositeField<Vector4, FloatField, float>.FieldDescription(m_XLabel, "unity-x-input",
          (Func<Vector4, float>) (r => r.x),
          (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) =>
            r.x = v)),
        new BaseCompositeField<Vector4, FloatField, float>.FieldDescription(m_YLabel, "unity-y-input",
          (Func<Vector4, float>) (r => r.y),
          (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) =>
            r.y = v)),
        new BaseCompositeField<Vector4, FloatField, float>.FieldDescription(m_ZLabel, "unity-z-input",
          (Func<Vector4, float>) (r => r.z),
          (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) =>
            r.z = v)),
        new BaseCompositeField<Vector4, FloatField, float>.FieldDescription(m_WLabel, "unity-w-input",
          (Func<Vector4, float>) (r => r.w),
          (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) =>
            r.w = v))
      };
    }

    private void Init(string xLabel, string yLabel, string zLabel, string wLabel)
    {
      m_XLabel = xLabel;
      m_YLabel = yLabel;
      m_ZLabel = zLabel;
      m_WLabel = wLabel;
      SaveViewData();
      MarkDirtyRepaint();
    }

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of Vector4Field.
    /// </para>
    ///      </summary>
    public Parameter4Field()
      : this((string) null)
    {
    }

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of Vector4Field.
    /// </para>
    ///      </summary>
    /// <param name="label">The text to use as a label.</param>
    public Parameter4Field(string label)
      : base(label, 4)
    {
      this.AddToClassList(Parameter4Field.ussClassName);
      this.labelElement.AddToClassList(Parameter4Field.labelUssClassName);
      this.visualInput.AddToClassList(Parameter4Field.inputUssClassName);
    }

    /// <summary>
    ///        <para>
    /// Instantiates a Vector4Field using the data read from a UXML file.
    /// </para>
    ///      </summary>
    public new class UxmlFactory : UnityEngine.UIElements.UxmlFactory<Parameter4Field, Parameter4Field.UxmlTraits>
    {
    }

    /// <summary>
    ///        <para>
    /// Defines UxmlTraits for the Vector4Field.
    /// </para>
    ///      </summary>
    public new class UxmlTraits : BaseField<Vector4>.UxmlTraits
    {
      private UxmlStringAttributeDescription m_XLable;
      private UxmlStringAttributeDescription m_YLable;
      private UxmlStringAttributeDescription m_ZLable;
      private UxmlStringAttributeDescription m_WLable;
      
      private UxmlFloatAttributeDescription m_XValue;
      private UxmlFloatAttributeDescription m_YValue;
      private UxmlFloatAttributeDescription m_ZValue;
      private UxmlFloatAttributeDescription m_WValue;

      /// <summary>
      ///        <para>
      /// Initialize Vector4Field properties using values from the attribute bag.
      /// </para>
      ///      </summary>
      /// <param name="ve">The object to initialize.</param>
      /// <param name="bag">The attribute bag.</param>
      /// <param name="cc"></param>
      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((Parameter4Field) ve).Init(m_XLable.GetValueFromBag(bag, cc), m_YLable.GetValueFromBag(bag, cc), m_ZLable.GetValueFromBag(bag, cc), m_WLable.GetValueFromBag(bag, cc));
        ((BaseField<Vector4>) ve).SetValueWithoutNotify(new Vector4(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc)));
      }

      public UxmlTraits() : base()
      {
        var labelAttributeDescription1 = new UxmlStringAttributeDescription
        {
          name = "x-label"
        };
        m_XLable = labelAttributeDescription1;
        Debug.Log($"Label1: {m_XLable}");
        var labelAttributeDescription2 = new UxmlStringAttributeDescription
        {
          name = "y-label"
        };
        m_YLable = labelAttributeDescription2;
        var labelAttributeDescription3 = new UxmlStringAttributeDescription
        {
          name = "z-label"
        };
        m_ZLable = labelAttributeDescription3;
        var labelAttributeDescription4 = new UxmlStringAttributeDescription
        {
          name = "w-label"
        };
        m_WLable = labelAttributeDescription4;
        
        UxmlFloatAttributeDescription attributeDescription1 = new UxmlFloatAttributeDescription();
        attributeDescription1.name = "x";
        this.m_XValue = attributeDescription1;
        UxmlFloatAttributeDescription attributeDescription2 = new UxmlFloatAttributeDescription();
        attributeDescription2.name = "y";
        this.m_YValue = attributeDescription2;
        UxmlFloatAttributeDescription attributeDescription3 = new UxmlFloatAttributeDescription();
        attributeDescription3.name = "z";
        this.m_ZValue = attributeDescription3;
        UxmlFloatAttributeDescription attributeDescription4 = new UxmlFloatAttributeDescription();
        attributeDescription4.name = "w";
        this.m_WValue = attributeDescription4;
      }
    }
  }
}