// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using System.Collections.Generic;
// ReSharper disable All
namespace Doozy.Runtime.UIManager.Containers
{
    public partial class UIView
    {
        public static IEnumerable<UIView> GetViews(UIViewId.FlowLoading id) => GetViews(nameof(UIViewId.FlowLoading), id.ToString());
        public static void Show(UIViewId.FlowLoading id, bool instant = false) => Show(nameof(UIViewId.FlowLoading), id.ToString(), instant);
        public static void Hide(UIViewId.FlowLoading id, bool instant = false) => Hide(nameof(UIViewId.FlowLoading), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.FlowMainmenu id) => GetViews(nameof(UIViewId.FlowMainmenu), id.ToString());
        public static void Show(UIViewId.FlowMainmenu id, bool instant = false) => Show(nameof(UIViewId.FlowMainmenu), id.ToString(), instant);
        public static void Hide(UIViewId.FlowMainmenu id, bool instant = false) => Hide(nameof(UIViewId.FlowMainmenu), id.ToString(), instant);
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UIViewId
    {
        public enum FlowLoading
        {
            Loading
        }

        public enum FlowMainmenu
        {
            Ingame,
            Lose,
            Mainmenu,
            Setting,
            Win
        }    
    }
}
