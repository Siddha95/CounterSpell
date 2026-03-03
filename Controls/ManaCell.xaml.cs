using System.Windows.Input;

namespace MauiApp1.Controls;

public partial class ManaCell : ContentView
{
    // ── Bindable Properties ──────────────────────────────────────────────

    public static readonly BindableProperty ManaNameProperty =
        BindableProperty.Create(nameof(ManaName), typeof(string), typeof(ManaCell), string.Empty);

    public static readonly BindableProperty CircleColorProperty =
        BindableProperty.Create(nameof(CircleColor), typeof(Color), typeof(ManaCell), Colors.Gray);

    public static readonly BindableProperty CountProperty =
        BindableProperty.Create(nameof(Count), typeof(int), typeof(ManaCell), 0);

    public static readonly BindableProperty IncrementCommandProperty =
        BindableProperty.Create(nameof(IncrementCommand), typeof(ICommand), typeof(ManaCell));

    public static readonly BindableProperty DecrementCommandProperty =
        BindableProperty.Create(nameof(DecrementCommand), typeof(ICommand), typeof(ManaCell));

    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(nameof(IconSource), typeof(string), typeof(ManaCell), string.Empty,
            propertyChanged: (b, _, _) =>
            {
                var cell = (ManaCell)b;
                cell.OnPropertyChanged(nameof(HasIcon));
                cell.OnPropertyChanged(nameof(HasNoIcon));
            });

    // ── Public Properties ────────────────────────────────────────────────

    public string ManaName
    {
        get => (string)GetValue(ManaNameProperty);
        set => SetValue(ManaNameProperty, value);
    }

    public Color CircleColor
    {
        get => (Color)GetValue(CircleColorProperty);
        set => SetValue(CircleColorProperty, value);
    }

    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    public ICommand? IncrementCommand
    {
        get => (ICommand?)GetValue(IncrementCommandProperty);
        set => SetValue(IncrementCommandProperty, value);
    }

    public ICommand? DecrementCommand
    {
        get => (ICommand?)GetValue(DecrementCommandProperty);
        set => SetValue(DecrementCommandProperty, value);
    }

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public bool HasIcon   => !string.IsNullOrEmpty(IconSource);
    public bool HasNoIcon => !HasIcon;

    // ── Constructor ──────────────────────────────────────────────────────

    public ManaCell()
    {
        InitializeComponent();
    }
}
