
#if UNITY_EDITOR
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
  /// A Vector2 editor field.
  /// </para>
  ///      </summary>
  public class Parameter2Field : BaseCompositeField<Vector2, FloatField, float>
  {
    /// <summary>
    ///        <para>
    /// USS class name of elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string ussClassName = "kuyuri-parameter2-field";
    /// <summary>
    ///        <para>
    /// USS class name of labels in elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string labelUssClassName = Parameter2Field.ussClassName + "__label";
    /// <summary>
    ///        <para>
    /// USS class name of input elements in elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string inputUssClassName = Parameter2Field.ussClassName + "__input";

    private string m_Xlabel = "X";
    private string m_Ylabel = "Y";

    internal override BaseCompositeField<Vector2, FloatField, float>.FieldDescription[] DescribeFields() => new BaseCompositeField<Vector2, FloatField, float>.FieldDescription[2]
    {
      new BaseCompositeField<Vector2, FloatField, float>.FieldDescription(m_Xlabel, "unity-x-input", (Func<Vector2, float>) (r => r.x), (BaseCompositeField<Vector2, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector2 r, float v) => r.x = v)),
      new BaseCompositeField<Vector2, FloatField, float>.FieldDescription(m_Ylabel, "unity-y-input", (Func<Vector2, float>) (r => r.y), (BaseCompositeField<Vector2, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector2 r, float v) => r.y = v))
    };

    private void Init(string xLabel, string yLabel)
    {
      m_Xlabel = xLabel;
      m_Ylabel = yLabel;
    }

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of Vector2Field.
    /// </para>
    ///      </summary>
    public Parameter2Field()
      : this((string) null)
    {
    }

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of Vector2Field.
    /// </para>
    ///      </summary>
    /// <param name="label">The text to use as a label.</param>
    public Parameter2Field(string label)
      : base(label, 2)
    {
      this.AddToClassList(Parameter2Field.ussClassName);
      this.labelElement.AddToClassList(Parameter2Field.labelUssClassName);
      //this.visualInput.AddToClassList(Vector2Field.inputUssClassName);
    }

    /// <summary>
    ///        <para>
    /// Instantiates a Vector2Field using the data read from a UXML file.
    /// </para>
    ///      </summary>
    public new class UxmlFactory : UnityEngine.UIElements.UxmlFactory<Parameter2Field, Parameter2Field.UxmlTraits>
    {
    }

    /// <summary>
    ///        <para>
    /// Defines UxmlTraits for the Vector2Field.
    /// </para>
    ///      </summary>
    public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
    {
      private UxmlStringAttributeDescription m_XLable;
      private UxmlStringAttributeDescription m_YLable;
      
      private UxmlFloatAttributeDescription m_XValue;
      private UxmlFloatAttributeDescription m_YValue;

      /// <summary>
      ///        <para>
      /// Initialize Vector2Field properties using values from the attribute bag.
      /// </para>
      ///      </summary>
      /// <param name="ve">The object to initialize.</param>
      /// <param name="bag">The attribute bag.</param>
      /// <param name="cc"></param>
      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((BaseField<Vector2>) ve).SetValueWithoutNotify(new Vector2(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc)));
        ((Parameter2Field) ve).Init(m_XLable.GetValueFromBag(bag, cc), m_YLable.GetValueFromBag(bag, cc));
      }

      public UxmlTraits() : base()
      {
        var labelAttributeDescription1 = new UxmlStringAttributeDescription();
        labelAttributeDescription1.name = "-label";
        this.m_XLable = labelAttributeDescription1;
        var labelAttributeDescription2 = new UxmlStringAttributeDescription();
        labelAttributeDescription2.name = "y-label";
        this.m_YLable = labelAttributeDescription2;
        
        UxmlFloatAttributeDescription attributeDescription1 = new UxmlFloatAttributeDescription();
        attributeDescription1.name = "x";
        this.m_XValue = attributeDescription1;
        UxmlFloatAttributeDescription attributeDescription2 = new UxmlFloatAttributeDescription();
        attributeDescription2.name = "y";
        this.m_YValue = attributeDescription2;
      }
    }
  }
}

#endif