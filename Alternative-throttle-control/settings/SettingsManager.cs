using GTA;

using GTAScreen
      = GTA.UI.Screen;

using System.IO;

using System.Drawing;

using System.Windows.Forms;

using System.Collections.Generic;

using Alternative_throttle_control.user_interfaces.creators.resources.structs;

namespace Alternative_throttle_control.settings
{
    internal sealed class SettingsManager
    {
        internal string PathToTheAlternativeThrottleControlFolder
        {
            get
            {
                return _
                        = Directory
                            .GetCurrentDirectory()
                         +
                         @"\scripts\AlternativeThrottleControl";
            }
        }
        internal string PathToTheUserInterfaceResourcesFolder
        {
            get
            {
                return _
                       = PathToTheAlternativeThrottleControlFolder
                         +
                         @"\UserInterfaceResources";
            }
        }
        internal string PathToTheThrottleInterfaceFolder
        {
            get
            {
                return _
                       = PathToTheUserInterfaceResourcesFolder
                         +
                         @"\ThrottleInterface";
            }
        }
        internal string PathToTheCustomSpriteFolder
        {
            get
            {
                return _
                       = PathToTheThrottleInterfaceFolder
                         +
                         @"\CustomSprite";
            }
        }

        internal string PathToTheInterfaceSetupFile
        {
            get
            {
                return _
                       = PathToTheThrottleInterfaceFolder
                         +
                         @"\InterfaceSetup.ini";
            }
        }
        internal string PathToDisplayCompatibilityFile
        {
            get
            {
                return _
                       = PathToTheUserInterfaceResourcesFolder
                         +
                         @"\DisplayCompatibility.ini";
            }
        }
        internal string PathToBehaviorOfUserInterfaceElementsFile
        {
            get
            {
                return _
                       = PathToTheAlternativeThrottleControlFolder
                         +
                         @"\BehaviorOfUserInterfaceElements.ini";
            }
        }
        internal string PathToScriptBehaviorFile
        {
            get
            {
                return _
                       = PathToTheAlternativeThrottleControlFolder
                         +
                         @"\ScriptBehavior.ini";
            }
        }

        internal float ReturnTheThrottleSensitivity()
        {
            var throttleConfigurationFile
                = ScriptSettings
                    .Load(PathToScriptBehaviorFile);

            var throttleSensitivity
                = throttleConfigurationFile
                    .GetAllValues<float>("ThrottleConfiguration", "ThrottleSensitivity")[0];

            return throttleSensitivity;
        }

        internal bool ReturnTheInterfaceVisibility()
        {
            var behaviorOfUserInterfaceElementsFile
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var interfaceVisibility
                = false;

            var section
                = "InterfaceVisibility";

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

        internal Keys ReturnTheKeyForTheThrottleIncrease()
        {
            var keyConfigurationFile
                = ScriptSettings
                    .Load(PathToScriptBehaviorFile);

            var keyForThrottleIncrease
                = keyConfigurationFile
                    .GetAllValues<Keys>("KeyConfiguration", "KeyForTheThrottleIncrease")[0];

            return keyForThrottleIncrease;
        }
        internal Keys ReturnTheKeyForTheThrottleDecrease()
        {
            var keyConfigurationFile
                = ScriptSettings
                    .Load(PathToScriptBehaviorFile);

            var keyForThrottleDecrease
                = keyConfigurationFile
                    .GetAllValues<Keys>("KeyConfiguration", "KeyForTheThrottleDecrease")[0];

            return keyForThrottleDecrease;
        }

        internal PointF ReturnTheInterfaceOffsetPosition()
        {
            var interfaceOffsetPosition
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var offsetPositionX
                = interfaceOffsetPosition
                    .GetAllValues<string>(section: "InterfaceOffsetPosition",
                                          name   : "X")[0];

            var offsetPositionY
                = interfaceOffsetPosition
                    .GetAllValues<string>(section: "InterfaceOffsetPosition",
                                          name   : "Y")[0];
            
            return _
                   = new PointF(x: float
                                    .Parse(offsetPositionX),
                                y: float
                                    .Parse(offsetPositionY));
        }

        internal string  ReturnTheMeasurementSystemOfSpeedometer()
        {
            var behaviorOfUserInterfaceElementsFile
                = ScriptSettings
                    .Load(PathToBehaviorOfUserInterfaceElementsFile);

            var measurementSystem
                = behaviorOfUserInterfaceElementsFile
                    .GetAllValues<string>(section: "MeasurementSystemOfSpeedometer",
                                          name   : "_")[0];

            return measurementSystem;
        }

        internal PointF ReturnThePositionOfCenterOfScreen()
        {
            var displayCompatibility 
                = ScriptSettings
                    .Load(PathToDisplayCompatibilityFile);

            var aspectRatio 
                = GTAScreen
                    .AspectRatio;

            var screenCompatibility 
                = displayCompatibility
                    .GetAllValues<string>(section: "Compatibility",
                                          name   : $"{aspectRatio}")[0];

            var screenCenterPosition 
                = displayCompatibility
                    .GetAllValues<string>(section: screenCompatibility,
                                          name   : "ScreenCenterPosition")[0];

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
            var interfaceSetupFile
                = ScriptSettings
                        .Load(PathToTheInterfaceSetupFile);

            var unconvertedColor
                = interfaceSetupFile
                        .GetAllValues<string>(section, "Color")[0].Replace("A", "")
                                                                  .Replace("R", "")
                                                                  .Replace("G", "")
                                                                  .Replace("B", "")
                                                                  .Replace(":", "")
                                                                    .Split(' ');

            var convertedColor
                = new[]
                {
                    byte
                        .Parse(unconvertedColor[0]),
                    byte
                        .Parse(unconvertedColor[1]),
                    byte
                        .Parse(unconvertedColor[2]),
                    byte
                        .Parse(unconvertedColor[3])
                };

            return _
                   = Color
                        .FromArgb(alpha: convertedColor[0],
                                  red: convertedColor[1],
                                  green: convertedColor[2],
                                  blue: convertedColor[3]);
        }

        internal IList<string> ReturnOneListOfInterfaceComponentsName()
        {
            var allComponentsName
                = new List<string>();

            var allComponentsInformation
                = ReturnOneListOfInterfaceComponentsInformation();

            foreach (var componentInformation in allComponentsInformation)
            {
                allComponentsName
                    .Add(componentInformation.Replace("NAME", "")
                                             .Replace("TYPE", "")
                                             .Replace(":", "")
                                                .Split(' ')[0]);
            }

            return _
                   = allComponentsName;
        }
        internal IList<string> ReturnOneListOfInterfaceComponentsInformation()
        {
            var componentsInformation
                = new List<string>();

            var interfaceSetupFile
                = ScriptSettings
                        .Load(PathToTheInterfaceSetupFile);

            var allComponentsInformation
                = interfaceSetupFile
                    .GetAllValues<string>("ComponentsInformation", "Component");

            foreach (var ComponentInformation in allComponentsInformation)
            {
                componentsInformation
                    .Add(ComponentInformation);
            }

            return _
                   = componentsInformation;
        }

        internal StCustomSpriteConfiguration ReturnStCustomSpriteConfigurationOfThis(string filename)
        {
            var pathToTheImagesThatMakeUpTheInterface
                = PathToTheCustomSpriteFolder + $@"\{filename}.png";

            var customPositionOfCenterOfScreen
                = ReturnTheCustomPositionOfCenterOfScreen();

            var offsetPosition
                = ReturnTheInterfaceOffsetPosition();

            var customPositionWithInterfaceOffset
                = new PointF(customPositionOfCenterOfScreen.X + offsetPosition.X,
                             customPositionOfCenterOfScreen.Y + offsetPosition.Y);

            var customColorOfCustomSprite
                = ReturnTheColorOfThis(filename);

            var customSizeOfCustomSprite
                = ReturnTheCustomSizeOfThis(pathToTheImagesThatMakeUpTheInterface);

            return _
                   =
                   new StCustomSpriteConfiguration()
                   {
                       Filename
                       = pathToTheImagesThatMakeUpTheInterface,

                       Position
                       = customPositionWithInterfaceOffset,

                       Color
                       = customColorOfCustomSprite,

                       Size
                       = customSizeOfCustomSprite,
                   };
        }

        internal StTextElementConfiguration ReturnStTextElementConfigurationOfThis(string caption)
        {
            var customPositionOfCenterOfScreen
                = ReturnTheCustomPositionOfCenterOfScreen();

            var offsetPosition
                = ReturnTheInterfaceOffsetPosition();

            var customPositionWithInterfaceOffset
                = new PointF(customPositionOfCenterOfScreen.X + offsetPosition.X,
                             customPositionOfCenterOfScreen.Y + offsetPosition.Y);

            var customColorOfThisTextElement
                = ReturnTheColorOfThis(caption);

            var customScaleOfThisTextElement
                = 0.35f;

            return _
                   =
                   new StTextElementConfiguration()
                   {
                       Caption
                       = caption,

                       Position
                       = customPositionWithInterfaceOffset,

                       Color
                       = customColorOfThisTextElement,

                       Scale
                       = customScaleOfThisTextElement,
                   };
        }
    }
}
