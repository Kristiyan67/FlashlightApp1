using Plugin.Maui.ScreenBrightness;
namespace FlashlightApp;

public partial class MainPage : ContentPage
{
    bool _torchIsOn = false;

    public MainPage()
    {
        InitializeComponent();
    }

    // Button 1: Real camera flashlight toggle
    async void OnTorchClicked(object sender, EventArgs e)
    {
        try
        {
            if (_torchIsOn)
            {
                await Microsoft.Maui.Devices.Flashlight.TurnOffAsync();
                TorchButton.Text = "Turn On Flashlight";
                TorchButton.BackgroundColor = Colors.Gray;
            }
            else
            {
                await Microsoft.Maui.Devices.Flashlight.TurnOnAsync();
                TorchButton.Text = "Turn Off Flashlight";
                TorchButton.BackgroundColor = Colors.Yellow;
            }

            _torchIsOn = !_torchIsOn;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Button 2: Switch to white screen mode
    void OnScreenLightClicked(object sender, EventArgs e)
    {
        ButtonPanel.IsVisible = false;
        ScreenLightOverlay.IsVisible = true;

        double value = BrightnessSlider.Value;
        ScreenLightOverlay.BackgroundColor = new Color((float)value, (float)value, (float)value);

        try
        {
            ScreenBrightness.Default.Brightness = (float)value;
        }
        catch { }
    }

    void OnBrightnessChanged(object sender, ValueChangedEventArgs e)
    {
        double value = e.NewValue;

        ScreenLightOverlay.BackgroundColor = new Color((float)value, (float)value, (float)value);

        try
        {
            ScreenBrightness.Default.Brightness = (float)value;
        }
        catch { }
    }

    // "Turn Off" button inside the white screen
    void OnExitScreenLightClicked(object sender, EventArgs e)
    {
        ScreenLightOverlay.IsVisible = false;
        ButtonPanel.IsVisible = true;
    }
}