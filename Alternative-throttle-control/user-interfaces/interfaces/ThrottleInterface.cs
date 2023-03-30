using Alternative_throttle_control.settings;
using Alternative_throttle_control.user_interfaces.managers;
using GTA;
using GTA.UI;
using System.Drawing;
using GTAFont
      = GTA.UI.Font;


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
                    ReturnCustomSpriteDefaultLayoutSpeedometer(),
                    ReturnCustomSpriteDefaultLayout(),
                    ReturnCustomSpriteLine()
                };

            _containerElement
                .Items.Add(_customSprites[0]);

            _containerElement
                .Items.Add(_customSprites[1]);

            _containerElement
                .Items.Add(_customSprites[2]);

            if (_textElements != null)
            {
                return;
            }

            _textElements
                = new[]
                {
                    ReturnTextElementMeasurementSystem(),
                    ReturnTextElementSpeedometer(),
                    ReturnTextElementThrottleUp(),
                    ReturnTextElementThrottleDown()
                };

            _containerElement
                .Items.Add(_textElements[0]);

            _containerElement
                .Items.Add(_textElements[1]);

            _containerElement
                .Items.Add(_textElements[2]);

            _containerElement
                .Items.Add(_textElements[3]);
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
            var throttleUpValue
                = Game
                    .GetControlValueNormalized(Control.VehicleFlyThrottleUp);

            if (throttleUpValue != 0f)
            {
                var updatePosition
                    = new PointF(_sliderInitialPosition.X - (throttleUpValue * 240f / 2f),
                                 _sliderInitialPosition.Y);

                _textElements[2]
                    .Caption = $"ThrUp - {throttleUpValue * 100f:N1}%";

                _customSprites[2]
                    .Position = updatePosition;

                return;
            }

            _textElements[2]
                    .Caption = $"ThrUp - 0%";

            var throttleDownValue
                = Game
                    .GetControlValueNormalized(Control.VehicleFlyThrottleDown);

            if (throttleDownValue != 0f)
            {
                var updatePosition
                    = new PointF(_sliderInitialPosition.X + (throttleDownValue * 232f / 2f),
                                 _sliderInitialPosition.Y);

                _textElements[3]
                    .Caption = $"{throttleDownValue * 100f:N1}% - ThrDown";

                _customSprites[2]
                    .Position = updatePosition;

                return;
            }

            _textElements[3]
                    .Caption = $"0% - ThrDown";

            if (_sliderInitialPosition != new PointF(0f, -9f))
            {
                _sliderInitialPosition
                    = new PointF(0f, -9f);

                _containerElement
                    .Items[2]
                            .Position = _sliderInitialPosition;
            }
        }

        //private void UpdateTheYawLine()
        //{
        //    var throttleDownValue
        //        = Game
        //            .GetControlValueNormalized(Control.VehicleFlyThrottleDown);

        //    if (throttleDownValue != 0f)
        //    {
        //        var updatePosition
        //            = new PointF(_sliderInitialPosition.X + (throttleDownValue * 117.5f),
        //                         _sliderInitialPosition.Y);

        //        _textElements[3]
        //            .Caption = $"{throttleDownValue * 100f:N1}% - ThrottleDown";

        //        _containerElement
        //            .Items[1]
        //                    .Position = updatePosition;

        //        return;
        //    }

        //    _textElements[3]
        //            .Caption = $"0% - ThrottleDown";

        //    var throttleUpValue
        //        = Game
        //            .GetControlValueNormalized(Control.VehicleFlyThrottleUp);

        //    if (throttleUpValue != 0f)
        //    {
        //        var updatePosition
        //            = new PointF(_sliderInitialPosition.X - (throttleUpValue * 117.5f),
        //                         _sliderInitialPosition.Y);

        //        _textElements[2]
        //            .Caption = $"ThrottleUp: {throttleUpValue * 100f:N1}%";

        //        _containerElement
        //            .Items[1]
        //                    .Position = updatePosition;

        //        return;
        //    }

        //    _textElements[2]
        //            .Caption = $"ThrottleUp - 0%";

        //    if (_sliderInitialPosition != new PointF(0f, -2f))
        //    {
        //        _sliderInitialPosition
        //            = new PointF(0f, -2.5f);

        //        _containerElement
        //            .Items[1]
        //                    .Position = _sliderInitialPosition;
        //    }
        //}

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

        private CustomSprite ReturnCustomSpriteDefaultLayoutSpeedometer()
        {
            var customSpriteDefaultLayout
                = ReturnTheCustomSpritesOfThis(filename: "DefaultLayoutSpeedometer");

            customSpriteDefaultLayout
                .Centered = true;

            customSpriteDefaultLayout
                .Position = new PointF(82f, -38f);

            return _
                   = customSpriteDefaultLayout;
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
                .Position = new PointF(0f, -9f);

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

        private TextElement ReturnTextElementThrottleUp()
        {
            var textElementFlyThrottleUp
                = ReturnTheTextElementOfThis("ThrottleUp");

            textElementFlyThrottleUp
                .Font = GTAFont.Pricedown;

            textElementFlyThrottleUp
                .Alignment = Alignment.Right;

            textElementFlyThrottleUp
                .Position = new PointF(0f, 10f);

            textElementFlyThrottleUp
                .Scale = 0.35f;

            return _
                   = textElementFlyThrottleUp;
        }
        private TextElement ReturnTextElementThrottleDown()
        {
            var textElementFlyThrottleDown
                = ReturnTheTextElementOfThis("ThrottleDown");

            textElementFlyThrottleDown
                .Font = GTAFont.Pricedown;

            textElementFlyThrottleDown
                .Alignment = Alignment.Left;

            textElementFlyThrottleDown
                .Position = new PointF(4f, 10f);

            textElementFlyThrottleDown
                .Scale = 0.35f;

            return _
                   = textElementFlyThrottleDown;
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
                .Font = GTAFont.Pricedown;

            textElementFlyYawRight
                .Alignment = Alignment.Center;

            textElementFlyYawRight
                .Position = new PointF(81f, -64f);

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
                .Font = GTAFont.Pricedown;

            textElementFlyYawLeft
                .Alignment = Alignment.Center;

            textElementFlyYawLeft
                .Position = new PointF(81f, -40f);

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