namespace FamilyAllowanceCalculator.Models
{
    public class FamilyAllowanceModel
    {
        public double HoursFulltime { get; set; } = 38.5;
        public double HoursParttime { get; set; } = 25;

        private double _grossSalaryFulltimeMonthly = 2500;

        public double GrossSalaryFulltimeMonthly
        {
            get => _grossSalaryFulltimeMonthly;
            set => _grossSalaryFulltimeMonthly = Math.Max(0, value);
        }

        public double GrossSalaryParttimeMonthly
        {
            get => _grossSalaryFulltimeMonthly * (HoursParttime / HoursFulltime);
            set => _grossSalaryFulltimeMonthly = Math.Max(0, value / (HoursParttime / HoursFulltime));
        }

        public double GrossSalaryFulltime12X
        {
            get => _grossSalaryFulltimeMonthly * 12;
            set => _grossSalaryFulltimeMonthly = Math.Max(0, value / 12);
        }

        public double GrossSalaryFulltime14X
        {
            get => _grossSalaryFulltimeMonthly * 14;
            set => _grossSalaryFulltimeMonthly = Math.Max(0, value / 14);
        }

        public double GrossSalaryParttime12X
        {
            get => GrossSalaryParttimeMonthly * 12;
            set => _grossSalaryFulltimeMonthly = Math.Max(0, (value / 12) / (HoursParttime / HoursFulltime));
        }

        public double GrossSalaryParttime14X
        {
            get => GrossSalaryParttimeMonthly * 14;
            set => _grossSalaryFulltimeMonthly = Math.Max(0, (value / 14) / (HoursParttime / HoursFulltime));
        }

        public double FamilyAllowanceLimit { get; set; } = 17212;
        public double AllowableExpenses { get; set; } = 132;

        // SV rates
        public double SocialSecurityKrankenversicherung { get; set; } = 3.87;
        public double SocialSecurityUnfallversicherung { get; set; } = 0;
        public double SocialSecurityPensionsversicherung { get; set; } = 10.25;

        public double SocialSecurityArbeitslosenversicherung
        {
            get => ComputeAv(GrossSalaryParttimeMonthly);
            set { }
        }

        private static double ComputeAv(double grossSalaryParttimeMonthly)
        {
            if (grossSalaryParttimeMonthly <= 2074) return 0;
            if (grossSalaryParttimeMonthly <= 2262) return 1;
            if (grossSalaryParttimeMonthly <= 2451) return 2;
            return 2.95;
        }

        public double SocialSecurityAkumlage { get; set; } = 0.5;
        public double SocialSecurityWohnbaufoerderungsbeitrag { get; set; } = 0.5;
        private double? _socialSecurityTotalPercentage;

        public double SocialSecurityTotalPercentage
        {
            get => _socialSecurityTotalPercentage ?? (SocialSecurityKrankenversicherung +
                                                      SocialSecurityUnfallversicherung +
                                                      SocialSecurityPensionsversicherung +
                                                      SocialSecurityArbeitslosenversicherung + SocialSecurityAkumlage +
                                                      SocialSecurityWohnbaufoerderungsbeitrag);
            set => _socialSecurityTotalPercentage = value;
        }

        // Calculated values
        public double SocialSecurity12X => GrossSalaryParttime12X * (SocialSecurityTotalPercentage / 100.0);
        public double SocialSecurityMonthly => SocialSecurity12X / 12;

        public double TaxableIncome12X => (GrossSalaryParttime12X - SocialSecurity12X) - AllowableExpenses;

        private double _familyAllowanceMonthly = 280.2;

        public double FamilyAllowanceMonthly
        {
            get => _familyAllowanceMonthly;
            set => _familyAllowanceMonthly = Math.Max(0, value);
        }

        public double FamilyAllowance12X
        {
            get => _familyAllowanceMonthly * 12;
            set => _familyAllowanceMonthly = Math.Max(0, value / 12);
        }

        public double FamilyAllowanceToBePaidBack =>
            Math.Max(0, Math.Min(FamilyAllowance12X, TaxableIncome12X - FamilyAllowanceLimit));

        public double IncomeTax12X
        {
            get
            {
                if (TaxableIncome12X <= 13308) return 0;
                if (TaxableIncome12X <= 21617) return (TaxableIncome12X - 13308) * 0.2;
                if (TaxableIncome12X <= 35836) return (TaxableIncome12X - 21617) * 0.3 + 1661.8;
                if (TaxableIncome12X <= 69166) return (TaxableIncome12X - 35836) * 0.4 + 5927.5;
                if (TaxableIncome12X <= 103072) return (TaxableIncome12X - 69166) * 0.48 + 19259.5;
                if (TaxableIncome12X <= 1000000) return (TaxableIncome12X - 103072) * 0.5 + 35534.38;
                return (TaxableIncome12X - 1000000) * 0.55 + 483998.38;
            }
        }

        private double IncomeTaxMonthly => IncomeTax12X / 12;
        public double Absetzbetraege12X { get; set; } = 463;
        private double AbsetzbetraegeMonthly => Absetzbetraege12X / 12;

        public double NetSalaryMonthly => GrossSalaryParttimeMonthly - SocialSecurityMonthly - IncomeTaxMonthly +
                                          AbsetzbetraegeMonthly;

        public double NetSalary12X => NetSalaryMonthly * 12;
        public double NetSalary14X => 1.242 * NetSalary12X - 1295; // approximation

        public double EffectiveNetSalary14X => NetSalary14X + FamilyAllowance12X - FamilyAllowanceToBePaidBack;

        private const int WeeksPerYear = 52;
        private const int HolidaysPerYear = 5;

        public double EffectiveNetHourly => EffectiveNetSalary14X / (HoursParttime * (WeeksPerYear - HolidaysPerYear));
    }
}