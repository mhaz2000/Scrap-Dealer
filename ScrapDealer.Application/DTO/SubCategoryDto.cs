using DNTPersianUtils.Core;

namespace ScrapDealer.Application.DTO
{
    public class SubCategoryDto(Guid id, string name, decimal minPrice, decimal maxPrice, string lastUpdate, Guid parentCategoryId, ICollection<Guid> images)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public decimal MinPrice { get; set; } = minPrice;
        public decimal MaxPrice { get; set; } = maxPrice;
        public Guid ParentCategoryId { get; set; } = parentCategoryId;
        public ICollection<Guid> Images { get; set; } = images;
        public string LastUpdate { get; set; } = lastUpdate;


        public decimal LastMinPrice { get; set; }
        public decimal LastMaxPrice { get; set; }
        public string DailyMinPriceChangeRate => CalculatePercentageChange(MinPrice, LastMinPrice);
        public string DailyMaxPriceChangeRate => CalculatePercentageChange(MaxPrice, LastMaxPrice);

        public decimal LastWeekMinPrice { get; set; }
        public decimal LastWeekMaxPrice { get; set; }
        public string WeeklyMinPriceChangeRate => CalculatePercentageChange(MinPrice, LastWeekMinPrice);
        public string WeeklyMaxPriceChangeRate => CalculatePercentageChange(MaxPrice, LastWeekMaxPrice);

        public decimal LastYearMinPrice { get; set; }
        public decimal LastYearMaxPrice { get; set; }
        public string YearlyMinPriceChangeRate => CalculatePercentageChange(MinPrice, LastYearMinPrice);
        public string YearlyMaxPriceChangeRate => CalculatePercentageChange(MaxPrice, LastYearMaxPrice);

        public decimal LastMonthMinPrice { get; set; }
        public decimal LastMonthMaxPrice { get; set; }
        public string MonthlyMinPriceChangeRate => CalculatePercentageChange(MinPrice, LastMonthMinPrice);
        public string MonthlyMaxPriceChangeRate => CalculatePercentageChange(MaxPrice, LastMonthMaxPrice);

        public static string CalculatePercentageChange(decimal current, decimal last)
        {
            if (last == 0)
                return "0";

            return (((current - last) / last) * 100).ToString("+0.00;-0.00;0.00") + "%";
        }
    }
}
