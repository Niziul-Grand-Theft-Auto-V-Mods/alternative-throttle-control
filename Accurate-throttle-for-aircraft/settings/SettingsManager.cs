using GTA;
using GTA.UI;

using System;
using System.IO;
using System.Drawing;


namespace Accurate_throttle_for_aircraft.settings
{
    internal sealed class SettingsManager
    {
        internal string PathToTheAccurateThrottleForAircraft
        {
            get
            {
                return _
                       = Directory
                            .GetCurrentDirectory()
                         +
                         @"\scripts\AccurateThrottleForAircraft";
            }
        }
        internal string PathToTheInterfaceResources
        {
            get
            {
                return _
                       = PathToTheAccurateThrottleForAircraft
                         +
                         @"\UserInterfaceResources\CustomSprite";
            }
        }
        internal string PathToTheDefaultLayoutImage
        {
            get
            {
                return _
                       = PathToTheAccurateThrottleForAircraft
                         +
                         @"\UserInterfaceResources\CustomSprite\DefaultLayout.png";
            }
        }
        internal string PathToDisplayCompatibilityFile
        {
            get
            {
                return _
                       = PathToTheAccurateThrottleForAircraft
                         +
                         @"\UserInterfaceResources\DisplayCompatibility.ini";
            }
        }
        internal string PathToBehaviorOfUserInterfaceElementsFile
        {
            get
            {
                return _
                       = PathToTheAccurateThrottleForAircraft
                         +
                         @"\BehaviorOfUserInterfaceElements.ini";
            }
        }
        
        internal bool ReturnTheInterfaceVisibility()
        {
            var behaviorOfUserInterfaceElementsFile
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var interfaceVisibility
                = false;

            var section
                = "Interface Visibility";

            var key
                = "_";
            
            var value
                = behaviorOfUserInterfaceElementsFile
                    .GetAllValues<string>(section,
                                          key)[0];

            if (value != null 
                &&
                value == "On")
            {
                interfaceVisibility
                = true;
            }

            return interfaceVisibility;
        }
        
        internal bool ReturnTheInterfaceDisplayBehavior()
        {
            var behaviorOfUserInterfaceElementsFile
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var interfaceDiplayBehavior
                = false;

            var section
                = "Display Only In Autorotation";

            var key
                = "_";

            var value
                = behaviorOfUserInterfaceElementsFile
                    .GetAllValues<string>(section,
                                          key)[0];

            if (value != null
                &&
                value == "On")
            {
                interfaceDiplayBehavior
                = true;
            }

            return interfaceDiplayBehavior;
        }

        internal PointF ReturnTheInterfaceOffsetPosition()
        {
            var interfaceOffsetPosition
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var offsetPositionX
                = interfaceOffsetPosition
                    .GetAllValues<string>(section: "Interface Offset Position",
                                          name   : "X")[0];

            var offsetPositionY
                = interfaceOffsetPosition
                    .GetAllValues<string>(section: "Interface Offset Position",
                                          name   : "Y")[0];
            
            return _
                   = new PointF(x: float
                                    .Parse(offsetPositionX),
                                y: float
                                    .Parse(offsetPositionY));
        }

        internal PointF ReturnThePositionOfCenterOfScreen()
        {
            var displayCompatibility 
                = ScriptSettings
                    .Load(PathToDisplayCompatibilityFile);

            var aspectRatio 
                = Screen
                    .AspectRatio;

            var screenCompatibility 
                = displayCompatibility
                    .GetAllValues<string>(section: "Compatibility",
                                          name   : $"{aspectRatio}")[0];

            var screenCenterPosition 
                = displayCompatibility
                    .GetAllValues<string>(section: screenCompatibility,
                                          name   : "Screen Center Position")[0];

            return _ 
                   = new PointF(x: float
                                    .Parse(screenCenterPosition),
                                y: 0f);
        }
        internal PointF ReturnTheCustomPositionOfCenterOfScreen()
        {
            var positionOfCenterOfScreen 
                = ReturnThePositionOfCenterOfScreen();

            return _
                   = new PointF(x: positionOfCenterOfScreen
                                                         .X * 1.8f,
                                y: 680f);
        }
        
        internal SizeF ReturnTheSizeOfThis(string imagePath)
        {
            var sizeOfImage
                = new SizeF();

            using (var image = Image.FromFile(imagePath))
            {
                sizeOfImage
                = image
                    .Size;
            }


            return sizeOfImage;
        }
        internal SizeF ReturnTheCustomSizeOfThis(string imageFile)
        {
            var sizeOfThisImageFile
                = ReturnTheSizeOfThis(imageFile);

            return _
                   = new SizeF(width: sizeOfThisImageFile
                                                        .Width / 2f,
                               height: sizeOfThisImageFile
                                                        .Height / 2f);
        }

        internal Color ReturnTheColorOfThis(string section)
        {
            var behaviorOfUserInterfaceElementsFile
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var keys 
                = new[]
                {
                    "Color - A",
                    "Color - R",
                    "Color - G",
                    "Color - B"
                };

            var unconvertedColor
                = new string[4];

            var convertedColor
                = new byte[4];

            for (var i = (byte)0; i < keys
                                        .Length; i++)
            {
                unconvertedColor
                [i] = behaviorOfUserInterfaceElementsFile
                        .GetAllValues<string>(section, 
                                              keys[i])[0];

                convertedColor
                [i] = byte
                        .Parse(unconvertedColor[i]);
            }

            return _
                   = Color
                        .FromArgb(alpha: convertedColor[0],
                                  red  : convertedColor[1],
                                  green: convertedColor[2],
                                  blue : convertedColor[3]);
        }
    }
}