namespace flashquizz.Controls;

public partial class AnimatedEntry : ContentView
{
    public Entry EntryControl => InnerEntry;

    public AnimatedEntry()
    {
        InitializeComponent();
    }

    // Placeholder
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AnimatedEntry), default(string));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    // Text
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(AnimatedEntry), default(string), BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // Animation
    private void AnimateWidth(double from, double to)
    {
        var animation = new Animation(v => Underline.WidthRequest = v, from, to);
        animation.Commit(this, "UnderlineAnim", 16, 250, Easing.CubicOut);
    }

    private void OnFocused(object sender, FocusEventArgs e)
    {
        AnimateWidth(Underline.WidthRequest, 300);
    }

    private void OnUnfocused(object sender, FocusEventArgs e)
    {
        AnimateWidth(Underline.WidthRequest, 100);
        Text = InnerEntry.Text;
    }
}
