using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Stunts
{
    public static class TileButtonExtensions
    {
        public static DependencyProperty TerrainProperty = DependencyProperty.RegisterAttached("Terrain", typeof(BaseItem), typeof(TileButtonExtensions));
        public static BaseItem GetTerrain(Button owner) => (BaseItem) owner.GetValue(TerrainProperty);
        public static void SetTerrain(Button owner, BaseItem value) => owner.SetValue(TerrainProperty, value);

        public static DependencyProperty ItemProperty = DependencyProperty.RegisterAttached("Item", typeof(BaseItem), typeof(TileButtonExtensions));
        public static BaseItem GetItem(Button owner) => (BaseItem) owner.GetValue(ItemProperty);
        public static void SetItem(Button owner, BaseItem value) => owner.SetValue(ItemProperty, value);
    }
}
