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
namespace Doozy.Runtime.UIManager.Components
{
    public partial class UIButton
    {
        public static IEnumerable<UIButton> GetButtons(UIButtonId.MainMenu id) => GetButtons(nameof(UIButtonId.MainMenu), id.ToString());
        public static bool SelectButton(UIButtonId.MainMenu id) => SelectButton(nameof(UIButtonId.MainMenu), id.ToString());

        public static IEnumerable<UIButton> GetButtons(UIButtonId.Navigation id) => GetButtons(nameof(UIButtonId.Navigation), id.ToString());
        public static bool SelectButton(UIButtonId.Navigation id) => SelectButton(nameof(UIButtonId.Navigation), id.ToString());
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UIButtonId
    {
        public enum MainMenu
        {
            Adventure,
            Arena,
            Campaign,
            DailyCheckin,
            EventMonsterDefeat,
            EventPetoRacing,
            EventSurvival,
            EventWorldBoss,
            Guild,
            Heroes,
            Inventory,
            Leaderboard,
            LuckySpin,
            MysticalShop,
            Pack7Days,
            PackGold,
            PackMaterial,
            PackSpecial,
            Pet,
            Quest,
            Referral,
            Renting,
            Shop
        }

        public enum Navigation
        {
            Back
        }    
    }
}
