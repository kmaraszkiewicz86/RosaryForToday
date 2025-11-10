using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using NSubstitute;

namespace RosaryForToday.Application.Unit.Tests;

public class BaseFixture
{
    protected Fixture Fixture { get; }

    // A freezeable IMapper substitute for tests to configure mappings easily
    protected IMapper Mapper { get; }

    public BaseFixture()
    {
        Fixture = new Fixture().Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });
        // Freeze IMapper so all resolves return the same substitute
        Mapper = Fixture.Freeze<IMapper>();
    }
}