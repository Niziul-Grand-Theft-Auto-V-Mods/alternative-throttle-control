using GTA;
using GTA.UI;

using System.Drawing;

using Accurate_throttle_for_aircraft.settings;
using Accurate_throttle_for_aircraft.user_interfaces.managers;
using Accurate_throttle_for_aircraft.user_interfaces.creators.resources.structs;

namespace Accurate_throttle_for_aircraft.user_interfaces
{
    internal sealed class UserInterfaces : Script
    {
        public UserInterfaces()
        {
            var settingsManager
                = new SettingsManager();
            
            var interfaceVisibility
                = settingsManager
                    .ReturnTheInterfaceVisibility();

            if (!interfaceVisibility)
                Pause();

            var throttleInterface
                = new ThrottleInterface();

            Tick    += (o, e) =>
            {
                if (Game.Player.Character.IsInFlyingVehicle)
                {
                    throttleInterface
                        .BuildTheInterface();

                    throttleInterface
                        .ShowTheInterface();

                    return;
                }

                throttleInterface
                    .RemoveTheInterface();
            };

            Aborted += (o, e) =>
            {
                throttleInterface
                    .RemoveTheInterface();
            };
        }
    }

    internal sealed class ThrottleInterface
    {
        private CustomSprite[] _imagensForTheInterface;

        private Elements _elementsForTheThrottleInterface;
        
        private PointF _lineImagePosition;
        
        public ThrottleInterface()
        {
            _elementsForTheThrottleInterface 
                = new Elements(_imagensForTheInterface);
        }

        private CustomSprite[] ReturnTheImagensForTheInterface()
        {
            var customSpriteManager
                = new CustomSpriteManager();

            return _
                   =
                   new[]
                   {
                       customSpriteManager
                           .ReturnAnCustomSpriteWithThis(ReturnStCustomSpriteConfigurationOfThis("DefaultLayout")),
                       customSpriteManager
                           .ReturnAnCustomSpriteWithThis(ReturnStCustomSpriteConfigurationOfThis("Line"))
                   };
        }

        private CustomSprite[] ReturnTheImagesOrganizedAndReadyForTheInterface()
        {
            var settingsManager
                = new SettingsManager();

            var interfaceOffsetPosition
                = settingsManager
                    .ReturnTheInterfaceOffsetPosition();

            var imagensForTheInterface
                = ReturnTheImagensForTheInterface();

            var defaultLayoutImage
                = imagensForTheInterface[0];

            var lineImage
                = imagensForTheInterface[1];

            defaultLayoutImage
                .Centered = true;

            lineImage
                .Centered = false;

            var defaultLayoutImagePosition
                = defaultLayoutImage
                        .Position;
                
            var lineImagePosition
                = lineImage
                        .Position;

            var defaultLayoutImageOffsetPosition
                = new PointF(defaultLayoutImagePosition.X + interfaceOffsetPosition.X,
                             defaultLayoutImagePosition.Y + interfaceOffsetPosition.Y);

            var lineImageOffsetPosition
                = new PointF(lineImagePosition.X + interfaceOffsetPosition.X - 120f,
                             lineImagePosition.Y + interfaceOffsetPosition.Y - 14f );

            defaultLayoutImage
                .Position = defaultLayoutImageOffsetPosition;

            lineImage
                .Position = lineImageOffsetPosition;

            return _
                   =
                   new[]
                   {
                       defaultLayoutImage,
                       lineImage
                   };
        }
        
        private StCustomSpriteConfiguration ReturnStCustomSpriteConfigurationOfThis(string imageName)
        {
            var settingsManager
                = new SettingsManager();

            var pathToTheInterfaceResources
                = settingsManager
                        .PathToTheInterfaceResources;

            var pathToTheImagesThatMakeUpTheInterface
                = pathToTheInterfaceResources + $@"\{imageName}.png";

            var customPositionOfCenterOfScreen
                = settingsManager
                        .ReturnTheCustomPositionOfCenterOfScreen();

            var customColorOfCustomSprite
                = settingsManager
                        .ReturnTheColorOfThis(imageName);

            var customSizeOfCustomSprite
                = settingsManager
                        .ReturnTheCustomSizeOfThis(pathToTheImagesThatMakeUpTheInterface);

            return _
                   =
                   new StCustomSpriteConfiguration()
                   {
                       Filename
                       = pathToTheImagesThatMakeUpTheInterface,

                       Position
                       = customPositionOfCenterOfScreen,

                       Color
                       = customColorOfCustomSprite,

                       Size
                       = customSizeOfCustomSprite,
                   };
        }
        
        private void PutTheImageOfTheLineAtTheBeginningOfTheSlideBar()
        {
            var lineImage
                = _imagensForTheInterface[1];

            var lineImagePosition
                = lineImage
                        .Position;

            var beginningOfTheSlideBar
                = new PointF(x: 159.5f,
                             y: 0f);

            var beginningPositionOfTheSlideBar
                = new PointF(x: lineImagePosition.X + beginningOfTheSlideBar.X, 
                             y: lineImagePosition.Y + beginningOfTheSlideBar.Y);

            lineImage
                .Position = beginningPositionOfTheSlideBar;
        }
        
        internal void BuildTheInterface()
        {
            for (var i = 0; i < 2; i++)
            {
                if (_imagensForTheInterface == null)
                {
                    _imagensForTheInterface
                        = ReturnTheImagensForTheInterface();

                    _imagensForTheInterface
                        = ReturnTheImagesOrganizedAndReadyForTheInterface();

                    PutTheImageOfTheLineAtTheBeginningOfTheSlideBar();

                    _lineImagePosition 
                        = _imagensForTheInterface[1].Position;

                    _elementsForTheThrottleInterface
                        = new Elements(_imagensForTheInterface);
                }
            }
        }

        internal void RemoveTheInterface()
        {
            for (var i = 0; i < 2; i++)
            {
                if (_imagensForTheInterface != null)
                {
                    _imagensForTheInterface = null;
                }
            }
        }
        
        internal void ShowTheInterface()
        {
            var lineImage
                = _imagensForTheInterface[1];

            var updatePosition
                = new PointF(_lineImagePosition.X - (Game.GetControlValueNormalized(Control.VehicleFlyThrottleUp) * 159.9f),
                             _lineImagePosition.Y);

            lineImage
                .Position = updatePosition;

            _elementsForTheThrottleInterface
                .ScaledDraw();
        }
    }
}