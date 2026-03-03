using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private const int DefaultLifeTotal = 20;

    [ObservableProperty] private int _lifeTotal = DefaultLifeTotal;
    [ObservableProperty] private int _opponentLifeTotal = DefaultLifeTotal;
    [ObservableProperty] private int _manaW;
    [ObservableProperty] private int _manaU;
    [ObservableProperty] private int _manaB;
    [ObservableProperty] private int _manaR;
    [ObservableProperty] private int _manaG;
    [ObservableProperty] private int _manaC;
    [ObservableProperty] private int _stormCount;

    // ── Vita ────────────────────────────────────────────────────────────
    [RelayCommand] private void IncrementLife()         => LifeTotal++;
    [RelayCommand] private void DecrementLife()         => LifeTotal = Math.Max(0, LifeTotal - 1);
    [RelayCommand] private void IncrementOpponentLife() => OpponentLifeTotal++;
    [RelayCommand] private void DecrementOpponentLife() => OpponentLifeTotal = Math.Max(0, OpponentLifeTotal - 1);

    // ── Mana ─────────────────────────────────────────────────────────────
    [RelayCommand] private void IncrementMana(string color) => SetMana(color, GetMana(color) + 1);
    [RelayCommand] private void DecrementMana(string color) => SetMana(color, Math.Max(0, GetMana(color) - 1));

    // ── Storm ─────────────────────────────────────────────────────────────
    [RelayCommand] private void AddSpell()    => StormCount++;
    [RelayCommand] private void RemoveSpell() => StormCount = Math.Max(0, StormCount - 1);

    // ── Reset ─────────────────────────────────────────────────────────────
    [RelayCommand]
    private void ResetTurn() =>
        ManaW = ManaU = ManaB = ManaR = ManaG = ManaC = StormCount = 0;

    [RelayCommand]
    private async Task ResetGameAsync()
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Reset Game",
            "Azzerare tutto e riportare la vita a 20?",
            "Sì, reset",
            "Annulla");

        if (!confirmed) return;

        LifeTotal         = DefaultLifeTotal;
        OpponentLifeTotal = DefaultLifeTotal;
        ResetTurn();
    }

    // ── Helpers ───────────────────────────────────────────────────────────
    private int GetMana(string color) => color switch
    {
        "W" => ManaW, "U" => ManaU, "B" => ManaB,
        "R" => ManaR, "G" => ManaG, "C" => ManaC,
        _ => 0
    };

    private void SetMana(string color, int value)
    {
        switch (color)
        {
            case "W": ManaW = value; break;
            case "U": ManaU = value; break;
            case "B": ManaB = value; break;
            case "R": ManaR = value; break;
            case "G": ManaG = value; break;
            case "C": ManaC = value; break;
        }
    }
}
