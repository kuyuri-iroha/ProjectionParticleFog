#if UNITY_EDITOR
// Decompiled with JetBrains decompiler
// Type: UnityEditor.UIElements.Vector4Field
// Assembly: UnityEditor.UIElementsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B91B1302-FAC5-442B-87D8-C7D7002F3AB8
// Assembly location: D:\DevTools\Unity\2021.3.5f1\Editor\Data\Managed\UnityEngine\UnityEditor.UIElementsModule.dll
// XML documentation location: D:\DevTools\Unity\2021.3.5f1\Editor\Data\Managed\UnityEngine\UnityEditor.UIElementsModule.xml

using System;
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
  public class Vector4Field : BaseCompositeField<Vector4, FloatField, float>
  {
    /// <summary>
    ///        <para>
    /// USS class name of elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string ussClassName = "unity-vector4-field";
    /// <summary>
    ///        <para>
    /// USS class name of labels in elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string labelUssClassName = Vector4Field.ussClassName + "__label";
    /// <summary>
    ///        <para>
    /// USS class name of input elements in elements of this type.
    /// </para>
    ///      </summary>
    public new static readonly string inputUssClassName = Vector4Field.ussClassName + "__input";

    internal override BaseCompositeField<Vector4, FloatField, float>.FieldDescription[] DescribeFields() => new BaseCompositeField<Vector4, FloatField, float>.FieldDescription[4]
    {
      new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("X", "unity-x-input", (Func<Vector4, float>) (r => r.x), (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) => r.x = v)),
      new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("Y", "unity-y-input", (Func<Vector4, float>) (r => r.y), (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) => r.y = v)),
      new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("Z", "unity-z-input", (Func<Vector4, float>) (r => r.z), (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) => r.z = v)),
      new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("W", "unity-w-input", (Func<Vector4, float>) (r => r.w), (BaseCompositeField<Vector4, FloatField, float>.FieldDescription.WriteDelegate) ((ref Vector4 r, float v) => r.w = v))
    };

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of Vector4Field.
    /// </para>
    ///      </summary>
    public Vector4Field()
      : this((string) null)
    {
    }

    /// <summary>
    ///        <para>
    /// Initializes and returns an instance of Vector4Field.
    /// </para>
    ///      </summary>
    /// <param name="label">The text to use as a label.</param>
    public Vector4Field(string label)
      : base(label, 4)
    {
      this.AddToClassList(Vector4Field.ussClassName);
      this.labelElement.AddToClassList(Vector4Field.labelUssClassName);
      this.visualInput.AddToClassList(Vector4Field.inputUssClassName);
    }

    /// <summary>
    ///        <para>
    /// Instantiates a Vector4Field using the data read from a UXML file.
    /// </para>
    ///      </summary>
    public new class UxmlFactory : UnityEngine.UIElements.UxmlFactory<Vector4Field, Vector4Field.UxmlTraits>
    {
    }

    /// <summary>
    ///        <para>
    /// Defines UxmlTraits for the Vector4Field.
    /// </para>
    ///      </summary>
    public new class UxmlTraits : BaseField<Vector4>.UxmlTraits
    {
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
        ((BaseField<Vector4>) ve).SetValueWithoutNotify(new Vector4(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc)));
      }

      public UxmlTraits() : base()
      {
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

#endif