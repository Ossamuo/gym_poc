using System.Diagnostics.Metrics;

namespace Gym.Domain.Contexts.SharedContext.Metrics;

public static class BusinessMetrics
{
    // O nome aqui deve ser exatamente o mesmo configurado no AddMeter do Program.cs
    public static readonly Meter Meter = new("Gym.App.BusinessMetrics", "1.0.0");

    // Cria um contador
    public static readonly Counter<int> BookingCounter = Meter.CreateCounter<int>(
        name: "gymmarketplace.bookings.count",
        unit: "Bookings",
        description: "Conta o número de reservas processadas divididas por status"
    );
}