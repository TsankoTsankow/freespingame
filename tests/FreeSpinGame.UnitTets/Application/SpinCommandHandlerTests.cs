using FakeItEasy;
using FreeSpinGame.Application.Features.Campaigns.Commands.Spin;
using FreeSpinGame.Domain.Interfaces;

namespace FreeSpinGame.Application.UnitTets.Application;

public class SpinCommandHandlerTests
{
    private readonly ISpinRepository _fakeRepository;
    private readonly SpinCommandHandler _sut;

    public SpinCommandHandlerTests()
    {
        _fakeRepository = A.Fake<ISpinRepository>();
        _sut = new SpinCommandHandler(_fakeRepository);
    }
    
    
}