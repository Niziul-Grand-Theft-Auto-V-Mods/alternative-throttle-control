using GTA;
using GTA.UI;
using GTAFont
      = GTA.UI.Font;

using System.Drawing;

using Alternative_throttle_control.settings;

using Alternative_throttle_control.user_interfaces.managers;


namespace Alternative_throttle_control.user_interfaces.interfaces
{
    internal class ThrottleInterface
    {
        private ContainerElement _containerElement;

        private SettingsManager _settingsManager;

        private CustomSprite[] _customSprites;

        private TextElement[] _textElements;

        private PointF _sliderInitialPosition;

        private string _measurementSystem;

        public ThrottleInterface()
        {
            _settingsManager
                = new SettingsManager();

            _measurementSystem
                = _settingsManager
                        .ReturnTheMeasurementSystemOfSpeedometer();
        }

        internal void BuildTheInterface()
        {
            if (_containerElement != null)
            {
                return;
            }

            _containerElement
                = ReturnContainerElementForTheYawInterface();

            _customSprites
                = new[]
                {
                    ReturnCustomSpriteDefaultLayout(),
                    ReturnCustomSpriteLine()
                };

            _containerElement
                .Items.Add(_customSprites[0]);

            _containerElement
                .Items.Add(_customSprites[1]);

            if (_textElements != null)
            {
                return;
            }

            _textElements
                = new[]
                {
                    ReturnTextElementMeasurementSystem(),
                    ReturnTextElementSpeedometer()
                };

            _containerElement
                .Items.Add(_textElements[0]);

            _containerElement
                .Items.Add(_textElements[1]);
        }

        internal void ShowTheInterface()
        {
            UpdateTheSpeedometer();
            
            UpdateTheThrotlleLine();

            _containerElement
                .ScaledDraw();
        }

        private void UpdateTheThrotlleLine()
        {
            if (Game.GetControlValueNormalized(Control.VehicleFlyThrottleUp) != 0f)
            {
                var throttleValue
                    = Game.GetControlValueNormalized(Control.VehicleFlyThrottleUp);

                _customSprites[1]
                    .Position = new PointF(_sliderInitialPosition.X - (throttleValue * 160.5f),
                                           _sliderInitialPosition.Y);

                return;
            }

            _sliderInitialPosition
                    = new PointF(41f, 4f);
        }

        private void UpdateTheSpeedometer()
        {
            var vehicleSpeed
                = 0f;

            var vehicleSpeedInMetrePerSecond
                = Game.Player.Character.CurrentVehicle.Speed;

            switch (_measurementSystem)
            {
                case "Knot":
                    {
                        vehicleSpeed
                               = vehicleSpeedInMetrePerSecond * 1.943f;
                    }
                    break;
                case "Kmh":
                    {
                        vehicleSpeed
                               = vehicleSpeedInMetrePerSecond * 3.590f;
                    }
                    break;
                case "Mph":
                    {
                        vehicleSpeed
                               = vehicleSpeedInMetrePerSecond * 2.236f;
                    }
                    break;
                default:
                    {
                        vehicleSpeed
                               = vehicleSpeedInMetrePerSecond;
                    }
                    break;
            }

            _textElements[1]
                .Caption = $"{vehicleSpeed:0}";
        }

        internal void RemoveTheInterface()
        {
            if (_containerElement == null)
            {
                return;
            }

            _customSprites
                = null;

            _textElements
                = null;

            _containerElement
                .Items.Clear();

            _containerElement
                = null;
        }

        private ContainerElement ReturnContainerElementForTheYawInterface()
        {
            var containerElement
                = new ContainerElement();

            var customSpriteDefaultLayout
                = ReturnTheCustomSpritesOfThis("DefaultLayout");

            containerElement
                .Position = customSpriteDefaultLayout
                                                .Position;

            containerElement
                .Size = customSpriteDefaultLayout
                                                .Size;

            containerElement
                .Centered = false;

            return _
                   = containerElement;
        }

        private CustomSprite ReturnCustomSpriteDefaultLayout()
        {
            var customSpriteDefaultLayout
                = ReturnTheCustomSpritesOfThis(filename: "DefaultLayout");

            customSpriteDefaultLayout
                .Centered = true;

            customSpriteDefaultLayout
                .Position = PointF.Empty;

            return _
                   = customSpriteDefaultLayout;
        }
        private CustomSprite ReturnCustomSpriteLine()
        {
            var customSpriteLine
                = ReturnTheCustomSpritesOfThis(filename: "Line");

            customSpriteLine
                .Centered = false;

            customSpriteLine
                .Position = new PointF(40f, 3.5f);

            return _
                   = customSpriteLine;
        }
        private CustomSprite ReturnTheCustomSpritesOfThis(string filename)
        {
            var customSpriteManager
                = new CustomSpriteManager();

            var customSprite
                = customSpriteManager
                        .ReturnAnCustomSpriteWithThis(_settingsManager
                                                            .ReturnStCustomSpriteConfigurationOfThis(filename));

            return _
                   = customSprite;
        }

        private TextElement ReturnTextElementMeasurementSystem()
        {
            var textElementFlyYawRight
                = ReturnTheTextElementOfThis("MeasurementSystem");

            textElementFlyYawRight
                .Caption = _measurementSystem;

            textElementFlyYawRight
                .Scale = 0.60f;

            textElementFlyYawRight
                .Outline = false;

            textElementFlyYawRight
                .Shadow = false;

            textElementFlyYawRight
                .Font = GTAFont.Pricedown;

            textElementFlyYawRight
                .Alignment = Alignment.Center;

            textElementFlyYawRight
                .Position = new PointF(81f, -32f);

            return _
                   = textElementFlyYawRight;
        }
        private TextElement ReturnTextElementSpeedometer()
        {
            var textElementFlyYawLeft
                = ReturnTheTextElementOfThis("Speedometer");

            textElementFlyYawLeft
                .Caption = "0";

            textElementFlyYawLeft
                .Scale = 0.55f;

            textElementFlyYawLeft
                .Outline = false;

            textElementFlyYawLeft
                .Shadow = false;

            textElementFlyYawLeft
                .Font = GTAFont.Pricedown;

            textElementFlyYawLeft
                .Alignment = Alignment.Center;

            textElementFlyYawLeft
                .Position = new PointF(81f, -8f);

            return _
                   = textElementFlyYawLeft;
        }
        private TextElement ReturnTheTextElementOfThis(string caption)
        {
            var textElementManager
                = new TextElementManager();

            var textElement
                = textElementManager
                        .ReturnAnTextElementWithThis(_settingsManager
                                                            .ReturnStTextElementConfigurationOfThis(caption));

            return _
                   = textElement;
        }
    }
}