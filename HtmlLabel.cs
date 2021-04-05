// "Therefore those skilled at the unorthodox
// are infinite as heaven and earth,
// inexhaustible as the great rivers.
// When they come to an end,
// they begin again,
// like the days and months;
// they die and are reborn,
// like the four seasons."
// 
// - Sun Tsu,
// "The Art of War"

using System;
using Avalonia.HtmlRenderer;
using Avalonia.Media;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.Avalonia.Adapters;
using TheArtOfDev.HtmlRenderer.Core;

namespace Avalonia.Controls.Html
{
    /// <summary>
    /// Provides HTML rendering using the text property.<br/>
    /// WPF control that will render html content in it's client rectangle.<br/>
    /// Using <see cref="AutoSize"/> and <see cref="AutoSizeHeightOnly"/> client can control how the html content effects the
    /// size of the label. Either case scrollbars are never shown and html content outside of client bounds will be clipped.
    /// MaxWidth/MaxHeight and MinWidth/MinHeight with AutoSize can limit the max/min size of the control<br/>
    /// The control will handle mouse and keyboard events on it to support html text selection, copy-paste and mouse clicks.<br/>
    /// </summary>
    /// <remarks>
    /// See <see cref="HtmlControl"/> for more info.
    /// </remarks>
    public class HtmlLabel : HtmlControl
    {
        #region Dependency properties

        public static readonly AvaloniaProperty AutoSizeProperty = PropertyHelper.Register<HtmlLabel, bool>("AutoSize", true, OnAvaloniaProperty_valueChanged);
        public static readonly AvaloniaProperty AutoSizeHeightOnlyProperty = PropertyHelper.Register<HtmlLabel, bool>("AutoSizeHeightOnly", false, OnAvaloniaProperty_valueChanged);
        public static readonly StyledProperty<bool> AutoSizeProperty =
            AvaloniaProperty.Register<HtmlLabel, bool>("AutoSize", true);
        public static readonly StyledProperty<bool> AutoSizeHeightOnlyProperty =
            AvaloniaProperty.Register<HtmlLabel, bool>("AutoSizeHeightOnly", false);

        #endregion


        /// <summary>
        /// Init.
        /// </summary>
        static HtmlLabel()
        {
            BackgroundProperty.OverrideDefaultValue<HtmlLabel>(Brushes.Transparent);
            AffectsMeasure<HtmlLabel>(AutoSizeProperty, AutoSizeHeightOnlyProperty);
        }

        /// <summary>
        /// Automatically sets the size of the label by content size
        /// </summary>
        public bool AutoSize
        {
            get => GetValue(AutoSizeProperty);
            set => SetValue(AutoSizeProperty, value);
        }

        /// <summary>
        /// Automatically sets the height of the label by content height (width is not effected).
        /// </summary>
        public virtual bool AutoSizeHeightOnly
        {
            get => GetValue(AutoSizeHeightOnlyProperty);
            set => SetValue(AutoSizeHeightOnlyProperty, value);
        }

        #region Private methods

        /// <summary>
        /// Perform the layout of the html in the control.
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            if (_htmlContainer != null)
            {
                using (var ig = new GraphicsAdapter())
                {
                    var horizontal = Padding.Left + Padding.Right + BorderThickness.Left + BorderThickness.Right;
                    var vertical = Padding.Top + Padding.Bottom + BorderThickness.Top + BorderThickness.Bottom;

                    var size = new Size(Math.Min(MaxWidth, constraint.Width), Math.Min(MaxHeight, constraint.Height));
                    var maxSize = new RSize(size.Width < Double.PositiveInfinity ? size.Width - horizontal : 0, size.Height < Double.PositiveInfinity ? size.Height - vertical : 0);
                    _htmlContainer.HtmlContainerInt.MaxSize = maxSize;

                    _htmlContainer.HtmlContainerInt.PerformLayout(ig);
                    var newSize = _htmlContainer.ActualSize;
                    constraint = new Size(newSize.Width + horizontal, newSize.Height + vertical);
                    _htmlContainer.HtmlContainerInt.PageSize = _htmlContainer.HtmlContainerInt.ActualSize;
                }
            }

            if (double.IsPositiveInfinity(constraint.Width) || double.IsPositiveInfinity(constraint.Height))
                constraint = Size.Empty;

            return constraint;
        }


        /// <summary>
        /// Handle when dependency property value changes to update the underline HtmlContainer with the new value.
        /// </summary>
        private static Action<IAvaloniaObject, AvaloniaPropertyChangedEventArgs> OnAvaloniaProperty_valueChanged = new Action<IAvaloniaObject, AvaloniaPropertyChangedEventArgs>((AvaloniaObject, e) =>
        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> e)
        {
            var control = AvaloniaObject as HtmlLabel;
            if (control != null)
            base.OnPropertyChanged(e);

            if (e.Property == AutoSizeProperty)
            {
                if (e.Property == AutoSizeProperty)
                if (e.NewValue.GetValueOrDefault<bool>())
                {
                    if ((bool)e.NewValue)
                    {
                        AvaloniaObject.SetValue(AutoSizeHeightOnlyProperty, false);
                        control.InvalidateMeasure();
                        control.InvalidateVisual();
                    }
                    SetValue(AutoSizeHeightOnlyProperty, false);
                }
                else if (e.Property == AutoSizeHeightOnlyProperty)
            }
            else if (e.Property == AutoSizeHeightOnlyProperty)
            {
                if (e.NewValue.GetValueOrDefault<bool>())
                {
                    if ((bool)e.NewValue)
                    {
                        AvaloniaObject.SetValue(AutoSizeProperty, false);
                        control.InvalidateMeasure();
                        control.InvalidateVisual();
                    }
                    SetValue(AutoSizeProperty, false);
                }
            }
        });

        }
        #endregion
    }
}