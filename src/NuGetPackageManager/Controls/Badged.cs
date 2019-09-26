﻿using Catel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NuGetPackageManager.Controls
{
    [TemplatePart(Name = BadgeContentPartName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = BadgePartName, Type = typeof(FrameworkElement))]
    public class Badged : ContentControl
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        const string BadgeContentPartName = "PART_BadgeContent";
        const string BadgePartName = "PART_Badge";

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetTemplatePartVisibility(this, ToVisibility(IsShowed));
        }

        public object Badge
        {
            get { return (object)GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BadgeProperty =
            DependencyProperty.Register("Badge", typeof(object), typeof(Badged), new FrameworkPropertyMetadata(null));


        public Brush BadgeForeground
        {
            get { return (Brush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        public static readonly DependencyProperty BadgeForegroundProperty =
            DependencyProperty.Register("BadgeForeground", typeof(Brush), typeof(Badged), new PropertyMetadata(null));

        //public Brush BadgeBackground
        //{
        //    get { return (Brush)GetValue(BadgeBackgroundProperty); }
        //    set { SetValue(BadgeBackgroundProperty, value); }
        //}

        //public static readonly DependencyProperty BadgeBackgroundProperty =
        //    DependencyProperty.Register("BadgeBackground", typeof(Brush), typeof(Badged), new PropertyMetadata(null));

        public bool IsShowed
        {
            get { return (bool)GetValue(IsShowedProperty); }
            set { SetValue(IsShowedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowedProperty =
            DependencyProperty.Register("IsShowed", typeof(bool), typeof(Badged), new PropertyMetadata(true, (s,e) => OnIsShowedChanged(s,e)));

        public event DependencyPropertyChangedEventHandler IsShowedChanged;

        private void RaiseIsShowedChanged(DependencyPropertyChangedEventArgs e)
        {
            IsShowedChanged?.Invoke(this, e);
        }

        private static void OnIsShowedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var badged = sender as Badged;

            if(badged == null)
            {
                return;
            }

            var visibility = badged.ToVisibility((bool)e.NewValue);
            badged.SetTemplatePartVisibility(badged, visibility);

            badged.RaiseIsShowedChanged(e);
        }

        private void SetTemplatePartVisibility(Badged b, Visibility visibility)
        {
            var templateBadgeContent = b.GetTemplateChild(BadgeContentPartName);
            var templateBadge = b.GetTemplateChild(BadgePartName);

            Log.Info($"{b.DataContext}: {visibility}");

            templateBadgeContent?.SetCurrentValue(VisibilityProperty, visibility);
            templateBadge?.SetCurrentValue(VisibilityProperty, visibility);
        }

        private Visibility ToVisibility(bool value)
        {
            return value ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
