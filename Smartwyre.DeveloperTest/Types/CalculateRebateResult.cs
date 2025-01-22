namespace Smartwyre.DeveloperTest.Types;

public class CalculateRebateResult
{
    public bool Success { get; set; }
    //Added this property for improving testability
    public decimal RebateAmount { get; set; }
}
