using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    CalculateRebateResult CalculateRebate(CalculateRebateRequest request);
}
