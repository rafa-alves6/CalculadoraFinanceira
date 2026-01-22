using System;

public class Loan
{
    public double PresentValue { get; set; }
    public double InterestRate { get; set; }
    public int Time { get; set; }
    public string AmortizationSystem { get; set; }

    public Loan(double presentValue, double interestRate, int time, string amortizationSystem) 
	{
        PresentValue = presentValue;
        InterestRate = interestRate;
        Time = time;
        AmortizationSystem = amortizationSystem;
	}
}
