﻿using Catel.MVVM.Converters;
using NuGetPackageManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NuGetPackageManager.Converters
{
    public class PackageStatusEnumToBrushConverter : ValueConverterBase<PackageStatus, Brush>
    {
        int offset = -1;
        readonly Themes.Brushes resourceDictionary;

        public PackageStatusEnumToBrushConverter()
        {
            resourceDictionary = new Themes.Brushes();

            resourceDictionary.InitializeComponent();
        }

        protected override object Convert(PackageStatus value, Type targetType, object parameter)
        {
            var resourceKeys = parameter as string[];

            if(resourceKeys == null)
            {
                return new SolidColorBrush(Colors.Transparent);
            }

            int keyIndex = (int)value - offset;

            if (keyIndex >= 0 && keyIndex < resourceKeys.Length)
            {
                var brushResource = resourceDictionary[resourceKeys[keyIndex]];

                return brushResource;
            }

            return new SolidColorBrush(Colors.Transparent);
        }
    }
}
