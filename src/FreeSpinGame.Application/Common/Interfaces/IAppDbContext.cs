using FreeSpinGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinGame.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<PlayerSpinState> PlayerSpinStates { get; }
    DbSet<Campaign> Campaigns { get; }
}