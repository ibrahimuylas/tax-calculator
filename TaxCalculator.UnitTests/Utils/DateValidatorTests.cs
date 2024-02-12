using TaxCalculator.Api.Utils;

namespace TaxCalculator.UnitTests;

public class DateValidatorTests
{
    [Scenario]
    [Example(null)]
    [Example(new object[] { new string[] { } })]
    [Example(new object[] { new string[] { "" } })]
    [Example(new object[] { new string[] { "01/01/2015 aa:hh" } })]
    [Example(new object[] { new string[] { "2013-02-07 15:37:00", "date_date" } })]
    public void ShouldReturnFalseWhenOneOfDateIsInvalid(string[] dates)
    {
        bool valid = false;
        "Given the dates contains an invalid date".x(() =>
        {
            valid = DateValidator.AreAllDatesValid(dates);
        });

        "Then AreAllDatesValid should return false".x(() =>
        {
            valid.Should().BeFalse();
        });
    }
}
