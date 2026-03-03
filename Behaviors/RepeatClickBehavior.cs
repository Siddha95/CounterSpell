namespace MauiApp1.Behaviors;

public class RepeatClickBehavior : Behavior<Button>
{
    private const int InitialDelayMs  = 400;
    private const int RepeatIntervalMs = 80;

    private CancellationTokenSource? _cts;

    protected override void OnAttachedTo(Button button)
    {
        base.OnAttachedTo(button);
        button.Pressed  += OnPressed;
        button.Released += OnReleased;
    }

    protected override void OnDetachingFrom(Button button)
    {
        base.OnDetachingFrom(button);
        button.Pressed  -= OnPressed;
        button.Released -= OnReleased;
        _cts?.Cancel();
    }

    private void OnPressed(object? sender, EventArgs e)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        _ = RepeatAsync((Button)sender!, _cts.Token);
    }

    private void OnReleased(object? sender, EventArgs e)
    {
        _cts?.Cancel();
        _cts = null;
    }

    private static async Task RepeatAsync(Button button, CancellationToken token)
    {
        try
        {
            await Task.Delay(InitialDelayMs, token);

            while (!token.IsCancellationRequested)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (button.Command?.CanExecute(button.CommandParameter) == true)
                        button.Command.Execute(button.CommandParameter);
                });

                await Task.Delay(RepeatIntervalMs, token);
            }
        }
        catch (OperationCanceledException)
        {
            // cancellazione normale al rilascio del dito
        }
    }
}
